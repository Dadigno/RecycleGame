﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;


namespace Recycle_game.States
{
    public class ChooseBucket : State
    {
        //Interazione da tastiera
        Keys[] OldKeyPressed = { };

        //Finestra
        public Windows window;

        private SoundEffect effectCloseWindow;

        public ChooseBucket(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, int id) : base(_game, _graphics, _content, id)
        {
            window = new Windows(_game, _graphics, _content, new Vector2(0, 0), "Finestra/finestra_daUsare(sSBP)", (double)0.2);
            window.show = true;
            effectCloseWindow = _content.Load<SoundEffect>("soundEffect/effectCloseWindow");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Green);
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
                    effectCloseWindow.Play();
                }
            }

            OldKeyPressed = KeyPressed;
        }
    }
}