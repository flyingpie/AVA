using MUI.DI;
using MUI.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace AVA.Core.Settings
{
    public class SettingsRoot
    {
        public static SettingsRoot Instance { get; } = new SettingsRoot();

        public JObject Settings { get; private set; }

        private string _path = "settings.json".FromAppRoot();
        private JsonSerializerSettings _serializeSettings;
        private ILog _log;

        private SettingsRoot()
        {
            _serializeSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            _serializeSettings.Converters.Add(new StringEnumConverter());

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
            Settings = new JObject();

            try
            {
                if (File.Exists(_path))
                {
                    Settings = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(_path), _serializeSettings);
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Could not load settings: '{ex.Message}'");
            }
        }

        public void Save()
        {
            try
            {
                File.WriteAllText(_path, JsonConvert.SerializeObject(Settings, _serializeSettings));
            }
            catch (Exception ex)
            {
                _log.Error($"Could not save settings: '{ex.Message}'");
            }
        }

        public T Get<T>(Func<object> defaultObject = null) => (T)Get(typeof(T), defaultObject);

        public object Get(Type type, Func<object> defaultObject = null)
        {
            var s = Settings[type.FullName]?.ToObject(type) ?? defaultObject?.Invoke();

            Set(s, type);

            return s;
        }

        public void Set(object settings, Type type)
        {
            Settings[type.FullName] = JObject.FromObject(settings);
        }
    }
}