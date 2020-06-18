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
    public class Bar : Component
    {
        TextBox counter;
        TextBox title;
        Texture2D barTex;
        Texture2D infillTex;
        Texture2D avatarTex;
        
        protected Rectangle rectBar;
        protected Rectangle rectInfill;
        protected Rectangle rectAvatar;
        protected Vector2 displayPos;
        double scale = 0.2;
        //private double avatarScale = 0.2;


        public Bar(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, String titlebar, Vector2 displayPos, Item.Type type) : base(_game, _graphics, _content)
        {
            this.displayPos = displayPos;
            infillTex = _content.Load<Texture2D>("bars/infill");
            barTex = _content.Load<Texture2D>("bars/genericBar");
            double barWidth = barTex.Width * (scale * ConstVar.displayDim.X / barTex.Width);
            double barHeight = barWidth * barTex.Height / barTex.Width;
            rectBar = new Rectangle((int)displayPos.X, (int)displayPos.Y, (int)(barWidth), (int)barHeight);

            double infillWidth = infillTex.Width * (0.93 * barWidth) / infillTex.Width;
            double infillHeight = infillTex.Height * (0.8 * barHeight) / infillTex.Height;

            rectInfill = new Rectangle(rectBar.X + (int)(0.03 * barWidth), rectBar.Y + (int)(0.12 * barHeight), (int)(infillWidth), (int)(infillHeight));
            counter = new TextBox(_game, _graphics, _content, new Vector2(0, 0), new Vector2(rectInfill.X + rectInfill.Width / 2, rectInfill.Y), _content.Load<SpriteFont>("Fonts/barFont"), "0", Color.White);
            title = new TextBox(_game, _graphics, _content, new Vector2(0, 0), new Vector2(rectBar.X + (int)(rectBar.Width * 0.05), rectInfill.Y), _content.Load<SpriteFont>("Fonts/barFont"), titlebar, Color.White);
            
            Type = type;
            //avatarTex = _content.Load<Texture2D>("oggetti/plastic-bottle");
            //double avatarHeight = (int)(avatarTex.Height * ((rectBar.Width * avatarScale) / avatarTex.Width));
            //rectAvatar = new Rectangle(rectBar.X, rectBar.Y - (int)avatarHeight / 2 + rectBar.Height / 2 , (int)(rectBar.Width * avatarScale), (int)avatarHeight);
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
            ConstVar.sb.Draw(infillTex, new Rectangle(rectInfill.X, rectInfill.Y , (int)(rectInfill.Width * Value), rectInfill.Height), Color.White);
            counter.Draw();
            title.Draw();
            //ConstVar.sb.Draw(avatarTex, rectAvatar, Color.White);
        }
        public void Update(int v, int target)
        {
            //Target = _game.GameLevel.POINT_TARGET;
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
        }


    }
}
