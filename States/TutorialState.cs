using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recycle_game.States
{
    public class TutorialState : State
    {
        public TutorialState(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, int id) : base(_game, _graphics, _content, id)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Green);
            spriteBatch.Begin();
            ConstVar.tutorial.Draw();
            spriteBatch.End();

        }

        public override void Update(GameTime gameTime, KeyboardState kbState)
        {
            ConstVar.UI.Update(gameTime);
            ConstVar.tutorial.Update();
        }
    }
}
