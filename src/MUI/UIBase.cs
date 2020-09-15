using System.Threading.Tasks;

namespace MUI
{
	public abstract class UIBase
	{
		public int Width { get; set; }

		public int Height { get; set; }

		public virtual void Load()
		{
		}

		public virtual void Unload()
		{
		}

		public virtual Task Update()
		{
			return Task.CompletedTask;
		}

		public virtual void Draw()
		{
		}
	}
}