using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Gioco_generico
{
    public class Narrator : Sprite
    {
        SpeechBubble speechBubble;
        bool show = false;
        List<String> messages;
        bool isSpeaking;
        public Narrator(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, Vector2 initGamePos, Vector2 initDisplayPos, double scale = 1) : base(_game, _graphics, _content, nameTex, initGamePos, initDisplayPos, scale)
        {
            speechBubble = new SpeechBubble(_game, _graphics, _content, "bubble/bubble-narrator", "Fonts/speechFont");
            speechBubble.Text = "\nCiao sono il\ntuo narratore";
            isSpeaking = true;
        }

        public void Draw()
        {
            Draw(isSpeaking);
            speechBubble.Draw();
            if (isSpeaking)
            {
                isSpeaking = speechBubble.Draw();
            }
        }

        public void Update(GameTime gameTime)
        {
            speechBubble.Update(gameTime, getPos());
        }
    }
}
