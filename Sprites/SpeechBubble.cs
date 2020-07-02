using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Recycle_game
{
    public class SpeechBubble : Sprite
    {
        
        protected SpriteFont font;
        protected Color PenColour = Color.Black;
        protected Color colourTex = Color.White;
        private float SCROLL_VEL = 50;

		private float BUBBLE_DELAY = 4000;
        private  float scrollingTime;
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
		float border;
        bool isStatic;
        public SpeechBubble(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, string font, float scale = 1) : base(_game, _graphics, _content, nameTex, new Vector2(0,0), new Vector2(0,0), scale) 
        {
            this.font = _content.Load<SpriteFont>(font);
            scrollingTime = SCROLL_VEL;
            bubbleTime = BUBBLE_DELAY;
            //Load effect
            speechSound = _content.Load<SoundEffect>("soundEffect/speech-sound");
            instance = speechSound.CreateInstance();
            border = rect.Width*0.9f;
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
            setPos((int)(pos.X + rect.Width * 0.5), (int)(pos.Y - rect.Height * 0.5));
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
        float row = 1;
        /// <summary>
        /// Mostra la nuvoletta con lo scorrimento del testo e il suono della voce
        /// </summary>
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
                        float a = font.MeasureString(temp.Replace("\r\n","")).X / border;
                        if ( a > row)
                        {
                            if (l[0] != ' ' && l[1] != ' ')
                            {
                                temp += "            ";
                            }
                            if(l[1] == ' ')
                            {
                                l.RemoveAt(1);
                            }

                            temp += Environment.NewLine;
                            row++;
                        }
                        l.RemoveAt(0);
                    }
                    else
                    {
                        isScrolling = false;
                        row = 1;
                        instance.Stop();
                    }
                }
                if (show)
                {
                    base.Draw(true);
                    ConstVar.sb.DrawString(font, temp, new Vector2(rect.X - texture.Width*0.45f, rect.Y-texture.Height*0.43f), PenColour);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Mostra la nuvoletta ma senza lo scorrimento del testo
        /// </summary>
        public void StaticDraw(string text)
        {
            Text = text + ' ';
            while (l.Count() > 0)
            {
                temp += l[0];
                float a = font.MeasureString(temp.Replace("\r\n", "")).X / border;
                if (a > row)
                {
                    if (l[0] != ' ' && l[1] != ' ')
                    {
                        temp += "           ";
                    }
                    if (l[1] == ' ')
                    {
                        l.RemoveAt(1);
                    }

                    temp += Environment.NewLine;
                    row++;
                }
                l.RemoveAt(0);
            }
            row = 1;
            if (show)
            {
                base.Draw(true);
                ConstVar.sb.DrawString(font, temp, new Vector2(rect.X - texture.Width * 0.45f, rect.Y - texture.Height * 0.43f), PenColour);
            }
        }
            
    }
}
