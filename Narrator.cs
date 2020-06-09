using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Gioco_generico
{
    public class Narrator : Sprite
    {
        //Max 128 caratteru per riga
        SpeechBubble speechBubble;
        bool show = false;
        List<String> messages;
        bool isSpeaking;
        delegate void doSomething();
        doSomething handler;

        public Narrator(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, Vector2 initGamePos, Vector2 initDisplayPos, float scale = 1) : base(_game, _graphics, _content, nameTex, initGamePos, initDisplayPos, scale)
        {
            speechBubble = new SpeechBubble(_game, _graphics, _content, "bubble/bubble-narrator", "Fonts/font");
            handler = doTutorial;
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

        public void speak(string text)
        {
            speechBubble.Text = text;
            isSpeaking = true;
        }
        int step = 0;
        public void doTutorial()
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
                    handler = null;
                }
            }
        }
    }
}
