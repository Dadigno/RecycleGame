using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Recycle_game
{
    public class TextBox : Sprite
    {
        private String text;
        private SpriteFont font;
        private Color color;

        public TextBox(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, Vector2 initGamePosition, Vector2 initDisplayPos, SpriteFont font, String initText, Color color) : base(_game, _graphics, _content, "", initGamePosition, initDisplayPos, 1)
        {
            this.text = initText;
            this.font = font;
            this.color = color;
        }

        protected void setText(String text)
        {
            this.text = text;
        }
        public void update(String text)
        {
            setText(text);
        }

        public void Draw()
        {
            ConstVar.sb.DrawString(font, text, new Vector2(rect.X, rect.Y), color);
        }
    }
}
