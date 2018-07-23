namespace MUI
{
    public struct WindowCreateInfo
    {
        public int X;
        public int Y;
        public int WindowWidth;
        public int WindowHeight;

        public WindowCreateInfo(
            int x,
            int y,
            int windowWidth,
            int windowHeight)
        {
            X = x;
            Y = y;
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
        }
    }
}