namespace MUI
{
	public struct Margin
	{
		public float Top;

		public float Bottom;

		public float Left;

		public float Right;

		public Margin(float top, float bottom, float left, float right)
		{
			Top = top;
			Bottom = bottom;
			Left = left;
			Right = right;
		}

		public Margin(float topBottom, float leftRight) : this(topBottom, topBottom, leftRight, leftRight)
		{
		}

		public Margin(float all) : this(all, all, all, all)
		{
		}

		public static readonly Margin Zero = new Margin();
	}
}