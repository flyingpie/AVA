using MUI.DI;
using MUI.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AVA.Core.Settings
{
	public class SettingsRoot
	{
		public static SettingsRoot Instance { get; } = new SettingsRoot();

		public JObject Settings { get; private set; }

		private string[] _paths = new[]
		{
			"settings.json".FromAppRoot(),
			System.IO.Path.Combine("".FromAppRoot(), nameof(AVA), "settings.json")
		};

		public string Path { get; private set; }

		private JsonSerializerSettings _serializeSettings;
		private ILog _log;

		private SettingsRoot()
		{
			_serializeSettings = new JsonSerializerSettings()
			{
				Formatting = Formatting.Indented
			};

			_serializeSettings.Converters.Add(new StringEnumConverter());

			Path = _paths.First();
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

			foreach (var path in _paths)
			{
				try
				{
					if (File.Exists(path))
					{
						Path = path;

						_log.Info($"Loading settings from path '{Path}'...");

						Settings = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Path), _serializeSettings);
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
				File.WriteAllText(Path, JsonConvert.SerializeObject(Settings, _serializeSettings));
			}
			catch (Exception ex)
			{
				_log.Error($"Could not save settings: '{ex.Message}'");
			}
		}

		public T Get<T>(Func<object> defaultObject = null) => (T)Get(typeof(T), defaultObject);

		public object Get(Type type, Func<object> defaultObject = null)
		{
			var sectionAttr = type.GetCustomAttribute<SectionAttribute>();
			var sectionName = sectionAttr?.Name ?? type.FullName;

			var s = Settings
				.Children<JProperty>()
				.FirstOrDefault(c => c.Name.Equals(sectionName, StringComparison.OrdinalIgnoreCase))
				?.Value
				?.ToObject(type)
				?? defaultObject?.Invoke()
			;

			Set(s, type);

			return s;
		}

		public void Set(object settings, Type type)
		{
			Settings[type.FullName] = JObject.FromObject(settings);
		}
	}
}