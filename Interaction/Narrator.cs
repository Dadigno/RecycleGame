using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Recycle_game
{
    public class Narrator : Sprite
    {
        SpeechBubble speechBubble;
        bool show = false;
        List<String> messages;
        bool isSpeaking;
		bool isSleeping = true;
        delegate void doSomething();
        doSomething handler;
        /// <summary> Se True il narratore non sta facendo nulla</summary>
        public bool Sleeping { get; }
        public Narrator(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, Vector2 initGamePos, Vector2 initDisplayPos, float scale = 1) : base(_game, _graphics, _content, nameTex, initGamePos, initDisplayPos, scale)
        {            
			speechBubble = new SpeechBubble(_game, _graphics, _content, "bubble/bubble-narrator", "Fonts/speechFont");
			//speechBubble.Text = "\nCiao sono il\ntuo narratore";
            //speechBubble = new SpeechBubble(_game, _graphics, _content, "bubble/bubble-narrator", "Fonts/font");
        }

        public void Draw()
        {
            Draw(isSpeaking);
            speechBubble.Draw();
            if (isSpeaking)
            {
                isSpeaking = speechBubble.Draw();
            }
            handler?.Invoke();
        }

        public void Update(GameTime gameTime)
        {
            speechBubble.Update(gameTime, getPos());
        }

        /// <summary>Se isSleeping è true pronuncia il testo passato nell'argomento </summary>
        void speak(string text)
        {
            if (isSleeping)
            {
                speechBubble.Text = text;
                isSpeaking = true;
            }
        }

        int step = 0;

        
        void doTutorial()
        {
            if (!isSpeaking)
            {
                speak(ConstVar.narratorScript[step]);
                if (step < ConstVar.narratorScript.Count - 1)
                {
                    step++;
                }
                else
                {
                    isSleeping = true;
                    handler = null;
                }
            }
            
        }

        /// <summary>Segue il copione descritto dentro il file narratorScript</summary>
        public void phase1()
        {
            handler = doTutorial;
        }
    }
}
