using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Recycle_game
{
    public class FinishGame : Component
    {
        Sprite background;
        SpriteFont titlefont;
        Button button;
        public bool visible = false;
        public FinishGame(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content) : base(_game, _graphics, _content)
        {
            background = new Sprite(_game, _graphics, _content, "vocabulary", new Vector2(0, 0), new Vector2(ConstVar.displayDim.X / 2, ConstVar.displayDim.Y / 2), 1.5f);
            //dichiarazioni bottoni frecce
            button = new Button(_game, _graphics, _content, "button/continue_button", new Vector2(background.getPos().X, background.getPos().Y + background.getRect().Height * 0.45f), Item.Type.NONE, 0.80);
            button.Action += closeWindow;

            titlefont = _content.Load<SpriteFont>("Fonts/bigFont");
        }

        private void closeWindow(object sender, EventArgs e)
        {
            visible = false;
        }

        public void Draw()
        {
            if (visible)
            {
                background.Draw(true);
                string title = "Game Over";
                ConstVar.sb.DrawString(titlefont, title, new Vector2(background.getPos().X - titlefont.MeasureString(title).X / 2, background.getPos().Y - background.getRect().Height * 0.43f), Color.Black);
                Sprite img = new Sprite(_game, _graphics, _content, "immagini/finishgame", new Vector2(0, 0), new Vector2(background.getRect().X, background.getRect().Y));
                img.Draw(true);
                button.Draw();
            }
        }

        public void Update()
        {
            if (visible)
            {
                button.update();
            }
        }
    }
}
