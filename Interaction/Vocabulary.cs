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

namespace Recycle_game.Interaction
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

        //Bidoni
        List<Sprite> bidoni;
        //Scritte
        SpriteFont font;
        SpriteFont titlefont;

        //Sound effect
        SoundEffect effectScrollPage;

        //Draw page
        delegate void DrawPage();
        private DrawPage _DrawPage;


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
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/raee", new Vector2(0, 0), new Vector2(0,0), 0.25f), "Il logo Raee indica rifiuti elettrici o elettronici che non devono per nessuna ragione essere gettati tra i rifiuti generici ma smaltiti in appositi contenitori o piattaforma ecologica."));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/appiattire", new Vector2(0, 0), new Vector2(0, 0), 0.4f), "Appiattire dopo l'uso invita a comprimere i contenitori per ridurne il volume e l'impatto ambientale."));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/cestino", new Vector2(0, 0), new Vector2(0, 0), 0.25f), "Il cestino dei rifiuti indica che l'oggetto non va disperso nell'ambiente dopo l'uso. Se avete in mano un prodotto riciclabile ma siete lontani dall'apposito contenitore, il logo invita a conservarlo anziche' abbandonarlo in luoghi pubblici."));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/moebius", new Vector2(0, 0), new Vector2(0, 0), 0.25f), "Il simbolo di riciclaggio e' il simbolo internazionale che indica il riciclaggio dei rifiuti. E' composto da tre frecce che formano un nastro dinMobius."));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/grune", new Vector2(0, 0), new Vector2(0, 0), 0.25f), "Punto verde individua un sistema di smaltimento degli imballaggi, ma non fornisce alcuna informazione aggiuntiva sulla riciclabilita' del prodotto."));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/compostabile", new Vector2(0, 0), new Vector2(0, 0), 0.40f), "Il simbolo compostabili e' uno dei marchi degli organismi certificatori accreditati che attestano la certificazione della biodegradabilita' e della compostabilita'"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/PET-01", new Vector2(0, 0), new Vector2(0, 0), 0.08f), "Polietilentereftalato, un materiale leggero riciclabile al 100% utilizzato principalmente a fini alimentari"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/2_PEHD", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Polietilene ad alta densita', anche detto HDPE, una resina termoplastica usata per sacchetti della spesa e contenitori di cibo e saponi"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/3_PVC", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Polivinilcloruro, sostanza dalla consistenza gommosa usata soprattutto per bottiglie, tubature, giocattoli"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/4_PELD", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Anche detto LDPE, Polietilene a bassa densita': plastica molto utilizzata per sacchi e sacchetti molto morbidi"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/05-PP", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Polipropilene, termoplastica molto usata per gli oggetti di arredamento, contenitori e flaconi"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/06-PS", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Polistirolo o polistirene, usato soprattutto come isolante nel settore edile e negli imballaggi delle merci"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/07-O", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Con queste scritte si identificano tutti quei materiali che non rientrano nelle categorie precedenti e che non sono riciclabili"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/11-MiNh", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Identifica un le batterie di tipo Nichel Metallo Idruro"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/12-Lithium", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Identifica un tipo di batterie, batterie al Litio"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/13-HgO", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Identifica le batterie a ossido di argento"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/21-PAP", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Carta e cartone, puo' presentare anche codici 20,22"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/40_FE", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Acciaio: metallo utilizzato per contenitori e scatolette"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/51-FOR", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Legno, in particolare il codice 51 identifica il sughero, altri da 52 a 59 altri tipi di legno"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/60_TEX", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Materiali tessili, in particolare 60 corrisponde al cotone (vestiario). Dal 61 al 69 altri materiali tessili. IL cotone ad uso medicale non e' differenziato"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/70-GL", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Vetro trasparente e incolore, altri tipi di vetro vanno dal 73 al 79"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/72-GL", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Vetro di colore marrone, altri tipi di vetro vanno dal 73 al 79"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/ABS", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Acrylonitrile butadiene styrene,  NON e' plastica: e' un materiale antiurto utilizzato per utensili da cucina, parti plastiche di elettrodomestici, caschi motociclistici e paraurti e altri particolari delle auto "));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/ALU_41", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Alluminio: metallo utilizzato per lattine, scatolette e contenitori per alimenti"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/20-PAP", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Cartone ondulato, scatole per imballaggi"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/9-ALCALINE", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Batterie di tipo alcaline"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/8-PIOMBO", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Batterie di tipo al piombo"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/50-FOR", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Legno generico, altri da 52 a 59 altri tipi di legno specifici"));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/61-TEX", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Materiali tessili,  in particolare Juta. Dal 62 al 69 altri materiali tessili."));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/PAP_22", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Carta: carta di gionale sacchetti libri ecc, dal 23 al 39 altri tipi di carta."));
            simbols.Add(new ConstVar.Symbol(new Sprite(_game, _graphics, _content, "simboli/Vetro71", new Vector2(0, 0), new Vector2(0, 0), 0.70f), "Vetro di colore verde, altri tipi di vetro vanno dal 73 al 79"));

            bidoni = new List<Sprite>();
            bidoni.Add(new Sprite(_game, _graphics, _content, "Finestra/puzzleCarta", new Vector2(0, 0), new Vector2(0, 0), 0.45f));
            bidoni.Add(new Sprite(_game, _graphics, _content, "Finestra/puzzleOrganico", new Vector2(0, 0), new Vector2(0, 0), 0.45f));
            bidoni.Add(new Sprite(_game, _graphics, _content, "Finestra/puzzleSecco", new Vector2(0, 0), new Vector2(0, 0), 0.45f));
            bidoni.Add(new Sprite(_game, _graphics, _content, "Finestra/puzzleVetro", new Vector2(0, 0), new Vector2(0, 0), 0.45f));
            bidoni.Add(new Sprite(_game, _graphics, _content, "Finestra/puzzleOlioSpecifico", new Vector2(0, 0), new Vector2(0, 0), 0.45f));
            bidoni.Add(new Sprite(_game, _graphics, _content, "Finestra/puzzleToner", new Vector2(0, 0), new Vector2(0, 0), 0.45f));
            bidoni.Add(new Sprite(_game, _graphics, _content, "Finestra/puzzleFarmaci", new Vector2(0, 0), new Vector2(0, 0), 0.45f));
            bidoni.Add(new Sprite(_game, _graphics, _content, "Finestra/puzzleAbiti", new Vector2(0, 0), new Vector2(0, 0), 0.45f));
            bidoni.Add(new Sprite(_game, _graphics, _content, "Finestra/puzzleCentroRac", new Vector2(0, 0), new Vector2(0, 0), 0.45f));
            bidoni.Add(new Sprite(_game, _graphics, _content, "Finestra/puzzleBatterie", new Vector2(0, 0), new Vector2(0, 0), 0.45f));
            bidoni.Add(new Sprite(_game, _graphics, _content, "Finestra/puzzlePlastMetal", new Vector2(0, 0), new Vector2(0, 0), 0.45f));

            titlefont = _content.Load<SpriteFont>("Fonts/bigFont");
            font = _content.Load<SpriteFont>("Fonts/fontSimbol");
            _DrawPage = DrawPage1;
        }
        /// <summary>
        /// Vai alla pagina precedente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            else if (_DrawPage == DrawPage4)
            {
                effectScrollPage.Play();
                _DrawPage = DrawPage3;
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
            else if (_DrawPage == DrawPage3)
            {
                effectScrollPage.Play();
                _DrawPage = DrawPage4;
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
                int offsetx = offset * (i % 6);
                int offsety = offset * (i / 6);
                simbol.symbol.setPos((int)(background.getPos().X + offsetx - background.getRect().Width * 0.40f), (int)(background.getPos().Y  + offsety - background.getRect().Height * 0.28f));
                simbol.symbol.Draw(true);
            }

            foreach (var simbol in simbols)
            {
                MouseState _currentMouse = Mouse.GetState();
                var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
                if (Intersects(mouseRectangle, simbol.symbol.getRect()))
                {
                    SpeechBubble speechBubble = new SpeechBubble(_game, _graphics, _content, "bubble/bubble-narrator", "Fonts/speechFont");
                    speechBubble.setPos(mouseRectangle.X + (int)(speechBubble.getRect().Width * 0.25f), mouseRectangle.Y - speechBubble.getRect().Height / 2);
                    speechBubble.StaticDraw(simbol.description);
                }
            }
        }

        void DrawPage2()
        {
            string title = "Tutti i bidoni della raccolta";
            ConstVar.sb.DrawString(titlefont, title, new Vector2(background.getPos().X - titlefont.MeasureString(title).X / 2, background.getPos().Y - background.getRect().Height * 0.43f), Color.Black);
            foreach (var button in arrows)
                button.Draw();

            int offset = 150;
            foreach (var b in bidoni)
            {
                int i = bidoni.IndexOf(b);
                int offsetx = offset * (i % 3);
                int offsety = offset * (i / 3);
                b.setPos((int)(background.getPos().X + offsetx - background.getRect().Width * 0.30f), (int)(background.getPos().Y + offsety - background.getRect().Height * 0.25f));
                b.Draw(true);
            }

        }
        void DrawPage3()
        {
            string title = "I rifiuti";
            ConstVar.sb.DrawString(titlefont, title, new Vector2(background.getPos().X - titlefont.MeasureString(title).X / 2, background.getPos().Y - background.getRect().Height * 0.43f), Color.Black);
            foreach (var button in arrows)
                button.Draw();

            int offset = 80;
            foreach (var obj in ConstVar.allObjects)
            {
                int i = ConstVar.allObjects.IndexOf(obj);
                if (i < 56)
                {
                    int offsetx = offset * (i % 7);
                    int offsety = offset * (i / 7);
                    obj.setPos((int)(background.getPos().X + offsetx - background.getRect().Width * 0.39f), (int)(background.getPos().Y + offsety - background.getRect().Height * 0.32f));
                    obj.Draw(true);
                }
            }
        }

        void DrawPage4()
        {
            string title = "Riciclabolario";
            ConstVar.sb.DrawString(titlefont, title, new Vector2(background.getPos().X - titlefont.MeasureString(title).X / 2, background.getPos().Y - background.getRect().Height * 0.43f), Color.Black);
            foreach (var button in arrows)
                button.Draw();

            int offset = 80;
            foreach (var obj in ConstVar.allObjects)
            {
                int i = ConstVar.allObjects.IndexOf(obj);
                if (i > 55)
                {
                    int k = ConstVar.allObjects.Count() - i - 1;
                    int offsetx = offset * (k % 7);
                    int offsety = offset * (k / 7);
                    obj.setPos((int)(background.getPos().X + offsetx - background.getRect().Width * 0.39f), (int)(background.getPos().Y + offsety - background.getRect().Height * 0.32f));
                    obj.Draw(true);
                }
            }
        }

    }
}
