using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Gioco_generico
{
    public class Button : Banner
    {
        private MouseState _currentMouse;
        private MouseState _previousMouse;
        
        private bool _isPressed;
        public bool _isActive { get; set; }
        public EventHandler Click;
        public Button(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String texture, Vector2 initDisplayPos, String font, String text, double scale = 1, bool state = true) : base(_game, _graphics, _content, texture, initDisplayPos, font, text, scale)
        {
            _isActive = state;
        }

        public new void Draw()
        {
            colourTex = Color.White;
            if (_isPressed || !_isActive)
                colourTex = Color.Gray;

            isVisible = true;
            base.Draw();
        }

        public void update()
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            _isPressed = false;

            if (mouseRectangle.Intersects(rect) && _currentMouse.LeftButton == ButtonState.Pressed)
            {
                _isPressed = true;
            }
            if (mouseRectangle.Intersects(rect) && _currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
            {
                if (_isActive)
                    Click?.Invoke(this, new EventArgs());
            }
        }

        

    }
}
