using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Gioco_generico.States
{
    public class ChooseBucket : State
    {
        //Oggetti
        private List<Button> cans;
        public List<Item> obj;
        //Interazione da tastiera
        Keys[] OldKeyPressed = { };
        TextBox counter;
        public ChooseBucket(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, int id) : base(_game, _graphics, _content, id)
        {
            counter = new TextBox(_game, _graphics, _content, new Vector2(0, 0), new Vector2(ConstVar.displayDim.X / 2, ConstVar.displayDim.Y / 2), _content.Load<SpriteFont>("Fonts/barFont"), "0", Color.Black);
            //Cans
            var bidonPlastica = new Button(_game, _graphics, _content, "oggetti/bidone-plastica", new Vector2((int)(ConstVar.displayDim.X * 0.1), (int)(ConstVar.displayDim.Y * 0.6)), "Fonts/Font", "", 0.7);
            //helpButton.Click += Click_help;
            var bidoneCarta = new Button(_game, _graphics, _content, "oggetti/bidone-carta", new Vector2((int)(ConstVar.displayDim.X * 0.25), (int)(ConstVar.displayDim.Y * 0.6)), "Fonts/Font", "", 0.7);
            //exitButton.Click += Click_exit;
            var bidoneVetro = new Button(_game, _graphics, _content, "oggetti/bidone-vetro", new Vector2((int)(ConstVar.displayDim.X * 0.40), (int)(ConstVar.displayDim.Y * 0.6)), "Fonts/Font", "", 0.7);
            //exitButton.Click += Click_exit;
            var bidoneUmido = new Button(_game, _graphics, _content, "oggetti/bidone-umido", new Vector2((int)(ConstVar.displayDim.X * 0.55), (int)(ConstVar.displayDim.Y * 0.6)), "Fonts/Font", "", 0.7);
            //exitButton.Click += Click_exit;
            var bidoneSecco = new Button(_game, _graphics, _content, "oggetti/bidone-secco", new Vector2((int)(ConstVar.displayDim.X * 0.70), (int)(ConstVar.displayDim.Y * 0.6)), "Fonts/Font", "", 0.7);
            //exitButton.Click += Click_exit;
            cans = new List<Button>()
            {
                bidonPlastica,
                bidoneCarta,
                bidoneVetro,
                bidoneUmido,
                bidoneSecco
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            counter.Draw();
            drawObject();
            foreach (var can in cans)
                can.Draw();
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime, KeyboardState kbState)
        {
            keyboardMgnt(kbState, gameTime);
            counter.update(obj.Count().ToString());

            foreach (var can in cans)
                can.update();
        }

        public void keyboardMgnt(KeyboardState kbState, GameTime gameTime)
        {
            Keys[] KeyPressed = kbState.GetPressedKeys();

            if (!KeyPressed.Contains(Keys.Space))
            {
                if (OldKeyPressed.Contains(Keys.Space))
                {
                    _game.ChangeState(ConstVar.main);
                }
            }

            OldKeyPressed = KeyPressed;
        }

        public void drawObject()
        {
            int n = obj.Count();
            double spacing = (int)(ConstVar.displayDim.X * 0.05);
            int cols = 15;
            foreach (Item i in obj) 
            {
                i.rect.X = (int)(ConstVar.displayDim.X * 0.1 + spacing * (obj.IndexOf(i) % cols));
                if (obj.IndexOf(i) % cols == 0 && obj.IndexOf(i) != 0)
                {
                    i.rect.Y = (int)((ConstVar.displayDim.X * 0.1) + spacing * obj.IndexOf(i) / cols);
                }
                else
                {
                    i.rect.Y = (int)(ConstVar.displayDim.X * 0.1);
                }
                i.rect.Width *= 2;
                i.rect.Height *= 2;
                i.Draw();
                i.rect.Width /= 2;
                i.rect.Height /= 2;
            }

        }

    }
}