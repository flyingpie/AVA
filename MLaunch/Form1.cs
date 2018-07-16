using NCalc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLaunch
{
    // TODO: url detection (elastic.co -> open "elastic.co", "some term" -> open "ddg.com?q=some+term"
    public partial class Form1 : Form
    {
        private const string urlPattern = "^" +
            // protocol identifier
            "(?:(?:https?|ftp)://|)" +
            // user:pass authentication
            "(?:\\S+(?::\\S*)?@)?" +
            "(?:" +
            // IP address exclusion
            // private & local networks
            "(?!(?:10|127)(?:\\.\\d{1,3}){3})" +
            "(?!(?:169\\.254|192\\.168)(?:\\.\\d{1,3}){2})" +
            "(?!172\\.(?:1[6-9]|2\\d|3[0-1])(?:\\.\\d{1,3}){2})" +
            // IP address dotted notation octets
            // excludes loopback network 0.0.0.0
            // excludes reserved space >= 224.0.0.0
            // excludes network & broacast addresses
            // (first & last IP address of each class)
            "(?:[1-9]\\d?|1\\d\\d|2[01]\\d|22[0-3])" +
            "(?:\\.(?:1?\\d{1,2}|2[0-4]\\d|25[0-5])){2}" +
            "(?:\\.(?:[1-9]\\d?|1\\d\\d|2[0-4]\\d|25[0-4]))" +
            "|" +
            // host name
            "(?:(?:[a-z\\u00a1-\\uffff0-9]-*)*[a-z\\u00a1-\\uffff0-9]+)" +
            // domain name
            "(?:\\.(?:[a-z\\u00a1-\\uffff0-9]-*)*[a-z\\u00a1-\\uffff0-9]+)*" +
            // TLD identifier
            "(?:\\.(?:[a-z\\u00a1-\\uffff]{2,}))" +
            ")" +
            // port number
            "(?::\\d{2,5})?" +
            // resource path
            "(?:/\\S*)?" +
            "$";

        private Regex reg = new Regex(urlPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private PerformanceCounter _cpuCounter;
        private PerformanceCounter _ramCounterAv;
        private PerformanceCounter _ramCounterUse;

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(int hwnd);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        private List<App> _apps;

        private Task _t;

        public Form1()
        {
            _cpuCounter = new PerformanceCounter();
            _cpuCounter.CategoryName = "Processor";
            _cpuCounter.CounterName = "% Processor Time";
            _cpuCounter.InstanceName = "_Total";

            _ramCounterAv = new PerformanceCounter("Memory", "Available Bytes");
            _ramCounterUse = new PerformanceCounter("Memory", "Committed Bytes");

            var folders = new[]
            {
                @"%ProgramData%\Microsoft\Windows\Start Menu\Programs",
                @"%APPDATA%\Microsoft\Windows\Start Menu",
                @"%NEXTCLOUD%"
            }.Select(f => Environment.ExpandEnvironmentVariables(f)).ToList();

            _apps = new List<App>();

            LookForApps(folders, _apps);

            _apps.ForEach(app => Console.WriteLine($"APP {app}"));

            InitializeComponent();

            //HotKeyManager.RegisterHotKey(Keys.Space, KeyModifiers.Control);
            HotKeyManager.RegisterHotKey(Keys.Space, KeyModifiers.Alt);

            HotKeyManager.HotKeyPressed += (s, a) =>
            {
                //Console.WriteLine($"{a.Key} {a.Modifiers}");

                //if (Visible) Hide();
                //else Show();

                Toggle();
            };

            ShowInTaskbar = false;

            _t = Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        //GetPhysicallyInstalledSystemMemory(out long memTotal);

                        var memAv = PerformanceInfo.GetPhysicalAvailableMemoryInMiB() * 1024 * 1024;
                        var memTot = PerformanceInfo.GetTotalMemoryInMiB() * 1024 * 1024;
                        var memUse = memTot - memAv;

                        lblCpu.Text = $"CPU {Math.Round(_cpuCounter.NextValue())}%";
                        lblMem.Text = $"MEM {Describe(memUse)} / {Describe(memTot)}";
                    }
                    catch { }

                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            });

            UpdateList(null);
        }

        private void LookForApps(IEnumerable<string> folders, List<App> apps)
        {
            foreach (var folder in folders)
            {
                try
                {
                    //Console.WriteLine($"{folder}");

                    foreach (var app in Directory.GetFiles(folder, "*.exe", SearchOption.TopDirectoryOnly))
                    {
                        apps.Add(new App(app));
                    }

                    foreach (var app in Directory.GetFiles(folder, "*.lnk", SearchOption.TopDirectoryOnly))
                    {
                        apps.Add(new App(app));
                    }

                    var dd = Directory.GetDirectories(folder);

                    LookForApps(dd, apps);

                    //_apps.AddRange(apps);

                    //Console.WriteLine($"{apps.Count} apps");
                }
                catch { }
                finally
                {
                }
            }
        }

        private string _term;

        private void UpdateList(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                listBox1.Items.Clear();
                listBox1.Items.AddRange(new[]
                {
                    "<app name>",
                    "website.com",

                    "d <web search term>",
                    "g <github search term>",
                    "w <wikipedia search term>",

                    "1 + 1",
                    ">cmd"
                });

                return;
            }

            term = term.ToLowerInvariant();

            var items = _apps
                .Where(app => app.NameLower.Contains(term))
                .Take(10)
                .ToArray();

            listBox1.BeginUpdate();

            listBox1.Items.Clear();
            listBox1.Items.AddRange(items);

            if (items.Any())
                listBox1.SelectedIndex = 0;

            listBox1.EndUpdate();
        }

        public void ClearSearch()
        {
            textBox1.Clear();
            UpdateList(null);
        }

        public void Toggle()
        {
            if (Visible) Down();
            else Up();
        }

        public void Up()
        {
            var screen = Screen.FromPoint(MousePosition);

            StartPosition = FormStartPosition.Manual;

            var centerX = screen.WorkingArea.Width / 2;
            var centerY = screen.WorkingArea.Height / 2;

            var x = centerX - (Width / 2);
            var y = centerY - (Height / 2);

            Location = new System.Drawing.Point(screen.WorkingArea.Location.X + x, screen.WorkingArea.Location.Y + y);

            Show();

            SetForegroundWindow((int)Handle);

            textBox1.Focus();
        }

        public void Down()
        {
            Hide();
            ClearSearch();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            // Up/down
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;

                if (e.KeyCode == Keys.Down && listBox1.SelectedIndex < listBox1.Items.Count - 1)
                {
                    listBox1.SelectedIndex++;
                    return;
                }

                if (e.KeyCode == Keys.Up && listBox1.SelectedIndex > 0)
                {
                    listBox1.SelectedIndex--;
                    return;
                }

                return;
            }

            // Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Calculator
                if (!string.IsNullOrWhiteSpace(_parsedExpression))
                {
                    Clipboard.SetText(_parsedExpression);
                    Down();
                    return;
                }

                // Plugins
                var prefs = new Dictionary<string, Action<string>>()
                {
                    {"d", term => Process.Start($"https://duckduckgo.com/?q=c%23+{term}") },
                    {"g", term => Process.Start($"https://github.com/search?q={term}") },
                    {"w", term => Process.Start($"https://en.wikipedia.org/w/index.php?search={term}") }
                };

                foreach (var pref in prefs)
                {
                    var fpref = pref.Key + " ";
                    if (textBox1.Text.StartsWith(fpref) && textBox1.Text.Length > fpref.Length)
                    {
                        var term = textBox1.Text.Substring(fpref.Length);

                        try
                        {
                            pref.Value(term);
                        }
                        catch { }

                        Down();

                        return;
                    }
                }

                if (reg.IsMatch(textBox1.Text))
                {
                    var url = textBox1.Text;
                    if (!url.ToLower().StartsWith("http")) url = "http://" + url;

                    Process.Start(url);

                    Down();

                    return;
                }

                if (textBox1.Text.StartsWith(">"))
                {
                    var cmd = textBox1.Text.Substring(1).Trim();
                    var args = "";

                    var sp = cmd.IndexOf(' ');
                    if (sp > 0)
                    {
                        args = cmd.Substring(sp).Trim();
                        cmd = cmd.Substring(0, sp);
                    }

                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = cmd,
                        Arguments = args
                    });

                    Down();

                    return;
                }

                // Start process
                var selected = listBox1.SelectedItem as App;

                if (selected != null)
                {
                    selected.Start((e.Modifiers & Keys.Control) == Keys.Control);

                    Down();
                }

                return;
            }

            if (e.KeyCode == Keys.Escape)
            {
                Down();
                return;
            }

            // Type
            var plugins = new Dictionary<string, Action<string>>()
            {
                {
                    "time",
                    term =>
                    {
                        var tz = TimeZoneInfo.GetSystemTimeZones().ToList();

                        var timeZones = new[]
                        {
                            tz.FirstOrDefault(t => t.Id == "UTC"),
                            tz.FirstOrDefault(t => t.Id == "Pacific Standard Time"),
                            tz.FirstOrDefault(t => t.Id == "Eastern Standard Time"),
                            tz.FirstOrDefault(t => t.Id == "Russian Standard Time"),
                            tz.FirstOrDefault(t => t.Id == "Tokyo Standard Time"),
                            tz.FirstOrDefault(t => t.Id == "AUS Eastern Standard Time")
                        }.OrderBy(t => t.BaseUtcOffset).ToList();

                        var now = DateTimeOffset.UtcNow;

                        listBox1.BeginUpdate();
                        listBox1.Items.Clear();

                        foreach(var z in timeZones)
                        {
                            var dt = now.Add(z.BaseUtcOffset);
                            var dst = z.IsDaylightSavingTime(dt);
                            var name = dst ? z.DaylightName : z.StandardName;

                            listBox1.Items.Add($"{dt.ToString("dd-MM-yyyy HH:mm")} {name} {(dst ? " (DST)" : "")}");
                        }

                        listBox1.EndUpdate();
                    }
                }
            };

            var plugin = plugins.FirstOrDefault(p => textBox1.Text.StartsWith(p.Key));

            if (plugin.Value != null)
            {
                plugin.Value(_term);

                return;
            }

            try
            {
                _parsedExpression = null;

                Console.WriteLine(textBox1.Text);

                _parsedExpression = new Expression(textBox1.Text).Evaluate().ToString();

                listBox1.Items.Clear();

                listBox1.Items.Add(_parsedExpression);

                return;
            }
            catch { }

            if (_term != textBox1.Text)
            {
                _term = textBox1.Text;
                UpdateList(_term);
                return;
            }
        }

        private string _parsedExpression;

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        public static string Describe(long byteCount)
        {
            var units = new[] { "B", "KB", "MB", "GB" };

            for (int i = 0; i < units.Length; i++)
            {
                if (byteCount < 1024)
                    return byteCount + units[i];

                byteCount /= 1024;
            }

            return "unknown";
        }
    }

    public enum AppType
    {
        Exe,
        Shortcut,
        Unknown
    }

    public class App
    {
        public AppType AppType { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public string NameLower { get; set; }

        public string Description { get; set; }

        public App(string path)
        {
            if (path.EndsWith(".exe")) AppType = AppType.Exe;
            if (path.EndsWith(".lnk")) AppType = AppType.Shortcut;

            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
            NameLower = Name.ToLowerInvariant();

            //var x = FileVersionInfo.GetVersionInfo(path);

            //var y = 2;

            Description = ""; // x.FileDescription;

            if (AppType == AppType.Shortcut)
            {
                var ss = ShortcutUtils.ResolveShortcut(Path);

                var xx = 2;
            }
        }

        public void Start(bool asAdmin)
        {
            try
            {
                //var p = new Process();
                //p.StartInfo.FileName = Path;
                //p.Start();

                var startInfo = new ProcessStartInfo()
                {
                    FileName = Path
                };

                if (asAdmin) startInfo.Verb = "runas";

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wups: {ex.Message}");

                var xx = 2;
            }
        }

        public override string ToString()
        {
            //return $"[{AppType}] {Name} - {Description}";
            return Name;
        }
    }

    public static class PerformanceInfo
    {
        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation, [In] int Size);

        [StructLayout(LayoutKind.Sequential)]
        public struct PerformanceInformation
        {
            public int Size;
            public IntPtr CommitTotal;
            public IntPtr CommitLimit;
            public IntPtr CommitPeak;
            public IntPtr PhysicalTotal;
            public IntPtr PhysicalAvailable;
            public IntPtr SystemCache;
            public IntPtr KernelTotal;
            public IntPtr KernelPaged;
            public IntPtr KernelNonPaged;
            public IntPtr PageSize;
            public int HandlesCount;
            public int ProcessCount;
            public int ThreadCount;
        }

        public static Int64 GetPhysicalAvailableMemoryInMiB()
        {
            PerformanceInformation pi = new PerformanceInformation();
            if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64((pi.PhysicalAvailable.ToInt64() * pi.PageSize.ToInt64() / 1048576));
            }
            else
            {
                return -1;
            }
        }

        public static Int64 GetTotalMemoryInMiB()
        {
            PerformanceInformation pi = new PerformanceInformation();
            if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64((pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / 1048576));
            }
            else
            {
                return -1;
            }
        }
    }
}