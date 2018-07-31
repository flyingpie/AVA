namespace MUI
{
    public interface IInputSnapshot
    {
        bool IsKeyDown(Keys key);

        bool WasKeyDown(Keys key);

        void Update();
    }
}