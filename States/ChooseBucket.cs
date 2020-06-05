using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Gioco_generico.States
{
    public class ChooseBucket : State
    {
        //Interazione da tastiera
        Keys[] OldKeyPressed = { };

        //Finestra
        public Windows window;

        public ChooseBucket(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, int id) : base(_game, _graphics, _content, id)
        {
            window = new Windows(_game, _graphics, _content, new Vector2(0,0), "Finestra/finestra_daUsare(sSBP)",1);
            window.show = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            window.Draw();
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime, KeyboardState kbState)
        {
            keyboardMgnt(kbState, gameTime);

            window.Update(gameTime);

        }

        public void keyboardMgnt(KeyboardState kbState, GameTime gameTime)
        {
            Keys[] KeyPressed = kbState.GetPressedKeys();

            if (!KeyPressed.Contains(Keys.Escape))
            {
                if (OldKeyPressed.Contains(Keys.Escape))
                {
                    ConstVar.main.mainChar.move = true;
                    ConstVar.main.ban.isActive = true;
                    _game.ChangeState(ConstVar.main);
                }
            }

            OldKeyPressed = KeyPressed;
        }
    }
}