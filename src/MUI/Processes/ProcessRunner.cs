using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MUI;

public enum ProcessOutputType
{
	Output,
	Error,
}

public class ProcessStandardOutput
{
	public string? Line { get; set; }
}

public class ProcessStandardError
{
	public string? Line { get; set; }
}

public sealed class ProcessRunner : IDisposable
{
	//public static ProcessRunner Start(string fileName, string? arguments = null, bool runAsAdmin = false)
	//{
	//	arguments ??= string.Empty;

	//}

	private readonly Process _process;

	public ProcessRunner(Process process)
	{
		_process = process ?? throw new ArgumentNullException(nameof(process));
	}

	public Action<ProcessStandardError>? OnStandardError { get; set; }

	public Action<ProcessStandardOutput>? OnStandardOutput { get; set; }

	public ProcessRunner(
		string fileName,
		string? arguments = null,
		string? workingDirectory = null,
		IEnumerable<(string Key, string Value)>? environmentVariables = null,
		bool runAsAdmin = false)
	{
		arguments ??= string.Empty;
		environmentVariables ??= Enumerable.Empty<(string Key, string Value)>();
		workingDirectory ??= string.Empty;

		_process = new Process();
		_process.EnableRaisingEvents = true;

		_process.ErrorDataReceived += (s, a) =>
		{
			var dbg = 2;
		};

		_process.ErrorDataReceived += (s, a) =>
		{
			var dbg = 2;
		};

		_process.Exited += (s, a) =>
		{
			var dbg = 2;
		};

		_process.Disposed += (s, a) =>
		{
			var dbg = 2;
		};

		_process.StartInfo.FileName = fileName.ExpandEnvVars();
		_process.StartInfo.Arguments = arguments.ExpandEnvVars();
		_process.StartInfo.WorkingDirectory = workingDirectory.ExpandEnvVars();
		//_process.StartInfo.UseShellExecute = false;
		_process.StartInfo.UseShellExecute = true; // Must be true, otherwise we can't use "RunAs". Though this also excludes the use of redirect output streams.
		_process.StartInfo.CreateNoWindow = true;

		//_process.StartInfo.RedirectStandardOutput = true;
		//proc.StartInfo.RedirectStandardInput = true;
		//_process.StartInfo.RedirectStandardError = true;

		//_ = Task.Run(async () =>
		//{
		//	while (!_process.StandardOutput.EndOfStream)
		//	{
		//		var line = await _process.StandardOutput.ReadLineAsync().ConfigureAwait(false);

		//		OnStandardOutput?.Invoke(new ProcessStandardOutput()
		//		{
		//			Line = line,
		//		});
		//	}
		//});

		//_ = Task.Run(async () =>
		//{
		//	while (!_process.StandardError.EndOfStream)
		//	{
		//		var line = await _process.StandardError.ReadLineAsync().ConfigureAwait(false);

		//		OnStandardError?.Invoke(new ProcessStandardError()
		//		{
		//			Line = line,
		//		});
		//	}
		//});

		

		// Environment variables
		foreach (var envVar in environmentVariables)
		{
			_process.StartInfo.Environment[envVar.Key] = envVar.Value.ExpandEnvVars();
		}

		if (runAsAdmin)
		{
			_process.StartInfo.Verb = "runas";
		}

		//proc.Dispose();
	}

	public void Dispose()
	{
		_process.Dispose();
	}

	public void Start()
	{
		try
		{
			_process.Start();

			//_ = Task.Run(async () =>
			//{
			//	try
			//	{
			//		await _process.WaitForExitAsync();

			//		var dbg = 2;
			//	}
			//	catch (Exception ex)
			//	{
			//		var dbg2 = 2;
			//	}
			//});
		}
		catch (Exception ex)
		{
			//log.Error($"Could not start process '{_process.StartInfo.FileName}': {ex.Message}");
			//return true;
		}
	}
}