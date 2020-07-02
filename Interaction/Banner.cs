using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Reflection;

namespace Recycle_game
{
    public class Banner : Component
    {
        protected SpriteFont font;
        protected Color PenColour;
        protected Color colourTex = Color.White;
        protected Texture2D texture;
        protected Vector2 displayPos;
        protected Rectangle rect;
        public bool isVisible = false;
        private float timer;
        public bool isActive = true;
        public String text { get; set; }
        public Banner(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String texture, Vector2 initDisplayPos, String font, String text,  double scale = 1) : base (_game, _graphics, _content)
        {
            this.text = text;
            this.texture = _content.Load<Texture2D>(texture);
            this.font = _content.Load<SpriteFont>(font);
            PenColour = Color.Black;
            displayPos = initDisplayPos;
            
        }

        public void Draw()
        {
            if (isVisible && isActive)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    rect = new Rectangle();
                    rect.Width = (int)(font.MeasureString(text).X * 2);
                    rect.Height = (int)(font.MeasureString(text).Y * 2);
                    rect.X = (int)displayPos.X - rect.Width / 2;
                    rect.Y = (int)displayPos.Y - rect.Height / 2;
                    var x = (rect.X + (rect.Width / 2)) - (font.MeasureString(text).X / 2);
                    var y = (rect.Y + (rect.Height / 2)) - (font.MeasureString(text).Y / 2);
                   
                    ConstVar.sb.Draw(texture, rect, colourTex);
                    ConstVar.sb.DrawString(font, text, new Vector2(x, y), PenColour);
                }
                else
                {
                    ConstVar.sb.Draw(texture, rect, colourTex);
                }
            }
        }
        
        public void update(GameTime gameTime)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (isVisible)
            {
                if (timer < 0)
                {
                    isVisible = false;
                    timer = ConstVar.TIMER_BANNER;
                }
            }
        }
    }
}
