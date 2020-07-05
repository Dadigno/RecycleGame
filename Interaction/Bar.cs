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
    public class Bar : Component
    {
        TextBox counter;
        TextBox title;
        Texture2D barTex;

        Texture2D infillTex1;
        Texture2D infillTex2;

        protected Rectangle rectBar;
        protected Rectangle rectInfill;
        protected Rectangle rectAvatar;
        protected Vector2 displayPos;
        double scale = 0.2;
        //private double avatarScale = 0.2;
        //Blink
        public bool blink = false;
        static float BLINK_TIME = 250;
        float timer = 0;
        int blinkCounter = 0;
        bool isBlinking = false;

        public Bar(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, String titlebar, Vector2 displayPos, Item.Type type) : base(_game, _graphics, _content)
        {
            this.displayPos = displayPos;
            infillTex1 = _content.Load<Texture2D>("bars/infillGreen");
            infillTex2 = _content.Load<Texture2D>("bars/infillRed");

            barTex = _content.Load<Texture2D>("bars/genericBar");
            double barWidth = barTex.Width * (scale * ConstVar.displayDim.X / barTex.Width);
            double barHeight = barWidth * barTex.Height / barTex.Width;
            rectBar = new Rectangle((int)displayPos.X, (int)displayPos.Y, (int)(barWidth), (int)barHeight);

            double infillWidth = infillTex1.Width * (0.93 * barWidth) / infillTex1.Width;
            double infillHeight = infillTex1.Height * (0.8 * barHeight) / infillTex1.Height;

            rectInfill = new Rectangle(rectBar.X + (int)(0.03 * barWidth), rectBar.Y + (int)(0.12 * barHeight), (int)(infillWidth), (int)(infillHeight));
            counter = new TextBox(_game, _graphics, _content, new Vector2(0, 0), new Vector2(rectInfill.X + rectInfill.Width / 2, rectInfill.Y), _content.Load<SpriteFont>("Fonts/barFont"), "0", Color.White);
            title = new TextBox(_game, _graphics, _content, new Vector2(0, 0), new Vector2(rectBar.X + (int)(rectBar.Width * 0.05), rectInfill.Y), _content.Load<SpriteFont>("Fonts/barFont"), titlebar, Color.White);
            
            Type = type;
        }

        public double Value   // property
        { get; set; }

        public double Target   // property
        { get; set; }

        public Item.Type Type   // property
        { get; set; }

        public void Draw()
        {
            ConstVar.sb.Draw(barTex, rectBar, null, Color.White, 0, new Vector2(0, 0), new SpriteEffects(), 0);
            if(!blink)
                ConstVar.sb.Draw(infillTex1, new Rectangle(rectInfill.X, rectInfill.Y, (int)(rectInfill.Width * Value), rectInfill.Height), Color.White);
            else
                ConstVar.sb.Draw(infillTex2, new Rectangle(rectInfill.X, rectInfill.Y, (int)(rectInfill.Width * Value), rectInfill.Height), Color.White);

            counter.Draw();
            title.Draw();
        }

        public void Update(GameTime gameTime, int v, int target)
        {
            Target = target;
            counter.update(v.ToString() + " / " + Target.ToString());
            if (v > Target)
            {
                Value = 1;
            }
            else
            {
                Value = v / Target;
            }

            if (isBlinking)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                timer -= elapsed;
                if (timer < 0)
                {
                    timer = BLINK_TIME;
                    if (blinkCounter < 4)
                    {
                        blink = !blink;
                        blinkCounter++;
                    }
                    else
                    {
                        blinkCounter = 0;
                        blink = false;
                        isBlinking = false;
                    }
                }
            }
        }

        public void Blink()
        {
            if (!isBlinking)
            {
                blink = true;
                isBlinking = true;
            }
        }


    }
}
