using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Gioco_generico
{
    public class UI : Component
    {
        //Bottoni
        private List<Button> _buttons;

        //Bar
        //private List<Bar> _bars;
        Bar ScoreBar;
        Bar InventoryBar;
        Narrator narrator;


        public UI(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content) :  base(_game, _graphics, _content)
        {

            //Bars
            /*var barPlastica = new Bar(_game, _graphics, _content, "bars/plasticBar", "Plastica", new Vector2(10,0), 10, Item.Type.PLASTICA);
            var barCarta = new Bar(_game, _graphics, _content, "bars/plasticBar", "Carta", new Vector2(10, 50), 10, Item.Type.CARTA);
            var barVetro = new Bar(_game, _graphics, _content, "bars/plasticBar", "Vetro", new Vector2(10, 100), 10, Item.Type.VETRO);
            var barSecco = new Bar(_game, _graphics, _content, "bars/plasticBar", "Secco", new Vector2(10, 150), 10, Item.Type.SECCO);
            var barUmido = new Bar(_game, _graphics, _content, "bars/plasticBar", "Umido", new Vector2(10, 200), 10, Item.Type.UMIDO);
            var barSpeciale = new Bar(_game, _graphics, _content, "bars/plasticBar", "Speciale", new Vector2(10, 250), 10, Item.Type.SPECIALE);

            _bars = new List<Bar>()
              {
                barPlastica,
                barCarta,
                barVetro,
                barSecco,
                barUmido,
                barSpeciale
              };*/

            ScoreBar = new Bar(_game, _graphics, _content, "bars/genericBar", "Score", new Vector2(10, 10), Item.Type.NONE);
            InventoryBar = new Bar(_game, _graphics, _content, "bars/genericBar", "Inventory", new Vector2(10, 50), Item.Type.NONE);

            //Bottoni
            var helpButton = new Button(_game, _graphics, _content, "help-btn", new Vector2(ConstVar.displayDim.X * 0.95f, ConstVar.displayDim.Y * 0.05f), Item.Type.NONE, 0.2);
            helpButton.Action += Click_help;
            var exitButton = new Button(_game, _graphics, _content, "exit-btn", new Vector2(ConstVar.displayDim.X * 0.98f, ConstVar.displayDim.Y * 0.05f), Item.Type.NONE, 0.2);
            exitButton.Action += Click_exit;

            _buttons = new List<Button>()
              {
                helpButton,
                exitButton
              };


            //Narrator
            narrator = new Narrator(_game, _graphics, _content, "character/narrator", new Vector2(0, 0), new Vector2(ConstVar.displayDim.X * 0.08f, ConstVar.displayDim.Y * 0.85f));

            
        }

        public void Draw()
        {
            //foreach (var bar in _bars)
            //    bar.Draw();
            ScoreBar.Draw();
            InventoryBar.Draw();
            narrator.Draw();

            foreach (var button in _buttons)
                button.Draw();


        }

        public void Update(GameTime gameTime)
        {
            //Bottoni
            foreach (var button in _buttons)
                button.update();

            //Bars
            //foreach (var bar in _bars)
            //    bar.Update(mainChar.Inventory.Count(x => x.type == bar.Type));
            ScoreBar.Update(_game.Score, _game.GameLevel.POINT_TARGET);
            InventoryBar.Update(ConstVar.main.mainChar.Inventory.Count, _game.GameLevel.FULL_INVENTORY);
            //Narratore
            narrator.Update(gameTime);
        }

            private void Click_help(object sender, EventArgs e)
        {
            _game.Exit();
        }
        private void Click_exit(object sender, EventArgs e)
        {
            _game.Exit();
        }



    }
}
