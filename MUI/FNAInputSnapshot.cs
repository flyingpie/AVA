using Microsoft.Xna.Framework.Input;

namespace MUI
{
    public class FNAInputSnapshot : IInputSnapshot
    {
        private KeyboardState _lastKeyboardState;
        private KeyboardState _currentKeyboardState;

        private MouseState _lastMouseState;
        private MouseState _currentMouseState;

        public bool IsKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown((Microsoft.Xna.Framework.Input.Keys)key);
        }

        public bool IsKeyDownOnce(Keys key)
        {
            var xnaKey = (Microsoft.Xna.Framework.Input.Keys)key;

            return _currentKeyboardState.IsKeyDown(xnaKey) && !_lastKeyboardState.IsKeyDown(xnaKey);
        }

        public bool IsKeyPressed(Keys key)
        {
            var xnaKey = (Microsoft.Xna.Framework.Input.Keys)key;

            return !_currentKeyboardState.IsKeyDown(xnaKey) && _lastKeyboardState.IsKeyDown(xnaKey);
        }

        public void Update()
        {
            _lastKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();

            _lastMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
        }
    }
}