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
        Texture2D menu;
        Vector2 position;
        Rectangle rect;
        public MenuState(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, int id) : base(_game, _graphics, _content, id)
        {

            //background = new Background(_game, _graphics, _content, "maps/background/backgroundMenu");
            menu = _content.Load<Texture2D>("windows/menu");
            rect = new Rectangle((int)ConstVar.displayDim.X / 2 - menu.Width / 2, (int)ConstVar.displayDim.Y / 2 - menu.Height / 2, menu.Width, menu.Height);

            var buttonTexture = _content.Load<Texture2D>("button");
            
            position.X = rect.X + 340;
            position.Y = rect.Y + 330;
            var newGameButton = new Button(_game, _graphics, _content, "windows/play_button", new Vector2(position.X, position.Y));
            newGameButton.Action += PlayGameButton_Click;

            //var loadGameButton = new Button(_game, _graphics, _content, "", new Vector2(position.X, position.Y + 50));
            //loadGameButton.Click += InfoGameButton_Click;

            var quitGameButton = new Button(_game, _graphics, _content, "windows/exit_button", new Vector2(position.X, position.Y + 100));
            quitGameButton.Action += QuitGameButton_Click;

            _buttons = new List<Button>()
              {
                newGameButton,
                //loadGameButton,
                quitGameButton,
              };

            

        }


        private void InfoGameButton_Click(object sender, EventArgs e)
        {
            
        }

        private void PlayGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(ConstVar.main);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Green);
            spriteBatch.Begin();
            spriteBatch.Draw(menu, rect, Color.White);
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
