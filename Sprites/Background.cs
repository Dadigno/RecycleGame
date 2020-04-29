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
    public class Background : Component
    {
        protected Texture2D texture;
        private Rectangle rect;
        protected Vector2 origin;
        public Vector2 dim;
       // double ratioX = 0.625;
        //double ratioY = 0.351;
        public double tileDim = 32;
        //private Vector2 tileArea = new Vector2(80, 80);
        public Background(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex) : base(_game, _graphics, _content)
        {
            texture = _content.Load<Texture2D>(nameTex);
            double backWidth = texture.Width * ConstVar.displayDim.X / 1600;
            double backHeight = texture.Height * ConstVar.displayDim.Y / 900;
            tileDim = (backWidth / 80);
            dim = new Vector2((float)backWidth, (float)backHeight);
            rect = new Rectangle(0, 0, (int)dim.X, (int)dim.Y);
        }

        public Rectangle getRect()
        {
            return rect;
        }
        public void Draw()
        {
            ConstVar.sb.Draw(texture, rect, null, Color.White, 0, new Vector2(0, 0), new SpriteEffects(), 0);
        }

        public void update(Character mainChar)
        {
            rect.X = -((int)mainChar.getGamePos().X - (int)mainChar.getPos().X);
            rect.Y = -((int)mainChar.getGamePos().Y - (int)mainChar.getPos().Y);
        }
    }
}
