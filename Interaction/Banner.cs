using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RecycleGame
{
    public class Banner : Component
    {
        protected SpriteFont font;
        protected Color PenColour;
        protected Color colourTex = Color.White;
        protected Texture2D texture;
        protected Vector2 displayPos;
        protected Vector2 origin;
        protected Rectangle rect;
        public bool isVisible = false;
        private float timer;
        public String text { get; set; }
        public Banner(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String texture, Vector2 initDisplayPos, String font, String text,  double scale = 1) : base (_game, _graphics, _content)
        {
            this.text = text;
            this.texture = _content.Load<Texture2D>(texture);
            this.font = _content.Load<SpriteFont>(font);
            PenColour = Color.Black;
            displayPos = initDisplayPos;
            rect = new Rectangle((int)initDisplayPos.X, (int)initDisplayPos.Y, (int)(this.texture.Width * (ConstVar.displayDim.X * scale / this.texture.Width)), (int)(this.texture.Height * (ConstVar.displayDim.X * scale / this.texture.Width)));
            origin = new Vector2(this.texture.Width / 2, this.texture.Height / 2);
        }

        public void Draw()
        {
            if (isVisible)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    rect.Width = (int)(font.MeasureString(text).X * 2);
                    rect.Height = (int)(font.MeasureString(text).Y * 2);
                    var x = (rect.X + (rect.Width / 2)) - (font.MeasureString(text).X / 2);
                    var y = (rect.Y + (rect.Height / 2)) - (font.MeasureString(text).Y / 2);
                    ConstVar.sb.Draw(texture, rect, null, colourTex, 0, origin, new SpriteEffects(), 0);
                    ConstVar.sb.DrawString(font, text, new Vector2(x, y), PenColour);
                    
                }
                else
                {
                    ConstVar.sb.Draw(texture, rect, null, colourTex, 0, origin, new SpriteEffects(), 0);
                }
            }
        }
        
        /*public void Update(GameTime gameTime)
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
        }*/
    }
}
