using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Net;

namespace Recycle_game.States
{
    public class MainState : State
    {
        //Finestre di testo
        gameDebug gDebug;
        public Banner ban;

        //Personaggi 
        public Character mainChar;
        Character alice;

        //Oggetti
        List<Item> objects;
        

        //Interazione da tastiera
        Keys[] OldKeyPressed = { };

        Random rnd = new Random();

        //Sound
        //Song song;
        private SoundEffect coinSound;
        private SoundEffect effectOpenWindow;

        //Background
        public Background background;

        //Stati interni
        public delegate void state(GameTime gameTime);
        public state _currentInternalState;

        //Azioni 
        public delegate void action();
        public action space_button_action;

        //Timer
        private static float TIMER = 2000;
        private float timer = TIMER;
        protected float timerAnimated = 0;


        public MainState(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, int id) : base(_game, _graphics, _content, id)
        {
            //Carico la mappa
            background = new Background(_game, _graphics, _content, "maps/mondo");

            //Inizializzazione personaggi
            mainChar = new Character(_game, _graphics, _content, "character/bob", new Vector2(ConstVar.animatedSpriteWidth / 2, ConstVar.animatedSpriteHeigth), new Vector2(0, 0), ConstVar.animatedCols, ConstVar.animatedFrame, ConstVar.animatedSpriteWidth, ConstVar.animatedSpriteHeigth);
            mainChar.isGamer = true;
            mainChar.setTilePos(9, 28, background);
            mainChar.Action += Character_Action;

            alice = new Character(_game, _graphics, _content, "character/alice", new Vector2(ConstVar.animatedSpriteWidth / 2, ConstVar.animatedSpriteHeigth), new Vector2(0, 0), ConstVar.animatedCols, ConstVar.animatedFrame, ConstVar.animatedSpriteWidth, ConstVar.animatedSpriteHeigth);
            alice.isGamer = false;
            alice.setTilePos(47, 22, background);


            //Inizializzo le finestre di testo
            ban = new Banner(_game, _graphics, _content, "button/button", new Vector2(ConstVar.displayDim.X / 2, ConstVar.displayDim.Y / 2), "Fonts/Font", "");
            gDebug = new gameDebug(_game, _graphics, _content);

            //All Objects 
            //ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/bananaDebug", new Vector2(0, 0), new Vector2(0, 0), 0.5, Item.Type.ORGANICO, true));
            //ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/bananaDebug", new Vector2(0, 0), new Vector2(0, 0), 1, Item.Type.ORGANICO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/banana", new Vector2(0, 0), new Vector2(0, 0), 0.4f, Item.Type.ORGANICO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/lattina", new Vector2(0, 0), new Vector2(0, 0), 0.3f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/mela", new Vector2(0, 0), new Vector2(0, 0), 0.2f, Item.Type.ORGANICO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/giornale", new Vector2(0, 0), new Vector2(0, 0), 0.3f, Item.Type.CARTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/accendino", new Vector2(0, 0), new Vector2(0, 0), 0.2f, Item.Type.SECCO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/scatola-cartone", new Vector2(0, 0), new Vector2(0, 0), 0.25f, Item.Type.CARTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/bottiglia-vetro", new Vector2(0, 0), new Vector2(0, 0), 0.3f, Item.Type.VETRO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/bottiglia", new Vector2(0, 0), new Vector2(0, 0), 0.07f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/battery", new Vector2(0, 0), new Vector2(0, 0), 0.06f, Item.Type.BATTERIE, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/car-battery", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.BATTERIE, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/tv", new Vector2(0, 0), new Vector2(0, 0), 0.08f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/wooden-chair", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/microwave-oven", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/clothes", new Vector2(0, 0), new Vector2(0, 0), 0.05f, Item.Type.ABITI, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/lamp", new Vector2(0, 0), new Vector2(0, 0), 0.2f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/jeans", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.ABITI, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/plastic-sack", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/chemical-containers", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/bucket", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/asciugamani", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.ABITI, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/batteriaHgO", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.BATTERIE, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/batteriaLithium", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.BATTERIE, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/batteriaMiNh", new Vector2(0, 0), new Vector2(0, 0), 0.2f, Item.Type.BATTERIE, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/contenitorePolipropilene", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/cotone", new Vector2(0, 0), new Vector2(0, 0), 0.25f, Item.Type.SECCO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/farmaci1", new Vector2(0, 0), new Vector2(0, 0), 0.2f, Item.Type.FARMACI, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/farmaci2", new Vector2(0, 0), new Vector2(0, 0), 0.2f, Item.Type.FARMACI, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/flaconeBioplastica", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/foglioAlluminio", new Vector2(0, 0), new Vector2(0, 0), 0.2f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/lattinaAcciaio", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/olioMinerale", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.OLIOSPECIFICO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/olioGirasole", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.OLIOSPECIFICO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/libri", new Vector2(0, 0), new Vector2(0, 0), 0.2f, Item.Type.CARTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/olioOliva", new Vector2(0, 0), new Vector2(0, 0), 0.15f, Item.Type.OLIOSPECIFICO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/riviste", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.CARTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/scatolaPolistirolo", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/scatoleAlimentari", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.CARTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/tappoSughero", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.SECCO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/toner1", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.TONER, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/toner2", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.TONER, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/toner3", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.TONER, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/vetroMarrone", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.VETRO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/vetroTrasparente", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.VETRO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/mattonciniLego", new Vector2(0, 0), new Vector2(0, 0), 0.25f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/toner4", new Vector2(0, 0), new Vector2(0, 0), 0.15f, Item.Type.TONER, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/toner5", new Vector2(0, 0), new Vector2(0, 0), 0.15f, Item.Type.TONER, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/asciugacapelli", new Vector2(0, 0), new Vector2(0, 0), 0.10f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/bilanciaCucina", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/bombolettaSpray", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/borsa", new Vector2(0, 0), new Vector2(0, 0), 0.10f, Item.Type.ABITI, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/cappello", new Vector2(0, 0), new Vector2(0, 0), 0.10f, Item.Type.ABITI, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/caricabatterie", new Vector2(0, 0), new Vector2(0, 0), 0.10f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/cartonePizzaSporca", new Vector2(0, 0), new Vector2(0, 0), 0.15f, Item.Type.SECCO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/cassa", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/chewing-gum", new Vector2(0, 0), new Vector2(0, 0), 0.15f, Item.Type.SECCO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/coperta", new Vector2(0, 0), new Vector2(0, 0), 0.15f, Item.Type.ABITI, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/frullatore", new Vector2(0, 0), new Vector2(0, 0), 0.10f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/insalata", new Vector2(0, 0), new Vector2(0, 0), 0.10f, Item.Type.ORGANICO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/lampadinaLED", new Vector2(0, 0), new Vector2(0, 0), 0.10f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/lettoreMP3", new Vector2(0, 0), new Vector2(0, 0), 0.10f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/liscaPesce", new Vector2(0, 0), new Vector2(0, 0), 0.15f, Item.Type.ORGANICO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/mascheraMare", new Vector2(0, 0), new Vector2(0, 0), 0.15f, Item.Type.SECCO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/matita", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.SECCO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/pannolino", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.SECCO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/scarpe", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.ABITI, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/scontrino", new Vector2(0, 0), new Vector2(0, 0), 0.10f, Item.Type.SECCO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/lavatrice", new Vector2(0, 0), new Vector2(0, 0), 0.15f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/sigaretta", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.SECCO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/uovo", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.ORGANICO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/armadio", new Vector2(0, 0), new Vector2(0, 0), 0.2f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/poltrona", new Vector2(0, 0), new Vector2(0, 0), 0.2f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/ringhiera", new Vector2(0, 0), new Vector2(0, 0), 0.2f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/cellulareRotto", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.CENTRORACCOLTA, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/bicchiere", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.VETRO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/bottigliaBirra", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.VETRO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/carote", new Vector2(0, 0), new Vector2(0, 0), 0.15f, Item.Type.ORGANICO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/lattinaBibita", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.PLASTICA_MET, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/pomata", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.FARMACI, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/tazzinaCaffeVetro", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.VETRO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/termometro", new Vector2(0, 0), new Vector2(0, 0), 0.2f, Item.Type.FARMACI, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/zucca", new Vector2(0, 0), new Vector2(0, 0), 0.15f, Item.Type.ORGANICO, true));
            ConstVar.allObjects.Add(new Item(_game, _graphics, _content, "oggetti/bloccoNote", new Vector2(0, 0), new Vector2(0, 0), 0.1f, Item.Type.CARTA, true));

            objects = new List<Item>();

            //Load effect
            coinSound = _content.Load<SoundEffect>("soundEffect/coin-dropped");
            effectOpenWindow = _content.Load<SoundEffect>("soundEffect/effectOpenWindow");

            //for (int i = 0; i<10; i++) { 
            //mainChar.collect(ConstVar.allObjects[i]); }


            _currentInternalState = state0;
        }

        void Character_Action(object sender, CharEventArgs e)
        {
            List<KeyValuePair<string, int>> bidoni_tilecode = new List<KeyValuePair<string, int>>(){
                new KeyValuePair<string, int>("blue", 9765),
                new KeyValuePair<string, int>("grigio", 9764),
                new KeyValuePair<string, int>("giallo", 9846),
                new KeyValuePair<string, int>("marrone", 9762),
                new KeyValuePair<string, int>("rosso", 9766),
                new KeyValuePair<string, int>("verde", 9763),
                new KeyValuePair<string, int>("centro_raccolta", 9761),
                new KeyValuePair<string, int>("vestiti", 9833),
                new KeyValuePair<string, int>("olio", 13510),
                new KeyValuePair<string, int>("toner", 13526),
                new KeyValuePair<string, int>("batterie", 9850)
             };
            
            if(bidoni_tilecode.Where(kvp => kvp.Value == e.a) != null)
            {

                ban.text = "Premi space per gettare i rifiuti";
                ban.isVisible = true;
                ConstVar.chooseBucket.window.activeBin = bidoni_tilecode.Where(kvp => kvp.Value == e.a).ToList();
            }
            else
            {
                space_button_action = null;
                ban.isVisible = false;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            background.Draw();
            foreach (Item obj in objects)
                obj.Draw();

            //alice.Draw();
            mainChar.Draw();
            ban.Draw();
            //gDebug.Draw();
            ConstVar.UI.Draw();
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime, KeyboardState kbState)
        {
            keyboardMgnt(kbState, gameTime);
            background.update(mainChar);
            ban.update(gameTime);
            //gDebug.Update(mainChar);
            mainChar.update(gameTime, background);


            // Spawn spazzatura
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timerAnimated -= elapsed;
            if (timerAnimated < 0)
            {
                timerAnimated = 100;

                List<Item> temp = ConstVar.allObjects.FindAll(o => _game.GameLevel.OBJ_TYPES.Contains(o.type));
                Item obj = temp[rnd.Next(0, temp.Count())].Clone();
                //
                
                int x = rnd.Next(0, 79);
                int y = rnd.Next(0, 79);

                switch (obj.type)
                {
                    case Item.Type.ORGANICO:
                        if (ConstVar.layers.Find(t => Equals(t.name, "parco")).tileMap[y, x] != 0 && !objects.Exists(b => b.getTilePos(background) == new Vector2(x, y)))
                        {
                            obj.setTilePos(x, y, background);
                            objects.Add(obj);
                        }
                        break;
                    case Item.Type.PLASTICA_MET:
                        if (ConstVar.layers.Find(t => Equals(t.name, "spiaggia")).tileMap[y, x] != 0 && !objects.Exists(b => b.getTilePos(background) == new Vector2(x, y)))
                        {
                            obj.setTilePos(x, y, background);
                            objects.Add(obj);
                        }
                        break;
                    case Item.Type.FARMACI:
                        if (ConstVar.layers.Find(t => Equals(t.name, "farmacia")).tileMap[y, x] != 0 && !objects.Exists(b => b.getTilePos(background) == new Vector2(x, y)))
                        {
                            obj.setTilePos(x, y, background);
                            objects.Add(obj);
                        }
                        break;
                    case Item.Type.CENTRORACCOLTA:
                        if (ConstVar.layers.Find(t => Equals(t.name, "spiaggia")).tileMap[y, x] != 0 && !objects.Exists(b => b.getTilePos(background) == new Vector2(x, y)) ||
                            ConstVar.layers.Find(t => Equals(t.name, "case_moderne")).tileMap[y, x] != 0 && !objects.Exists(b => b.getTilePos(background) == new Vector2(x, y))) 
                        {
                            obj.setTilePos(x, y, background);
                            objects.Add(obj);
                        }
                        break;
                    default:
                        if (ConstVar.layers.Find(t => Equals(t.name, "street")).tileMap[y, x] != 0 && !objects.Exists(b => b.getTilePos(background) == new Vector2(x, y)))
                        {
                            obj.setTilePos(x, y, background);
                            objects.Add(obj);
                        }
                        break;
                }
            }

            for (int i = objects.Count; i > 0; i--)
            {
                objects[i - 1].Update(gameTime, background);
                if (objects[i - 1].getTilePos(background) == mainChar.getTilePos(background))
                {
                    if (mainChar.Inventory.Count < _game.GameLevel.FULL_INVENTORY)
                    {
                        coinSound.Play();
                        mainChar.collect(objects[i - 1]);
                        objects.RemoveAt(i - 1);
                    }
                    else
                    {
                        //Lampeggia
                        ConstVar.UI.InventoryBar.Blink();
                    }
                }
            }
            ConstVar.UI.Update(gameTime);
            _currentInternalState(gameTime);
        }

        void keyboardMgnt(KeyboardState kbState, GameTime gameTime)
        {
            Keys[] KeyPressed = kbState.GetPressedKeys();

            if (KeyPressed.Contains(Keys.Left))
            {
                mainChar.setAction(Character.walk.LEFT);
            }
            else if (KeyPressed.Contains(Keys.Right))
            {
                mainChar.setAction(Character.walk.RIGHT);
            }
            else if (KeyPressed.Contains(Keys.Up))
            {
                mainChar.setAction(Character.walk.UP);
            }
            else if (KeyPressed.Contains(Keys.Down))
            {
                mainChar.setAction(Character.walk.DOWN);
            }
            else
            {
                mainChar.setAction(Character.walk.NOP);
            }

            if (!KeyPressed.Contains(Keys.Escape))
            {
                if (OldKeyPressed.Contains(Keys.Escape))
                {
                    _game.ChangeState(ConstVar.menu);
                }
            }

            if (!KeyPressed.Contains(Keys.Space))
            {
                if (OldKeyPressed.Contains(Keys.Space) && ban.isVisible)
                {
                    ban.isVisible = false;
                    ban.isActive = false;
                    mainChar.move = false;
                    effectOpenWindow.Play();
                    ConstVar.chooseBucket.window.widget = ConstVar.main.mainChar.Inventory;
                    _game.ChangeState(ConstVar.chooseBucket);
                }

                if (OldKeyPressed.Contains(Keys.Space))
                {
                    space_button_action?.Invoke();
                }
            }

            OldKeyPressed = KeyPressed;
        }

        public bool isAround(Character a, Character b)  //mi dice se a è attorno a b
        {
            Vector2 a_tile = a.getTilePos(background);
            Vector2 b_tile = b.getTilePos(background);
            if (b_tile + new Vector2(-1, -1) == a_tile || b_tile + new Vector2(0, -1) == a_tile || b_tile + new Vector2(1, -1) == a_tile)
                return true;
            if (b_tile + new Vector2(-1, 0) == a_tile || b_tile + new Vector2(0, 0) == a_tile || b_tile + new Vector2(1, 0) == a_tile)
                return true;
            if (b_tile + new Vector2(0, -1) == a_tile || b_tile + new Vector2(0, 1) == a_tile || b_tile + new Vector2(1, 1) == a_tile)
                return true;
            return false;
        }


        void changeState(state nextState)
        {
            _currentInternalState = nextState;
        }

        void state0(GameTime gameTime)
        {
            if(!ConstVar.UI.narrator.Sleeping)
                mainChar.move = false;
        }
        
        void state1(GameTime gameTime)
        {
            alice.think(Character.thinkType.THINK);
            if(isAround(mainChar,alice))
            {
                space_button_action = delegate ()
                    {
                        alice.think(Character.thinkType.NONE);
                        ConstVar.UI.narrator.phase1();
                        changeState(state0);
                    };
            }
        }
        /*
        void state2(GameTime gameTime)
        {
            space_button_action = null;
            if (speechIntroAlice.Count() != 0 && !narrator.isSpeaking)
            {
                narrator.speak(speechIntroAlice[0]);
                speechIntroAlice.RemoveAt(0);
            }
            if (speechIntroAlice.Count() == 0 && !narrator.isSpeaking)
            {
                mainChar.think(Character.thinkType.ESCLAMATIVE);
                changeState(state3);
            }
        }
        
        void state3(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timer -= elapsed;
            if (timer < 0)
            {
                timer = TIMER;
                mainChar.think(Character.thinkType.NONE);
                alice.think(Character.thinkType.NONE);
                changeState(state0);
            }
        }

        List<string> speechIntroAlice = new List<string>() {
            "Hey ciao giovane \navventuriero!\nBenvenuto in citta'!",
            "Con questo gioco imparerai\na fare la raccolta\n differenziata . . .",
            "Fai quello che ti dico e\nnon rompere i coglioni\n. . . :)"
        };
        List<string> speechBob = new List<string>() {
            "Bene e tu?",
            "Cosa dai dicendo?\n Stupido omino con i bordi\n pixelati!!",
            "Stupido sprite che\nnon sei altro!!!"
        };*/
    }
}


