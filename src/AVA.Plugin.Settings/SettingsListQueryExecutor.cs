﻿using AVA.Core;
using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using AVA.Core.Settings;
using AVA.Plugin.Indexer;
using FontAwesomeCS;
using MUI;
using MUI.DI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AVA.Plugin.Settings
{
	[Help(Name = "Settings", Description = "Shows the settings set", ExampleUsage = "ava", Icon = FAIcon.VenusSolid)]
	public class SettingsListQueryExecutor : ListQueryExecutor
	{
		[Dependency] public IIndexer Indexer { get; set; }

		public override int Order => 0;

		public override string Prefix => "ava";

		public override IEnumerable<IListQueryResult> GetQueryResults(string term)
		{
			return new[]
			{
				CreateRebuildIndexSetting(),
				new ListQueryResult()
				{
					Name = "Open settings file",
					Description = SettingsRoot.Instance.Path,
					Icon = FAIcon.FileCodeRegular,
					OnExecute = q =>
					{
						var path = SettingsRoot.Instance.Path;
						var dir = System.IO.Path.GetDirectoryName(path);

						Process.Start(new ProcessStartInfo()
						{
							FileName = path,
							UseShellExecute = true,
							WorkingDirectory = dir,
						})?.Dispose();
					},
				},
				new ListQueryResult()
				{
					Name = "Open settings",
					Icon = FAIcon.CogSolid,
					OnExecute = qc =>
					{
						qc.HideUI = false;

						UIContext.Instance.PushUI(new SettingsUI());
					}
				}
			};
		}

		private ListQueryResult CreateRebuildIndexSetting()
		{
			var res = new ListQueryResult()
			{
				Name = "Rebuild index",
				Description = "",
				Icon = ResourceManager.Instance.DefaultImage
			};

			res.OnExecuteAsync = async query =>
			{
				res.Description = "Rebuilding index...";
				res.Icon = ResourceManager.Instance.LoadingImage;

				var progress = new IndexerProgress();
				var cts = new CancellationTokenSource();
				var updater = Task.Run(async () =>
				{
					while (!cts.IsCancellationRequested)
					{
						await Task.Delay(100);

						if (!progress.HasStarted) continue;

						res.Description = $"Rebuilding index: {progress.CurrentIndexerName} {progress.ProcessedIndexedItemsPercentage}%% ({progress.ProcessedIndexedItems}/{progress.TotalIndexedItems})";
					}
				});

				await Indexer.RebuildAsync(progress);

				cts.Cancel();

				await updater;

				res.Icon = ResourceManager.Instance.DefaultImage;
				res.Description = "";

				query.HideUI = false;
				query.ResetText = false;
			};

			return res;
		}
	}
}