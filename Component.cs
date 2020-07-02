using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Recycle_game
{
    public class Component
    {
        protected ContentManager _content;
        protected GraphicsDeviceManager _graphics;
        protected Game1 _game;
        public Component(Game1 game, GraphicsDeviceManager graphics, ContentManager content)
        {
            _game = game;

            _graphics = graphics;

            _content = content;
        }

        public Component()
        {
            
        }
    }
}
