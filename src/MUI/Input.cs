namespace MUI
{
    public static class Input
    {
        public static IInputSnapshot InputSnapshot { get; set; } = new FNAInputSnapshot();

        public static bool IsAltDown => InputSnapshot.IsKeyDown(Keys.LeftAlt) || InputSnapshot.IsKeyDown(Keys.RightAlt);

        public static bool IsControlDown => InputSnapshot.IsKeyDown(Keys.LeftControl) || InputSnapshot.IsKeyDown(Keys.RightControl);

        public static bool IsShiftDown => InputSnapshot.IsKeyDown(Keys.LeftShift) || InputSnapshot.IsKeyDown(Keys.RightShift);

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