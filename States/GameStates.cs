using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Gioco_generico.States
{
    public class GameStates : State
    {
        public GameStates(Game1 game, GraphicsDeviceManager graphics, ContentManager content, int id) : base(game, graphics, content, id)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

        public override void Update(GameTime gameTime, KeyboardState kbState)
        {
            
        }
    }
}
