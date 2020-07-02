using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Recycle_game
{
   //Ogni item rappresenta un oggetto da differenziare

    public class Item : Sprite
    {
        public enum Type { PLASTICA_MET, ORGANICO, SECCO, VETRO, CARTA, FARMACI, ABITI, OLIOSPECIFICO, BATTERIE, TONER, CENTRORACCOLTA, NONE };
        public Type type;
        double[] sine = {0.1, 0.1, 0.1, 0.2, 0.3, 0.3, 0.4, 0.5, 0.6, 0.7, 0.7, 0.8, 0.9, 0.9, 0.9, 1, 1, 1, 1, 1, 0.9, 0.9, 0.9, 0.8, 0.7, 0.7, 0.6, 0.5, 0.4, 0.3, 0.3, 0.2, 0.1, 0.1, 0.1, -0.1, -0.1, -0.1, -0.2, -0.3, -0.3, -0.4, -0.5, -0.6, -0.7, -0.7, -0.8, -0.9, -0.9, -0.9, -1, -1, -1, -1, -1, -0.9, -0.9, -0.9, -0.8, -0.7, -0.7, -0.6, -0.5, -0.4, -0.3, -0.3, -0.2, -0.1, -0.1, -0.1 };
        
        private float timerAnimated = 100;
        private bool floating = false;
        private int offset = 5;
        String objCardPath;
        public Item(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, Vector2 initGamePosition, Vector2 initDisplayPos, float scale, Type type = Type.NONE, bool animation = false) : base(_game, _graphics, _content, nameTex, initGamePosition, initDisplayPos, scale)
        {
            this.type = type;
            this.floating = animation;
            this.objCardPath = nameTex + "_card";
        }

        public Item Clone()
        {
            return new Item(this._game, this._graphics, this._content, this.nameTex, new Vector2(0, 0), this.displayPos, this.scale, this.type, this.floating);
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
                setPos((int)getPos().X, (int)(getPos().Y + offy));
                
                //ConstVar.sb.Draw(texture, new Vector2(getPos().X,getPos().Y + (int)offy), null, Color.White, 0, new Vector2(texture.Width/2, texture.Height/2), new Vector2(0.5f, 0.5f), new SpriteEffects(), 0);
            }
            base.Draw(true);
        }

        public void DrawCard(float scale_x, float scale_y)
        {
            Texture2D tex = _content.Load<Texture2D>(objCardPath);
            Rectangle r = new Rectangle(((int)(ConstVar.chooseBucket.window.background.Width * ConstVar.chooseBucket.window.scaleY * scale_x) + (int)(ConstVar.chooseBucket.window.background.Width * ConstVar.chooseBucket.window.scaleX / 2)), ((int)(ConstVar.chooseBucket.window.background.Height * ConstVar.chooseBucket.window.scaleY * scale_y) + (int)(ConstVar.chooseBucket.window.background.Height * ConstVar.chooseBucket.window.scaleX / 2)), (int)(tex.Width * ConstVar.chooseBucket.window.scaleY), (int)(tex.Height * ConstVar.chooseBucket.window.scaleY));
            ConstVar.sb.Draw(tex,r,Color.White);
        }

        
    }
}
