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
    public class MenuState : State
    {
        private List<Button> _buttons;
        private Background background;
        bool isVisible = true;
        Vector2 position;
        public MenuState(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, int id) : base(_game, _graphics, _content, id)
        {
            var buttonTexture = _content.Load<Texture2D>("button");
            
            position.X = ConstVar.displayDim.X / 2 - buttonTexture.Width / 2;
            position.Y = ConstVar.displayDim.Y / 2 - 200;
            var newGameButton = new Button(_game, _graphics, _content, "button", new Vector2(position.X, position.Y), "Fonts/Font", "Continue");
            newGameButton.Click += NewGameButton_Click;

            var loadGameButton = new Button(_game, _graphics, _content, "button", new Vector2(position.X, position.Y + 50), "Fonts/Font", "Load Game");
            loadGameButton.Click += LoadGameButton_Click;

            var quitGameButton = new Button(_game, _graphics, _content, "button", new Vector2(position.X, position.Y + 100), "Fonts/Font", "Quit Game");
            quitGameButton.Click += QuitGameButton_Click;

            background = new Background(_game, _graphics, _content, "maps/background/backgroundMenu");

            _buttons = new List<Button>()
              {
                newGameButton,
                loadGameButton,
                quitGameButton,
              };

            
        }


        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(ConstVar.main);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw();
            if (isVisible)
            {
                foreach (var button in _buttons)
                    button.Draw();
            }

            spriteBatch.End();
        }


        public override void Update(GameTime gameTime, KeyboardState kbState)
        {

            if (isVisible)
            {
                foreach (var button in _buttons)
                    button.update();
            }
        }

        
    }

}
