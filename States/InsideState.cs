using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gioco_generico.States
{
    public class InsideState : State
    {
        bool houseDoorActive = false;
        bool stair = false;
        Background map;
        Character mainChar;
        Banner ban;
        Keys[] OldKeyPressed;
        public InsideState(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, int id) : base(_game, _graphics, _content, id)
        {
            map = new Background(_game, _graphics, _content, "maps/background/insidehouse1");
            mainChar = new Character(_game, _graphics, _content, "bob", new Vector2(ConstVar.animatedSpriteWidth / 2, ConstVar.animatedSpriteHeigth), new Vector2(0, 0), ConstVar.animatedCols, ConstVar.animatedFrame, ConstVar.animatedSpriteWidth, ConstVar.animatedSpriteHeigth);
            mainChar.lockDisplay = false;
            mainChar.setTilePos(41, 37, map);
            mainChar.Action += Character_Action;
            ban = new Banner(_game, _graphics, _content, "button", new Vector2(ConstVar.displayDim.X - 300, ConstVar.displayDim.Y - 60), "Fonts/Font", "");
        }

        private void Character_Action(object sender, CharEventArgs e)
        {
            switch (e.a)
            {
                case 1127:
                    houseDoorActive = true;
                    ban.isVisible = true;
                    ban.text = "Space";
                    break;
                case 1099:
                    stair = true;
                    ban.isVisible = true;
                    ban.text = "Space";
                    break;
                default:
                    houseDoorActive = false;
                    break;

            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            map.Draw();
            mainChar.Draw();
            ban.Draw();
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime, KeyboardState kbState)
        {
            keyboardMgnt(kbState, gameTime);
            map.update(mainChar);
            mainChar.update(gameTime, map);
            ban.update(gameTime);
        }
        bool pass = false;
        public void keyboardMgnt(KeyboardState kbState, GameTime gameTime)
        {
            Keys[] KeyPressed = kbState.GetPressedKeys();

            if (KeyPressed.Contains(Keys.Left))
            {
                mainChar.setAction(Character.walk.LEFT);
            }
            else if (KeyPressed.Contains(Keys.Right))
            {
                mainChar.setAction(Character.walk.RIGHT);
            }
            else if (KeyPressed.Contains(Keys.Up))
            {
                mainChar.setAction(Character.walk.UP);
            }
            else if (KeyPressed.Contains(Keys.Down))
            {
                mainChar.setAction(Character.walk.DOWN);
            }
            else
            {
                mainChar.setAction(Character.walk.NOP);
            }

            if (KeyPressed.Contains(Keys.Escape))
            {
                _game.ChangeState(ConstVar.menu);
            }

            if (!KeyPressed.Contains(Keys.Space) && houseDoorActive)
            {
                if (OldKeyPressed.Contains(Keys.Space))
                {
                    _game.ChangeState(ConstVar.main);
                }
            }

            OldKeyPressed = KeyPressed;
        }
    }
}
