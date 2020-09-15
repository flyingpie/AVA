using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Windows.Management.Deployment;

namespace MUI.UWP
{
	public static class NativeApiManifestHelpers
	{
		public const int STGM_SHARE_DENY_NONE = 0x40;

		public static IEnumerable<PackageInfoEx> GetAllPackages()
		{
			var packageManager = new PackageManager();

			return ToPackageInfoEx(packageManager.FindPackagesForUser(string.Empty)).ToList();
		}

		private static ManifestInfo GetAppInfoFromManifest(string manifestFilePath)
		{
			if (File.Exists(manifestFilePath))
			{
				var factory = (NativeApiHelper.IAppxFactory)new NativeApiHelper.AppxFactory();

				IStream manifestStream;

				NativeApiHelper.SHCreateStreamOnFileEx(manifestFilePath, STGM_SHARE_DENY_NONE, 0, false, IntPtr.Zero, out manifestStream);

				if (manifestStream != null)
				{
					var manifestInfo = new ManifestInfo();

					var reader = factory.CreateManifestReader(manifestStream);
					var properties = reader.GetProperties();

					manifestInfo.Properties = properties;

					var apps = reader.GetApplications();

					while (apps.GetHasCurrent())
					{
						var app = apps.GetCurrent();
						var manifestApplication = new ManifestApplication(app);

						manifestApplication.AppListEntry = NativeApiHelper.GetStringValue(app, "AppListEntry");
						manifestApplication.Description = NativeApiHelper.GetStringValue(app, "Description");
						manifestApplication.DisplayName = NativeApiHelper.GetStringValue(app, "DisplayName");
						manifestApplication.EntryPoint = NativeApiHelper.GetStringValue(app, "EntryPoint");
						manifestApplication.Executable = NativeApiHelper.GetStringValue(app, "Executable");
						manifestApplication.Id = NativeApiHelper.GetStringValue(app, "Id");
						manifestApplication.Logo = NativeApiHelper.GetStringValue(app, "Logo");
						manifestApplication.SmallLogo = NativeApiHelper.GetStringValue(app, "SmallLogo");
						manifestApplication.StartPage = NativeApiHelper.GetStringValue(app, "StartPage");
						manifestApplication.Square150x150Logo = NativeApiHelper.GetStringValue(app, "Square150x150Logo");
						manifestApplication.Square30x30Logo = NativeApiHelper.GetStringValue(app, "Square30x30Logo");
						manifestApplication.BackgroundColor = NativeApiHelper.GetStringValue(app, "BackgroundColor");
						manifestApplication.ForegroundText = NativeApiHelper.GetStringValue(app, "ForegroundText");
						manifestApplication.WideLogo = NativeApiHelper.GetStringValue(app, "WideLogo");
						manifestApplication.Wide310x310Logo = NativeApiHelper.GetStringValue(app, "Wide310x310Logo");
						manifestApplication.ShortName = NativeApiHelper.GetStringValue(app, "ShortName");
						manifestApplication.Square310x310Logo = NativeApiHelper.GetStringValue(app, "Square310x310Logo");
						manifestApplication.Square70x70Logo = NativeApiHelper.GetStringValue(app, "Square70x70Logo");
						manifestApplication.MinWidth = NativeApiHelper.GetStringValue(app, "MinWidth");
						manifestInfo.Apps.Add(manifestApplication);
						apps.MoveNext();
					}
					Marshal.ReleaseComObject(manifestStream);

					return manifestInfo;
				}
				else
				{
					Debug.WriteLine("Call to SHCreateStreamOnFileEx failed on : " + manifestFilePath);
				}
			}
			else
			{
				Debug.WriteLine("Manifest File Missing: " + manifestFilePath);
			}

			return null;
		}

		public enum FindLogoScaleStrategy
		{
			Highest = 0,
			NeareastToCustomScale = 1,
		}

		public static string FindLogoImagePath(string path, string resourceName, FindLogoScaleStrategy findLogoScaleStrategy, int scaleValue = 100)
		{
			var isValidFindStrategy = Enum.IsDefined(typeof(FindLogoScaleStrategy), findLogoScaleStrategy);

			if (!isValidFindStrategy)
			{
				throw new ArgumentException("Invalid find logo strategy." + findLogoScaleStrategy);
			}

			if (string.IsNullOrWhiteSpace(resourceName))
			{
				return null;
			}

			const string fileNameScaleToken = ".scale-";

			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(resourceName);
			string fileExtension = Path.GetExtension(resourceName);

			var folderPath = Path.Combine(path, Path.GetDirectoryName(resourceName));

			if (!Directory.Exists(folderPath))
			{
				return null;
			}

			// Try to find logos by file names in folder first
			var files = Directory.EnumerateFiles(Path.Combine(path, folderPath), fileNameWithoutExtension + fileNameScaleToken + "*" + fileExtension).ToList();

			files = files.Select(Path.GetFileNameWithoutExtension).ToList();

			if (files.Any())
			{
				var foundFilesScale = GetDesiredLogoScale(files, findLogoScaleStrategy, fileNameScaleToken, scaleValue);

				var sizedFilePath = Path.Combine(path, Path.GetDirectoryName(resourceName), fileNameWithoutExtension + fileNameScaleToken + foundFilesScale + fileExtension);

				if (File.Exists(sizedFilePath))
				{
					return sizedFilePath;
				}
			}

			// If no files found try to mach again sub folder names
			const string folderNameScaleToken = "scale-";

			var folders = Directory.EnumerateDirectories(folderPath).Where(p => p.Contains("scale-")).ToList();

			if (folders.Any())
			{
				var foundScale = GetDesiredLogoScale(folders, findLogoScaleStrategy, folderNameScaleToken, scaleValue);

				if (foundScale != null)
				{
					var sizedFolderPath = Path.Combine(
						folderPath,
						folderNameScaleToken + foundScale,
						fileNameWithoutExtension + fileExtension);

					if (File.Exists(sizedFolderPath))
					{
						return sizedFolderPath;
					}
				}
			}

			// Finally just do an exact match with no
			// scale specifier
			var finalPath = Path.Combine(path, resourceName);

			if (File.Exists(finalPath))
			{
				return finalPath;
			}

			return null;
		}

		private static int? GetDesiredLogoScale(IList<string> paths, FindLogoScaleStrategy findLogoScaleStrategy, string token, int desiredScaleValue)
		{
			var scales = new List<int>();

			foreach (var path in paths)
			{
				int pos = path.ToLower().IndexOf(token) + token.Length;
				string sizeText = path.Substring(pos);
				int size;

				if (int.TryParse(sizeText, out size))
				{
					scales.Add(size);
				}
			}

			if (scales.Count > 0)
			{
				int closestScale = desiredScaleValue;

				if (findLogoScaleStrategy == FindLogoScaleStrategy.Highest)
				{
					closestScale = scales.Max();
				}
				else if (findLogoScaleStrategy == FindLogoScaleStrategy.NeareastToCustomScale)
				{
					closestScale =
						scales.Aggregate((x, y) => Math.Abs(x - desiredScaleValue) < Math.Abs(y - desiredScaleValue) ? x : y);
				}

				return closestScale;
			}

			return null;
		}

		public static string GetBestLogoPath(
			ManifestInfo manifestInfo,
			ManifestApplication manifestApplication,
			string installedLocationPath)
		{
			var findStrategy = FindLogoScaleStrategy.NeareastToCustomScale;
			string logoPath;

			if (manifestInfo.Apps.Count > 1)
			{
				// TODO: More testing if this is even necessary
				// Try to find logo based on application logo value
				logoPath = FindLogoImagePath(installedLocationPath, manifestApplication.Logo, findStrategy);
				if (!string.IsNullOrWhiteSpace(logoPath) && File.Exists(logoPath))
				{
					return logoPath;
				}
			}

			// Try to find logo based on package logo value
			var mainPackageLogo = manifestInfo.GetPropertyStringValue("Logo");
			logoPath = FindLogoImagePath(installedLocationPath, mainPackageLogo, findStrategy);
			if (!string.IsNullOrWhiteSpace(logoPath) && File.Exists(logoPath))
			{
				return logoPath;
			}

			return "";
		}

		public static IEnumerable<PackageInfoEx> ToPackageInfoEx(
			IEnumerable<Windows.ApplicationModel.Package> packages,
			bool processLogo = true)
		{
			//var packageTest = packages.FirstOrDefault(p => p.Id.Name.ToLower().Contains("reader"));

			//            if (packageTest != null)
			//            {
			//                var debugOut = "Blah!";
			//            }

			foreach (var package in packages)
			{
				// We don't care about framework
				// packages, these packages are libraries
				// not apps
				if (package.IsFramework)
				{
					continue;
				}

				string installedLocationPath = null;

				try
				{
					installedLocationPath = package.InstalledLocation.Path;
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Look up of install location failed. " + package.Id.Name);
					continue;
				}

				var manifestPath = Path.Combine(installedLocationPath, "AppxManifest.xml");
				var manifestInfo = GetAppInfoFromManifest(manifestPath);

				if (manifestInfo.Apps != null)
				{
					// Configured to not display on start menu
					// so we skip theses
					var unlistedApps = manifestInfo.Apps.Where(p => p?.AppListEntry == "none").ToList();
					var listedApps = manifestInfo.Apps.Except(unlistedApps);

					foreach (var application in listedApps)
					{
						var packageInfoEx = new PackageInfoEx();

						var fullName = package.Id.FullName;

						var displayName = NativeApiHelper.LoadResourceString(fullName, application.DisplayName);

						// Can't get display name, probably not
						// an app we care about
						if (string.IsNullOrWhiteSpace(displayName))
						{
							Debug.WriteLine(manifestPath);
							continue;
						}

						packageInfoEx.DisplayName = displayName;

						var description = NativeApiHelper.LoadResourceString(fullName, application.Description);

						if (!string.IsNullOrWhiteSpace(description))
						{
							packageInfoEx.Description = description;
						}

						var logoPath = GetBestLogoPath(manifestInfo, application, installedLocationPath);
						packageInfoEx.FullLogoPath = logoPath;
						packageInfoEx.AppInfo = application;

						//                        package.Description = package.GetPropertyStringValue("Description");
						//                        package.DisplayName = package.GetPropertyStringValue("DisplayName");
						//                        package.Logo = package.GetPropertyStringValue("Logo");
						//                        package.PublisherDisplayName = package.GetPropertyStringValue("PublisherDisplayName");
						//                        package.IsFramework = package.GetPropertyBoolValue("Framework");

						packageInfoEx.FullName = fullName;
						packageInfoEx.Name = package.Id.Name;

						yield return packageInfoEx;
					}
				}
				else
				{
					Debug.WriteLine("Manifest has no apps defined: " + manifestPath);
				}
			}
		}
	}
}