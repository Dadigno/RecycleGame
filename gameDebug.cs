using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gioco_generico
{
    class gameDebug
    {
        TextBox box1;
        TextBox box2;
        TextBox box3;
        TextBox box4;
        private MouseState _currentMouse;
        public gameDebug(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content)
        {
            box1 = new TextBox(_game, _graphics, _content, new Vector2(0,0),  new Vector2(ConstVar.displayDim.X - 200, 0), ConstVar.font, "", Color.Black);
            box2 = new TextBox(_game, _graphics, _content, new Vector2(0,0), new Vector2(ConstVar.displayDim.X - 200, 20), ConstVar.font, "", Color.Black);
            box3 = new TextBox(_game, _graphics, _content, new Vector2(0,0), new Vector2(ConstVar.displayDim.X - 200, 40), ConstVar.font, "", Color.Black);
            box4 = new TextBox(_game, _graphics, _content, new Vector2(0, 0), new Vector2(ConstVar.displayDim.X - 200, 60), ConstVar.font, "", Color.Black);
        }

        public void Draw()
        {
            box1.Draw();
            box2.Draw();
            box3.Draw();
            box4.Draw();
        }

        public void Update(Character c)
        {
            _currentMouse = Mouse.GetState();
            box1.update("X:" + _currentMouse.X.ToString() + "Y:" + _currentMouse.Y.ToString());
            box2.update("X:" + c.getRect().X.ToString() + "Y:" + c.getRect().Y.ToString());
            box3.update("X:" + c.getGamePos().X.ToString() + "Y:" + c.getGamePos().Y.ToString());
            box4.update("X:" + c.getTilePos(ConstVar.main.background).X + "Y:" + c.getTilePos(ConstVar.main.background).Y);
        }
    }
}
