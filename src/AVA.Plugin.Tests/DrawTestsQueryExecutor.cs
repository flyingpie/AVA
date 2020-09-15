using AVA.Core.QueryExecutors.CommandQuery;
using ImGuiNET;
using MUI;
using MUI.DI;
using MUI.Glyphs;
using MUI.Graphics;
using System;
using System.Numerics;

namespace AVA.Plugin.Tests
{
	[Service]
	public class DrawTestsQueryExecutor : CommandQueryExecutor
	{
		public override string[] CommandPrefixes => new[] { "drawt" };

		private Image _imgTall;
		private Image _imgWide;
		private Image _imgSquareSmall;
		private Image _imgSquareLarge;

		[RunAfterInject]
		public void Init()
		{
			_imgTall = ResourceManager.Instance.LoadImage("Resources/Images/debug-tall.png");
			_imgWide = ResourceManager.Instance.LoadImage("Resources/Images/debug-wide.png");
			_imgSquareSmall = ResourceManager.Instance.LoadFontAwesomeIcon(FontAwesomeCS.FAIcon.GithubSquareBrands, 20);
			_imgSquareLarge = ResourceManager.Instance.LoadFontAwesomeIcon(FontAwesomeCS.FAIcon.GithubSquareBrands, 200);
		}

		public override void Draw()
		{
			ImGui.Text("Scale modes - wide image");
			ImGui.Columns(4, Guid.NewGuid().ToString(), false);

			{
				ImGui.Text("Center");
				_imgTall.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, Vector4.Zero, Margin.Zero, ScaleMode.Center);

				ImGui.NextColumn();

				ImGui.Text("Fill");
				_imgTall.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, Vector4.Zero, Margin.Zero, ScaleMode.Fill);

				ImGui.NextColumn();

				ImGui.Text("Fit");
				_imgTall.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, Vector4.Zero, Margin.Zero, ScaleMode.Fit);

				ImGui.NextColumn();

				ImGui.Text("Stretch");
				_imgTall.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, Vector4.Zero, Margin.Zero, ScaleMode.Stretch);
			}

			ImGui.Columns(1, "_", false);
			ImGui.Text("Scale modes - tall image");
			ImGui.Columns(4, Guid.NewGuid().ToString(), false);

			{
				ImGui.Text("Center");
				_imgWide.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, Vector4.Zero, Margin.Zero, ScaleMode.Center);

				ImGui.NextColumn();

				ImGui.Text("Fill");
				_imgWide.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, Vector4.Zero, Margin.Zero, ScaleMode.Fill);

				ImGui.NextColumn();

				ImGui.Text("Fit");
				_imgWide.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, Vector4.Zero, Margin.Zero, ScaleMode.Fit);

				ImGui.NextColumn();

				ImGui.Text("Stretch");
				_imgWide.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, Vector4.Zero, Margin.Zero, ScaleMode.Stretch);
			}

			ImGui.Columns(1, "_", false);
			ImGui.Text("Scale modes - square image");
			ImGui.Columns(4, Guid.NewGuid().ToString(), false);

			{
				ImGui.Text("Center");
				_imgSquareLarge.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, Vector4.Zero, Margin.Zero, ScaleMode.Center);

				ImGui.NextColumn();

				ImGui.Text("Fill");
				_imgSquareLarge.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, Vector4.Zero, Margin.Zero, ScaleMode.Fill);

				ImGui.NextColumn();

				ImGui.Text("Fit");
				_imgSquareLarge.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, Vector4.Zero, Margin.Zero, ScaleMode.Fit);

				ImGui.NextColumn();

				ImGui.Text("Stretch");
				_imgSquareLarge.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, Vector4.Zero, Margin.Zero, ScaleMode.Stretch);
			}

			ImGui.Columns(1, "_", false);
			ImGui.Text("Scale modes - animated image");
			ImGui.Columns(4, Guid.NewGuid().ToString(), false);

			{
				ImGui.Text("Center");
				ResourceManager.Instance.LoadingImage.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, new Vector4(0, 0, 0, 1), Margin.Zero, ScaleMode.Center);

				ImGui.NextColumn();

				ImGui.Text("Fill");
				ResourceManager.Instance.LoadingImage.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, new Vector4(0, 0, 0, 1), Margin.Zero, ScaleMode.Fill);

				ImGui.NextColumn();

				ImGui.Text("Fit");
				ResourceManager.Instance.LoadingImage.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, new Vector4(0, 0, 0, 1), Margin.Zero, ScaleMode.Fit);

				ImGui.NextColumn();

				ImGui.Text("Stretch");
				ResourceManager.Instance.LoadingImage.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, new Vector4(0, 0, 0, 1), Margin.Zero, ScaleMode.Stretch);
			}

			ImGui.Columns(1, "_", false);
			ImGui.Text("Padding");
			ImGui.Columns(4, Guid.NewGuid().ToString(), false);

			{
				ImGui.Text("Center");
				ResourceManager.Instance.LoadingImage.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, new Vector4(0, 0, 0, 1), new Margin(25), ScaleMode.Center);

				ImGui.NextColumn();

				ImGui.Text("Fill");
				ResourceManager.Instance.LoadingImage.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, new Vector4(0, 0, 0, 1), new Margin(25), ScaleMode.Fill);

				ImGui.NextColumn();

				ImGui.Text("Fit");
				ResourceManager.Instance.LoadingImage.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, new Vector4(0, 0, 0, 1), new Margin(25), ScaleMode.Fit);

				ImGui.NextColumn();

				ImGui.Text("Stretch");
				ResourceManager.Instance.LoadingImage.Draw(new Vector2(120, 120), Vector4.One, Vector4.One, new Vector4(0, 0, 0, 1), new Margin(25), ScaleMode.Stretch);
			}
		}
	}
}