using ImGuiNET.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MUI.Win32;
using MUI.Win32.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUI;

public class UIContext : Game
{
	public event EventHandler FocusGained = (sender, e) => { };

	public event EventHandler FocusLost = (sender, e) => { };

	private readonly GraphicsDeviceManager _graphics;
	private readonly ImGuiRenderer _imGuiRenderer;

	private Stack<UIBase> _uis = new();

	private readonly Form _form;

	private Task? _updateTask;

	public UIContext(int width, int height)
	{
		Instance = this;

		_graphics = new GraphicsDeviceManager(this)
		{
			PreferredBackBufferWidth = width,
			PreferredBackBufferHeight = height,
			PreferMultiSampling = true,
		};

		_imGuiRenderer = new ImGuiRenderer(this);

		IsMouseVisible = true;
		Window.IsBorderless = true;

		_form = Control.FromHandle(Window.Handle)?.FindForm()
			?? throw new InvalidOperationException("Parent form not found");
	}

	public int FrameNumber { get; private set; }

	public static UIContext Instance { get; private set; }

	public ResourceManager ResourceManager { get; set; }

	public SpriteBatch SpriteBatch { get; private set; }

	public bool IsVisible
	{
		get; set;
		//get => _form.Visible;
		//set => _form.InvokeIfRequired(() => _form.Visible = value);
	}

	public float Opacity
	{
		set
		{
			var old = PInvoke.GetWindowLong(_form.Handle, PInvoke.GWL_EX_STYLE);
			PInvoke.SetWindowLong(_form.Handle, PInvoke.GWL_EX_STYLE, old | PInvoke.WS_EX_LAYERED);

			PInvoke.SetLayeredWindowAttributes(_form.Handle, 0, (byte)Math.Ceiling(255f * value), PInvoke.LWA_ALPHA);
		}
	}

	private bool _wasActive;

	public void CenterWindowToDisplayWithMouse()
	{
		var screen = Screen.AllScreens.FirstOrDefault(s => s.Bounds.Contains(Cursor.Position));

		if (screen == null)
		{
			return;
		}

		var x = screen.Bounds.X + (screen.Bounds.Width / 2) - (_form.Size.Width / 2);
		var y = screen.Bounds.Y + (screen.Bounds.Height / 2) - (_form.Size.Height / 2);

		_form.InvokeIfRequired(() =>
		{
			_form.Location = new System.Drawing.Point(x, y);
		});
	}

	public void Focus()
	{
		_form.InvokeIfRequired(() =>
		{
			PInvoke.SetForegroundWindow(Window.Handle);
		});
	}

	public void HideFromTaskbar(bool hide)
	{
		if (hide)
		{
			PInvoke.SetWindowLong(Window.Handle, PInvoke.GWL_EX_STYLE, (PInvoke.GetWindowLong(Window.Handle, PInvoke.GWL_EX_STYLE) | PInvoke.WS_EX_TOOLWINDOW) & ~PInvoke.WS_EX_APPWINDOW);
		}
		else
		{
			PInvoke.SetWindowLong(Window.Handle, PInvoke.GWL_EX_STYLE, (PInvoke.GetWindowLong(Window.Handle, PInvoke.GWL_EX_STYLE) | PInvoke.WS_EX_TOOLWINDOW) & PInvoke.WS_EX_APPWINDOW);
		}
	}

	public void Resize(int width, int height)
	{
		if (_graphics.PreferredBackBufferHeight == width && _graphics.PreferredBackBufferHeight == height)
		{
			return;
		}

		_graphics.PreferredBackBufferWidth = width;
		_graphics.PreferredBackBufferHeight = height;
		_graphics.ApplyChanges();

		CenterWindowToDisplayWithMouse();
	}

	public void PushUI(UIBase ui)
	{
		Resize(ui.Width, ui.Height);

		_uis.Push(ui);

		ui.Load();
	}

	public UIBase PopUI()
	{
		var ui = _uis.Pop();

		Resize(_uis.Peek().Width, _uis.Peek().Height);

		return ui;
	}

	protected override void Initialize()
	{
		base.Initialize();

		SpriteBatch = new SpriteBatch(GraphicsDevice);

		ResourceManager = new ResourceManager(GraphicsDevice, _imGuiRenderer);
		
		ResourceManager.Init();

		_imGuiRenderer.Initialize(GraphicsDevice);

		Fonts.Load(ResourceManager);
	}

	protected override void LoadContent()
	{
		if (_uis.Any())
		{
			_uis.Peek().Load();
		}

		base.LoadContent();
	}

	protected override void UnloadContent()
	{
		if (_uis.Any())
		{
			_uis.Peek().Unload();
		}

		base.UnloadContent();
	}

	protected override void Update(GameTime gameTime)
	{
		FrameNumber++;

		if (IsActive && !_wasActive)
		{
			FocusGained(this, EventArgs.Empty);
		}

		if (!IsActive && _wasActive)
		{
			FocusLost(this, EventArgs.Empty);
		}

		if (!IsActive || !IsVisible)
		{
			SuppressDraw();
		}

		Input.InputSnapshot.Update();

		_wasActive = IsActive;

		if (_updateTask?.IsCompleted ?? false)
		{
			_updateTask = null;
		}

		if (_uis.Any())
		{
			_uis.Peek().Update();
		}

		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.CornflowerBlue);

		SpriteBatch.Begin();

		_imGuiRenderer.BeforeLayout(gameTime);

		if (_uis.Any())
			_uis.Peek().Draw();

		SpriteBatch.End();

		_imGuiRenderer.AfterLayout();

		base.Draw(gameTime);
	}

	private Microsoft.Xna.Framework.Point _lastPosition;

	public void Maximize()
	{
		IsVisible = true;

		_form.InvokeIfRequired(() =>
		{
			Window.Position = _lastPosition;
		});

		Focus();
	}

	public void Minimize()
	{
		IsVisible = false;

		_form.InvokeIfRequired(() =>
		{
			_lastPosition = Window.Position;
			Window.Position = new Point(0, -4000);
		});
	}
}