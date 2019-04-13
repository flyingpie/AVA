using MUI;
using MUI.Graphics;
using MUI.Logging;
using System;
using System.Diagnostics;

namespace AVA.Plugins.UnitConverter
{
    public class Macro
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Command { get; set; }

        public bool RunOnMatch { get; set; }

        public virtual void Execute()
        {
        }

        public virtual Image GetIcon()
        {
            return ResourceManager.Instance.DefaultImage;
        }
    }

    public class StartProgramMacro : Macro
    {
        public string FileName { get; set; }

        public string Arguments { get; set; }

        public string WorkingDirectory { get; set; }

        public bool RunAsAdmin { get; set; }

        public override void Execute()
        {
            var log = Log.Get(this);

            try
            {
                var proc = new Process();

                proc.StartInfo.FileName = FileName;
                proc.StartInfo.Arguments = Arguments;
                proc.StartInfo.WorkingDirectory = WorkingDirectory;

                if (RunAsAdmin)
                {
                    proc.StartInfo.Verb = "runas";
                }

                proc.Start();
                proc.Dispose();
            }
            catch (Exception ex)
            {
                log.Error($"Could not start process '{FileName}': {ex.Message}");
            }
        }
    }
}