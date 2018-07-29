namespace MUI
{
    public interface IInputSnapshot
    {
        bool IsKeyDown(Keys key);

        bool IsKeyDownOnce(Keys key);

        bool IsKeyPressed(Keys key);

        void Update();
    }
}