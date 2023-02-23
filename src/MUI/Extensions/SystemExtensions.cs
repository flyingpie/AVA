﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System
{
	public static class SystemExtensions
	{
		public static string BufferToString(this byte[] buffer)
		{
			return new string(buffer.TakeWhile(c => c > 0).Select(b => (char)b).ToArray());
		}

		public static void ClearBuffer(this byte[] buffer)
		{
			for (int i = 0; i < buffer.Length; i++) buffer[i] = 0;
		}

		public static void CopyToBuffer(this byte[] buffer, string text)
		{
			buffer.ClearBuffer();

			if (string.IsNullOrWhiteSpace(text)) return;

			var bytes = Encoding.UTF8.GetBytes(text);

			for (int i = 0; i < text.Length && i < buffer.Length; i++) buffer[i] = bytes[i];
		}

		public static bool ContainsCaseInsensitive(this string source, string term)
		{
			return source.ToLowerInvariant().Contains(term.ToLowerInvariant());
		}

		public static string ExpandEnvVars(this string source)
		{
			if (string.IsNullOrWhiteSpace(source)) return source;

			return Environment.ExpandEnvironmentVariables(source);
		}

		public static string FromAppRoot(this string path)
		{
			path = path ?? string.Empty;

			var loc = typeof(MUI.UIContext).Assembly.Location;

			//loc = Path.GetDirectoryName(Path.GetDirectoryName(loc));
			loc = Path.GetDirectoryName(loc);

			return Path.Combine(loc, path);
		}

		public static string FromAppBin(this string path)
		{
			path = path ?? string.Empty;

			var loc = typeof(MUI.UIContext).Assembly.Location;

			loc = Path.GetDirectoryName(loc);

			return Path.Combine(loc, path);
		}

		public static string FromPluginRoot<T>(this string path)
			where T : class
		{
			path = path ?? string.Empty;

			return path.FromPluginRoot(typeof(T));
		}

		public static string FromPluginRoot(this string path, Type pluginType)
		{
			path = path ?? string.Empty;

			var loc = pluginType.Assembly.Location;

			loc = Path.GetDirectoryName(loc);

			return Path.Combine(loc, path);
		}

		public static List<string> GetFilesRecursive(this IEnumerable<string> folders, List<string> files = null)
		{
			files = files ?? new List<string>();

			foreach (var folder in folders)
			{
				try
				{
					foreach (var app in Directory.GetFiles(folder, "*", SearchOption.TopDirectoryOnly))
					{
						files.Add(app);
					}

					var dd = Directory.GetDirectories(folder);

					GetFilesRecursive(dd, files);
				}
				catch { }
			}

			return files;
		}

		public static byte[] ToByteArray(this Drawing.Image image) => image.ToByteArray(Drawing.Imaging.ImageFormat.Bmp);

		public static byte[] ToByteArray(this Drawing.Image image, Drawing.Imaging.ImageFormat format)
		{
			using (var stream = new MemoryStream())
			{
				image.Save(stream, format);

				stream.Position = 0;

				return stream.ToArray();
			}
		}
	}
}