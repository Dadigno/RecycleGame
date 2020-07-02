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
    public class AnimatedSprite : Sprite
    {
        public enum velocity { SLOW = 200, MEDIUM = 90, FAST = 20};
        public bool isRunning = false;
        protected int currentRow = 0;
        private int currentCol = 0;
        private int totalCol;
        private int totalFrames;
        protected float timerAnimated = 0;
        protected float TIMER;
        public AnimatedSprite(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, Vector2 origin, Vector2 initGamePos, int colums, int totFrames, int frameWidth, int frameHeight) : base(_game, _graphics, _content, nameTex, origin, initGamePos, frameWidth, frameHeight)
        {
            totalCol = colums;
            totalFrames = totFrames;
            TIMER = (float)velocity.MEDIUM;
        }

        protected void Draw(int row, int col)
        {
            ConstVar.sb.Draw(texture, rect, new Rectangle(col * rect.Width, row * rect.Height, rect.Width, rect.Height), Color.White, dir, origin, new SpriteEffects(), 0);
        }

        public void Draw()
        {
            ConstVar.sb.Draw(texture, rect, new Rectangle(currentCol * rect.Width, currentRow * rect.Height, rect.Width, rect.Height), Color.White, dir, new Vector2(rect.Width/2,rect.Height), new SpriteEffects(), 0);
            if (isRunning)
            {
                if (timerAnimated < 0)
                {
                    ++currentCol;
                    timerAnimated = TIMER;
                    if (currentCol >= totalCol)
                    {
                        currentCol = 0;
                        isRunning = false;
                    }
                }
            }
            else
            {
                currentCol = 0;
            }
         
        }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timerAnimated -= elapsed;
        }
    }
}
