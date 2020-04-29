using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gioco_generico
{
    public class Windows : Component
    {
        bool show;
        public List<Object> widget;
        Texture2D background;
        Vector2 displayPos;
        Button exit;
        Rectangle rect;
        public Windows(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, Vector2 displayPos, string texture, double scale = 1) : base(_game, _graphics, _content)
        {
            this.displayPos = displayPos;
            background = _content.Load<Texture2D>(texture);
            rect = new Rectangle((int)displayPos.X, (int)displayPos.Y, (int)(background.Width * scale),(int)(background.Height * scale));

            widget = new List<Object>();
        }

        public void Draw()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

    }
}
