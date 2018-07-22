using MLaunch.Core.QueryExecutors.ListQuery;
using MUI;
using MUI.DI;
using MUI.Graphics;
using MUI.Win32.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLaunch.Plugins.Dummy
{
    public class DummyQueryExecutor : ListQueryExecutor
    {
        [Dependency] private ResourceManager _resourceManager;

        private Image _avatar;

        private ListQueryResult[] _dummyResults;

        private Dictionary<string, Image> _images;

        [RunAfterInject]
        private void Initialize()
        {
            _images = new Dictionary<string, Image>();
            //_avatar = new Image(_resourceManager.LoadTexture(@"Resources\Images\crashlog-doom.png"));
            _avatar = _resourceManager.LoadImage(@"Resources\Images\crashlog-doom.png");

            _dummyResults = new[]
            {
                new ListQueryResult()
                {
                    Name = "Notepad++",
                    Description = @"D:\Syncthing\Apps\Notepad++\notepad++.exe",
                    //Icon = _avatar,
                    OnExecute = t => Console.WriteLine("npp")
                },
                new ListQueryResult()
                {
                    Name = "TailBlazer",
                    Description = @"D:\Syncthing\Apps\Dev\TailBlazer\TailBlazer.exe",
                    //Icon = _avatar,
                    OnExecute = t => Console.WriteLine("tb")
                },
                new ListQueryResult()
                {
                    Name = "Powershell",
                    Description = @"C:\Users\Marco van den Oever\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Windows PowerShell\Windows PowerShell.lnk",
                    //Icon = _avatar,
                    OnExecute = t => Console.WriteLine("ps")
                },
                new ListQueryResult()
                {
                    Name = "KeePass",
                    Description = @"D:\Syncthing\Apps\KeePass\KeePass.exe",
                    //Icon = _avatar,
                    OnExecute = t => Console.WriteLine("kp")
                }
            };
        }

        public override int Order => 999;

        public override IList<IListQueryResult> GetQueryResults(string term)
        {
            return _dummyResults
                .Where(d => d.Name.ToLowerInvariant().Contains(term.ToLowerInvariant()))
                .Select(d => new ListQueryResult()
                {
                    Name = d.Name,
                    Description = d.Description,
                    Icon = _resourceManager.LoadImageFromIcon(d.Description),
                    OnExecute = d.OnExecute
                })
                .Cast<IListQueryResult>()
                .ToList();
        }

        //private Image LoadImageFromIcon(string path)
        //{
        //    try
        //    {
        //        path = Path.GetFullPath(path);

        //        if (!_images.ContainsKey(path))
        //        {
        //            using (var icoExe = System.Drawing.Icon.ExtractAssociatedIcon(path))
        //            using (var str = new MemoryStream())
        //            {
        //                icoExe.ToBitmap().Save(str, System.Drawing.Imaging.ImageFormat.Bmp);

        //                var image = new Image(_resourceManager.LoadTexture(str.ToArray()));

        //                _images[path] = image;
        //            }
        //        }

        //        return _images[path];
        //    }
        //    catch { }

        //    return _avatar;
        //}
    }
}