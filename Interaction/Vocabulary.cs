using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gioco_generico.Interaction
{
    public class Vocabulary : Component
    {
        /*
         * Serve una immagine più piccola per lo sfondo della finestra ( tipo blocco note ) 
         * Due frecce per scorrere i paragrafi
         * PARAGRAFI
         *                          Titolo della pagina
         *      Cos'è la raccolta differenziata
         *          dfhsdaguashasudhfasdoifasidofajsoidfajsogisajgoiasjgaosihgaoighagoiahrg
         *      I bidoni del riciclo
         *          "immagini dei bidoni con il loro nome"
         *      I simboli del riciclo
         *          "immagine dei simboli con la loro descrizione"
         *      Il reciclabolario
         *          "tutti gli oggetti presenti nel gioco con i relativi bidoni in cui vanno gettati"
         *          
         * Questa finestra appare quando si clicca sul punto interrogativo in alto a destra
         */

        Sprite background;

        //Button
        private List<Button> arrows;

        //Simboli
        List<ConstVar.Symbol> simbols;

        //Scritte
        SpriteFont font;
        SpriteFont titlefont;

        //Sound effect
        SoundEffect effectScrollPage;
        //Draw page
        delegate void DrawPage();
        DrawPage _DrawPage;


        /// <summary>
        /// Una finestra per mostrare: ogni singolo simbolo del riciclo con la loro spiegazione, dove ogni oggetto usato nel gioco va gettato
        /// Il vocabolario si riempe con il proseguimento del gioco.
        /// </summary>
        /// <param name="_game"></param>
        /// <param name="_graphics"></param>
        /// <param name="_content"></param>
        public Vocabulary(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content) :  base(_game, _graphics, _content)
        {
            background = new Sprite(_game, _graphics, _content, "vocabulary", new Vector2(0, 0), new Vector2(ConstVar.displayDim.X / 2, ConstVar.displayDim.Y / 2), 1.5f);
            //dichiarazioni bottoni frecce
            var arrowLeft = new Button(_game, _graphics, _content, "Finestra/freccia_sinistraMax", new Vector2(background.getPos().X - background.getRect().Width * 0.4f, background.getPos().Y + background.getRect().Height * 0.45f), Item.Type.NONE);
            arrowLeft.Action += PreviousPage;
            var arrowRight = new Button(_game, _graphics, _content, "Finestra/freccia_destraMax", new Vector2(background.getPos().X + background.getRect().Width * 0.4f, background.getPos().Y + background.getRect().Height * 0.45f), Item.Type.NONE);
            arrowRight.Action += NextPage;
            arrows = new List<Button>()
              {
                arrowLeft,
                arrowRight
              };
            effectScrollPage = _content.Load<SoundEffect>("soundEffect/turnPage");

            simbols = new List<ConstVar.Symbol>();
            ConstVar.Symbol s1 = new ConstVar.Symbol( new Sprite(_game, _graphics, _content, "simboli/raee", new Vector2(0, 0), new Vector2(0,0), 0.25f), "Il logo Raee indica rifiuti elettrici o elettronici che non devono per nessuna ragione essere gettati tra i rifiuti generici ma smaltiti in appositi contenitori o piattaforma ecologica.");
            simbols.Add(s1);
            ConstVar.Symbol s2 = new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/appiattire", new Vector2(0, 0), new Vector2(0, 0), 0.4f), "Appiattire dopo l'uso invita a comprimere i contenitori per ridurne il volume e l'impatto ambientale.");
            simbols.Add(s2);
            ConstVar.Symbol s3 = new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/cestino", new Vector2(0, 0), new Vector2(0, 0), 0.25f), "Il cestino dei rifiuti indica che l'oggetto non va disperso nell'ambiente dopo l'uso. Se avete in mano un prodotto riciclabile ma siete lontani dall'apposito contenitore, il logo invita a conservarlo anziche' abbandonarlo in luoghi pubblici.");
            simbols.Add(s3);
            ConstVar.Symbol s4 = new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/moebius", new Vector2(0, 0), new Vector2(0, 0), 0.25f), "Il simbolo di riciclaggio e' il simbolo internazionale che indica il riciclaggio dei rifiuti. E' composto da tre frecce che formano un nastro dinMobius.");
            simbols.Add(s4);
            ConstVar.Symbol s5 = new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/grune", new Vector2(0, 0), new Vector2(0, 0), 0.25f), "Punto verde individua un sistema di smaltimento degli imballaggi, ma non fornisce alcuna informazione aggiuntiva sulla riciclabilita' del prodotto.");
            simbols.Add(s5);
            ConstVar.Symbol s6 = new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/compostabile", new Vector2(0, 0), new Vector2(0, 0), 0.40f), "Il simbolo compostabili e' uno dei marchi degli organismi certificatori accreditati che attestano la certificazione della biodegradabilita' e della compostabilita'");
            simbols.Add(s6);
            titlefont = _content.Load<SpriteFont>("Fonts/bigFont");
            font = _content.Load<SpriteFont>("Fonts/fontSimbol");
            _DrawPage = DrawPage1;
        }

        private void PreviousPage(object sender, EventArgs e)
        {

            if (_DrawPage == DrawPage3)
            {
                effectScrollPage.Play();
                _DrawPage = DrawPage2;
            }
            else if (_DrawPage == DrawPage2)
            {
                effectScrollPage.Play();
                _DrawPage = DrawPage1;
            }
        }

        private void NextPage(object sender, EventArgs e)
        {
            if (_DrawPage == DrawPage1)
            {
                effectScrollPage.Play();
                _DrawPage = DrawPage2;
            }
            else if (_DrawPage == DrawPage2)
            {
                effectScrollPage.Play();
                _DrawPage = DrawPage3;
            }
        }

        public void Draw()
        {
            background.Draw(true);
            _DrawPage();
        }

        public void Update()
        {
            foreach (var button in arrows)
                button.update();
        }

        bool Intersects(Rectangle a, Rectangle b)
        {
            if (a.X < b.X + b.Width / 2 && a.X > b.X - b.Width / 2)
                if(a.Y < b.Y + b.Height / 2 && a.Y > b.Y - b.Height / 2)
                    return true;
            return false;
        }

        int offset = 100;
        void DrawPage1()
        {
            string title = "I simboli del riciclo";
            ConstVar.sb.DrawString(titlefont, title, new Vector2(background.getPos().X - titlefont.MeasureString(title).X / 2, background.getPos().Y - background.getRect().Height * 0.43f), Color.Black);
            foreach (var button in arrows)
                button.Draw();

            foreach (var simbol in simbols)
            {

                int i = simbols.IndexOf(simbol);
                int offsetx = offset * (i % 5);
                int offsety = offset * (i / 5);
                simbol.symbol.setPos((int)(background.getPos().X + offsetx - background.getRect().Width * 0.30f), (int)(background.getPos().Y  + offsety - background.getRect().Height * 0.25f));
                simbol.symbol.Draw(true);
            }

            foreach (var simbol in simbols)
            {
                MouseState _currentMouse = Mouse.GetState();
                var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
                if (Intersects(mouseRectangle, simbol.symbol.getRect()))
                {
                    SpeechBubble speechBubble = new SpeechBubble(_game, _graphics, _content, "bubble/bubble-narrator", "Fonts/speechFont");
                    speechBubble.setPos(mouseRectangle.X + speechBubble.getRect().Width / 2, mouseRectangle.Y - speechBubble.getRect().Height / 2);
                    speechBubble.StaticDraw(simbol.description);
                }
            }
        }

        void DrawPage2()
        {
            string title = "I bidoni della raccolta";
            ConstVar.sb.DrawString(titlefont, title, new Vector2(background.getPos().X - titlefont.MeasureString(title).X / 2, background.getPos().Y - background.getRect().Height * 0.43f), Color.Black);
            foreach (var button in arrows)
                button.Draw();
        }
        void DrawPage3()
        {
            string title = "Riciclabolario";
            ConstVar.sb.DrawString(titlefont, title, new Vector2(background.getPos().X - titlefont.MeasureString(title).X / 2, background.getPos().Y - background.getRect().Height * 0.43f), Color.Black);
            foreach (var button in arrows)
                button.Draw();
        }

    }
}
