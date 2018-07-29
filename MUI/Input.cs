namespace MUI
{
    public static class Input
    {
        public static IInputSnapshot InputSnapshot { get; set; } = new FNAInputSnapshot();

        public static bool IsKeyDown(Keys key) => InputSnapshot.IsKeyDown(key);

        public static bool IsKeyDownOnce(Keys key) => InputSnapshot.IsKeyDownOnce(key);

        public static bool IsKeyPressed(Keys key) => InputSnapshot.IsKeyPressed(key);
    }
}