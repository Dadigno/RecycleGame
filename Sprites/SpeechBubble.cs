using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Gioco_generico
{
    public class SpeechBubble : Sprite
    {
        
        protected SpriteFont font;
        protected Color PenColour = Color.Black;
        protected Color colourTex = Color.White;
        private float SCROLL_VEL = 100;
        private float BUBBLE_DELAY = 2000;
        private float scrollingTime;
        private float bubbleTime;
        
        private Button continueButton;
        List<char> l;
        private string text = "";
        string temp = "";
        public bool isScrolling;
        public bool show = false;
        //Song song;
        private SoundEffect speechSound;
        SoundEffectInstance instance;
        public SpeechBubble(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, string font, double scale = 1) : base(_game, _graphics, _content, nameTex, new Vector2(0,0), new Vector2(0,0), scale)
        {
            this.font = _content.Load<SpriteFont>(font);
            origin = new Vector2(0, 0);
            scrollingTime = SCROLL_VEL;
            bubbleTime = BUBBLE_DELAY;

            //Load effect
            speechSound = _content.Load<SoundEffect>("soundEffect/speech-sound2");
            instance = speechSound.CreateInstance();
        }   

        public String Text
        {
            get { return text; }
            set
            {
                if (value != text)
                {
                    this.text = value;
                    l = text.ToList<char>();
                    temp = "";
                    show = true;
                }
               
            }
        }

        public void Update(GameTime gameTime, Vector2 pos)
        {
            setPos((int)(pos.X - rect.Width * 0.2), (int)(pos.Y - rect.Height * 1.2));
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            scrollingTime -= elapsed;
            if(isScrolling == false && temp != "")
            {   //significa che ha finito di scorrere un testo, adesso aspetto un tot di tempo prima di far sprire la vignetta
                bubbleTime -= elapsed;
                if ( bubbleTime < 0)
                {
                    bubbleTime = BUBBLE_DELAY;
                    show = false;
                }
            }
        }

        public new bool Draw()
        {
            if (text != "")
            {
                if (scrollingTime < 0)
                {
                    scrollingTime = SCROLL_VEL;
                    if (l.Count() > 0)
                    {
                        isScrolling = true;
                        if (instance.State != SoundState.Playing)
                        {
                            instance.Play();
                        }
                        temp += l[0];
                        l.RemoveAt(0);
                    }
                    else
                    {
                        isScrolling = false;
                        instance.Stop();
                    }
                }
                if (show)
                {
                    base.Draw(true);
                    ConstVar.sb.DrawString(font, temp, new Vector2((float)(rect.X + rect.Width * 0.05), (float)(rect.Y + rect.Height * 0.05)), PenColour);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;
        }
            
    }
}
