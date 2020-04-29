using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RecycleGame
{
    public class JoyStick : Component
    {
        private enum Mode { SX, DX };
        Mode mode = Mode.SX;
        Texture2D joytex;
        Texture2D padtex;
        double scale = 0.2;
        Vector2 pos;
        Vector2 dimjoy;
        Vector2 dimpad;
        Rectangle rectjoy;
        Rectangle rectpad;
        Vector2 originjoy;
        Vector2 originpad;
        TouchCollection oldtouchCollection;
        private Vector2 value;
        Button A_button;
        Button B_button;
        
        public Vector2 Value { get { return value; } }
        bool active = false;

        public JoyStick(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content) : base(_game, _graphics, _content)
        {
            joytex = _content.Load<Texture2D>("joy");
            padtex = _content.Load<Texture2D>("pad");
            pos = new Vector2(ConstVar.displayDim.X * 0.1f, ConstVar.displayDim.Y - ConstVar.displayDim.X * 0.1f);
            rectjoy = new Rectangle((int)pos.X, (int)pos.Y, (int)(joytex.Width * scale), (int)(joytex.Height * scale));
            rectpad = new Rectangle((int)pos.X, (int)pos.Y, (int)(padtex.Width * scale), (int)(padtex.Height * scale));
            originjoy = new Vector2(joytex.Width / 2, joytex.Height / 2);
            originpad = new Vector2(padtex.Width / 2, padtex.Height / 2);

            A_button = new Button(_game, _graphics, _content, "menu-btn", new Vector2(ConstVar.displayDim.X - ConstVar.displayDim.X * 0.05f, 10), "Fonts/font", "", 0.05);
            A_button.Click += Click_A;
            B_button = new Button(_game, _graphics, _content, "help-btn", new Vector2(ConstVar.displayDim.X - ConstVar.displayDim.X * 0.05f, 10), "Fonts/font", "", 0.05);
            B_button.Click += Click_B;
        }

        public void Draw()
        {
            ConstVar.sb.Draw(padtex, rectpad, null, Color.White, 0, originpad, new SpriteEffects(), 0);
            ConstVar.sb.Draw(joytex, rectjoy, null, Color.White, 0, originjoy, new SpriteEffects(), 0);
        }
        int id = 0;
        public Vector2 Update(TouchCollection touchCollection, GameTime gametime)
        {
            if (touchCollection.Count > 0)
            {
                
                foreach ( var touch in touchCollection)
                {
                    Point t = new Point((int)touch.Position.X, (int)touch.Position.Y);
                    Rectangle temp = new Rectangle(rectjoy.X - (rectjoy.Width / 2), rectjoy.Y - (rectjoy.Height / 2), rectjoy.Width, rectjoy.Height);
                    if (temp.Contains(t))
                    {
                        if (touch.State == TouchLocationState.Pressed) 
                        { 
                            active = true;
                            id = touch.Id;
                        }
                        else if(touch.State == TouchLocationState.Released)
                        {
                            active = false;
                            rectjoy.X = rectpad.X;
                            rectjoy.Y = rectpad.Y;
                        }
                    }
                }
                if (active)
                {
                    try
                    {
                        rectjoy.X = (int)touchCollection.Where(t => t.Id == id).First().Position.X;
                        rectjoy.Y = (int)touchCollection.Where(t => t.Id == id).First().Position.Y;
                    }
                    catch { }
                }
            }
            else
            {
                active = false;
                rectjoy.X = rectpad.X;
                rectjoy.Y = rectpad.Y;
            }

            //calcolo il valore
            Vector2 a = new Vector2(rectjoy.X, rectjoy.Y);
            Vector2 b = new Vector2(rectpad.X, rectpad.Y);
            return value = (a - b);
        }


        private void Click_A(Object sender, EventArgs e)
        {

        }
        private void Click_B(Object sender, EventArgs e)
        {

        }
    }
}