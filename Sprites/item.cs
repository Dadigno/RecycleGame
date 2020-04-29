using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Gioco_generico
{
   

    public class Item : Sprite
    {
        public enum Type { PLASTICA, UMIDO, SECCO, VETRO, CARTA, SPECIALE, NONE};
        public Type type;
        double[] sine = {0.1, 0.1, 0.1, 0.2, 0.3, 0.3, 0.4, 0.5, 0.6, 0.7, 0.7, 0.8, 0.9, 0.9, 0.9, 1, 1, 1, 1, 1, 0.9, 0.9, 0.9, 0.8, 0.7, 0.7, 0.6, 0.5, 0.4, 0.3, 0.3, 0.2, 0.1, 0.1, 0.1 };
        private float timerAnimated = 100;
        private bool floating = false;
        private int offset = 10;
        public Item(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, Vector2 initGamePosition, Vector2 initDisplayPos, double scale, Type type = Type.NONE, bool animation = false) : base(_game, _graphics, _content, nameTex, initGamePosition, initDisplayPos, scale)
        {
            this.type = type;
            this.floating = animation;
        }

        public Item Clone()
        {
            return new Item(this._game, this._graphics, this._content, this.nameTex, new Vector2(0, 0), this.displayPos, (double)this.scale, this.type, this.floating);
        }

        int i = 0;
        double offy = 0;
        public void Update(GameTime gameTime, Background background)
        {
            if (floating)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                timerAnimated -= elapsed;
                if (timerAnimated < 0)
                {
                    timerAnimated = 50;
                    i = (i + 1) % sine.Length;
                    offy = sine[i] * offset;
                }
            }
            else
            {
                offy = 0;
            }
            rect.X = background.getRect().X + (int)gamePos.X;
            rect.Y = background.getRect().Y + (int)gamePos.Y;
        }

        public new void Draw()
        {
            if (floating)
            {
                Rectangle t = new Rectangle(rect.X, rect.Y - (int)offy, rect.Width, rect.Height);
                ConstVar.sb.Draw(texture, t, null, Color.White, rotation, origin, new SpriteEffects(), 0);
            }
            else
            {
                base.Draw(true);
            }
        }

        
    }
}
