using MUI.DI;
using MUI.Logging;
using Nett;
using System;
using System.IO;
using System.Linq;

namespace AVA.Core.Settings
{
    public class SettingsRoot
    {
        public static SettingsRoot Instance { get; } = new SettingsRoot();

        private string[] _paths = new[]
        {
            "settings.toml".FromAppRoot(),
            Path.Combine("".FromAppRoot(), nameof(AVA), "settings.toml")
        };

        private string _path;
        private TomlTable _settings;
        private ILog _log;

        private SettingsRoot()
        {
            _log = Log.Get(this);

            Load();
        }

        public void Initialize(IContainer container)
        {
            AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(ass => ass.DefinedTypes)
                .Where(type => typeof(Settings).IsAssignableFrom(type))
                .Where(type => !type.IsAbstract)
                .ToList()
                .ForEach(type => container.Register(type, c => Instance.Get(type, () => Activator.CreateInstance(type))))
            ;
        }

        public void Load()
        {
            _path = _paths.First();
            _settings = Toml.Create();

            foreach (var path in _paths)
            {
                try
                {
                    if (File.Exists(path))
                    {
                        _path = path;

                        _log.Error($"Loading settings from path '{_path}'...");

                        _settings = Toml.ReadFile(_path);
                    }
                }
                catch (Exception ex)
                {
                    _log.Error($"Could not load settings: '{ex.Message}'");
                }
            }
        }

        public void Save()
        {
            try
            {
                Toml.WriteFile(_settings, _path);
            }
            catch (Exception ex)
            {
                _log.Error($"Could not save settings: '{ex.Message}'");
            }
        }

        public T Get<T>(Func<object> defaultObject = null) => (T)Get(typeof(T), defaultObject);

        public object Get(Type type, Func<object> defaultObject = null)
        {
            var s = _settings.ContainsKey(type.FullName) ?
                _settings.Get(type.FullName).Get(type)
                : defaultObject?.Invoke()
            ;

            Set(s, type);

            return s;
        }

        public void Set(object settings, Type type)
        {
            _settings[type.FullName] = Toml.Create(settings);
        }
    }
}