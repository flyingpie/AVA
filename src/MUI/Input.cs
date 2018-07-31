namespace MUI
{
    public static class Input
    {
        public static IInputSnapshot InputSnapshot { get; set; } = new FNAInputSnapshot();

        public static bool IsKeyDown(Keys key)
        {
            return InputSnapshot.IsKeyDown(key);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return InputSnapshot.IsKeyDown(key) && !InputSnapshot.WasKeyDown(key);
        }
    }
}