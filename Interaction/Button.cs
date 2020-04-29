using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

namespace RecycleGame
{
    public class Button : Banner
    {
        private MouseState _currentMouse;
        private MouseState _previousMouse;
        
        private bool _isPressed;
        public EventHandler Click;
        public Button(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String texture, Vector2 initDisplayPos, String font, String text, double scale = 1, bool state = true) : base(_game, _graphics, _content, texture, initDisplayPos, font, text, scale)
        {
            
        }

        public new void Draw()
        {

            if (_isPressed )
            {
                colourTex = Color.Gray;
            }
            else
            {
                colourTex = Color.White;
            }

            isVisible = true;
            base.Draw();
        }

        public void Update(TouchCollection touchcollection)
        {
            foreach (var touch in touchcollection)
            {

                Point t = new Point((int)touch.Position.X, (int)touch.Position.Y);
                Rectangle temp = new Rectangle(rect.X - (rect.Width / 2), rect.Y - (rect.Height / 2), rect.Width, rect.Height);
                if (temp.Contains(t))
                {
                    if (touch.State == TouchLocationState.Pressed)
                    {
                        _isPressed = true;
                        continue;
                    }
                    if (touch.State == TouchLocationState.Released)
                    {
                        _isPressed = false;
                        Click?.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        

    }
}
