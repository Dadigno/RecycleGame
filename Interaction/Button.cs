using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Gioco_generico
{
    public class Button
    {
        private MouseState _currentMouse;
        private MouseState _previousMouse;
        
        private bool _isPressed;
        //public bool _isActive { get; set; }
        public event EventHandler<ButtEventArgs> Action;
        //public EventHandler Click;
        Item.Type Type;
        public bool Enable { get; set; }
        private string name;
        Texture2D texture;
        Rectangle rect;
        Color colourTex = Color.White;
        Vector2 origin;
        public Button(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String texture, Vector2 initDisplayPos, Item.Type t = Item.Type.NONE, double scale = 1, bool state = true)
        {
            if(texture != "")
            {
                this.texture = _content.Load<Texture2D>(texture);
                rect = new Rectangle((int)initDisplayPos.X - (int)(this.texture.Width * scale) / 2, (int)initDisplayPos.Y - (int)(this.texture.Height * scale) / 2, (int)(this.texture.Width * scale), (int)(this.texture.Height * scale));
            }
            else
            {
                rect = new Rectangle((int)initDisplayPos.X, (int)initDisplayPos.Y, 1, 1);
            }
            origin = new Vector2(0, 0);
            Enable = state;
            name = texture;
            this.Type = t;
        }

        public new void Draw()
        {
            colourTex = Color.White;
            if (_isPressed || !Enable)
                colourTex = Color.Gray;
            ConstVar.sb.Draw(texture, rect, null, colourTex, 0, origin, new SpriteEffects(), 0);
        }

        public void update()
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            if (mouseRectangle.Intersects(rect) && _currentMouse.LeftButton == ButtonState.Pressed)
            {
                _isPressed = true;
            }
            else
            {
                _isPressed = false;
            }


            if (mouseRectangle.Intersects(rect) && _currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
            {
                if (Enable) {
                    ButtEventArgs e = new ButtEventArgs();
                    e.T = this.Type;
                    Action?.Invoke(this, e);
                }
            }
        }
        bool Intersects(Rectangle a, Rectangle b)
        {

            if (a.X < b.X + b.Width / 2 && a.X > b.X - b.Width / 2)
                if (a.Y < b.Y + b.Height / 2 && a.Y > b.Y - b.Height / 2)
                    return true;
            return false;
        }
    }

    public class ButtEventArgs : EventArgs
    {

        public Item.Type T { get; set; }
    }
}
