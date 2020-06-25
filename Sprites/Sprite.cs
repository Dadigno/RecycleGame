using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Gioco_generico
{
    public class Sprite : Component
    {
        protected Texture2D texture;
        protected Rectangle rect;
        protected float dir;
        protected float rotation;
        protected Vector2 origin;
        protected Vector2 gamePos;
        protected Vector2 displayPos;
        public String nameTex;
        public float scale;
        protected Color colourTex = Color.White;
        public Sprite(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, Vector2 initGamePos, Vector2 initDisplayPos, float scale = 1) : base(_game, _graphics, _content)
        {
            gamePos = initGamePos;
            displayPos = initDisplayPos;
            this.nameTex = nameTex;
            this.scale = scale;
            if (nameTex != "")
            {
                texture = _content.Load<Texture2D>(nameTex);
                rect = new Rectangle((int)initDisplayPos.X, (int)initDisplayPos.Y, (int)(this.texture.Width * scale), (int)(this.texture.Height * scale));
            }
            else
            {
                rect = new Rectangle((int)initDisplayPos.X, (int)initDisplayPos.Y, 0, 0);
            }
        }

        public Sprite(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, Vector2 origin, Vector2 initGamePos, int frameWidth, int frameHeight) : base(_game, _graphics, _content)
        {
            this.origin = origin;
            gamePos = initGamePos;
            texture = _content.Load<Texture2D>(nameTex);
            if (frameWidth!= 0 && frameHeight != 0)
            {
                rect = new Rectangle(0, 0, frameWidth, frameHeight);
            }
            else
            {
                rect = new Rectangle(0, 0, texture.Width, texture.Height);
            }
            rotation = 0;
        }

        public Sprite()
        {
            
        }
        public void Draw(bool show)
        {
            if (show)
            {
                ConstVar.sb.Draw(texture, new Vector2(getPos().X, getPos().Y), null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), new Vector2(scale, scale), new SpriteEffects(), 0);
                // ConstVar.sb.Draw(texture, rect, null, colourTex, rotation, new Vector2(0, 0), new SpriteEffects(), 0); 
            }
        }

        public Rectangle getRect()
        {
            return rect;
        }
        
        public Vector2 getOrigin()
        {
            return origin;
        }
        public Vector2 getPos()
        {
            return new Vector2(rect.X, rect.Y);
        }
        public void setPos(int X, int Y)
        {
            rect.X = X;
            rect.Y = Y;
        }
        public void stepPos(int X, int Y)
        {
            rect.X += X;
            rect.Y += Y;
        }
        public void setGamePos(int X, int Y)
        {
            gamePos.X = X;
            gamePos.Y = Y;
        }
        public void stepGamePos(int X, int Y)
        {
            gamePos.X += X;
            gamePos.Y += Y;
        }
        public Vector2 getGamePos()
        {
            return gamePos;
        }
        public void setTilePos(int X, int Y, Background background)
        {

            rect.X = (int)ConstVar.displayDim.X / 2;
            rect.Y = (int)ConstVar.displayDim.Y / 2;

            gamePos.X = (int)(background.tileDim * X + background.tileDim / 2);
            gamePos.Y = (int)(background.tileDim * Y + background.tileDim / 2);
            if (-(gamePos.X - rect.X) > 0)
            {
                rect.X = (int)ConstVar.displayDim.X / 2 - (int)(-(gamePos.X - rect.X));
            }

            if ((gamePos.X - rect.X) > background.dim.X - ConstVar.displayDim.X)
            {
                rect.X = (int)(ConstVar.displayDim.X / 2 +  ConstVar.displayDim.X - ( background.dim.X - ( gamePos.X - rect.X)));
            }

            if (-(gamePos.Y - rect.Y) > 0)
            {
                rect.Y = (int)ConstVar.displayDim.Y / 2 - (int)(-(gamePos.Y - rect.Y));
            }

            if ((gamePos.Y - rect.Y) > background.dim.Y - ConstVar.displayDim.Y)
            {
                rect.Y = (int)(ConstVar.displayDim.Y / 2 + ConstVar.displayDim.Y - (background.dim.Y - (gamePos.Y - rect.Y)));
            }

        }
        public Vector2 getTilePos(Background background)
        {
            int x = (int)(getGamePos().X / background.tileDim);
            int y = (int)(getGamePos().Y / background.tileDim);
            return new Vector2(x,y);
        }
    }
}
