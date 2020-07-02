using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Recycle_game.States
{
    public abstract class State : Component
    {
        public int ID;

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        //public abstract void PostUpdate(GameTime gameTime);
        public abstract void Update(GameTime gameTime, KeyboardState kbState);

        public State(Game1 game, GraphicsDeviceManager graphics, ContentManager content, int id) : base(game, graphics, content)
        {
            this.ID = id;
        }
    }
}
