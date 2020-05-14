using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Net;

namespace Gioco_generico.States
{
    public class MainState : State
    {
        //Finestre di testo
        gameDebug gDebug;
        public Banner ban;

        //Personaggi 
        public Character mainChar;
        Character alice;
        //AiMngnt aiAlice;
        //List<AiMngnt> AiStorm;
        //List<Character> storm;

        //Oggetti
        List<Item> objects;
        List<Item> allObjects;

        //Interazione da tastiera
        Keys[] OldKeyPressed = { };

        //Bottoni
        private List<Button> _buttons;

        //Bar
        Bar barPlastica;
        Bar barUmido;
        Bar barSecco;
        Bar barVetro;
        Bar barSpeciale;
        Bar barCarta;

        Random rnd = new Random();

        //Interazioni giocatore-mappa
        bool windowsActive = false;

        //Game variables

        private int plasticaTarget = 10;
        private int umidoTarget = 10;
        private int vetroTarget = 10;
        private int seccoTarget = 10;
        private int cartaTarget = 10;
        private int specialeTarget = 10;

        //Sound
        //Song song;
        private SoundEffect coinSound;

        //Background
        public Background background;

        //Stati interni
        public delegate void state(GameTime gameTime);
        public state _currentState;

        //Azioni 
        public delegate void action();
        public action space_button_action;

        //Timer
        private static float TIMER = 2000;
        private float timer = TIMER;
        protected float timerAnimated = 0;

        //Finestra
        private Windows window;

        public MainState(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, int id) : base(_game, _graphics, _content, id)
        {
            //Carico la mappa
            background = new Background(_game, _graphics, _content, "maps/background/mondo1");   
            
            //Inizializzazione personaggi
            mainChar = new Character(_game, _graphics, _content, "character/bob", new Vector2(ConstVar.animatedSpriteWidth / 2, ConstVar.animatedSpriteHeigth), new Vector2(0, 0), ConstVar.animatedCols, ConstVar.animatedFrame, ConstVar.animatedSpriteWidth, ConstVar.animatedSpriteHeigth);
            mainChar.lockDisplay = true;
            mainChar.setTilePos(46, 27, background);
            mainChar.Action += Character_Action;

            alice = new Character(_game, _graphics, _content, "character/alice", new Vector2(ConstVar.animatedSpriteWidth / 2, ConstVar.animatedSpriteHeigth), new Vector2(0, 0), ConstVar.animatedCols, ConstVar.animatedFrame, ConstVar.animatedSpriteWidth, ConstVar.animatedSpriteHeigth);
            alice.lockDisplay = false;
            alice.setTilePos(47, 22, background);
            //aiBob = new AiMngnt(bob);


            //Inizializzo le finestre di testo
            ban = new Banner(_game, _graphics, _content, "button", new Vector2(ConstVar.displayDim.X /2, ConstVar.displayDim.Y /2),"Fonts/Font", "");
            gDebug = new gameDebug(_game, _graphics, _content);

            //Bottoni
            var helpButton = new Button(_game, _graphics, _content, "help-btn", new Vector2(ConstVar.displayDim.X - 120, 10), 0.2);
            helpButton.Click += Click_help;
            var exitButton = new Button(_game, _graphics, _content, "exit-btn", new Vector2(ConstVar.displayDim.X - 60, 10), 0.2);
            exitButton.Click += Click_exit;

            _buttons = new List<Button>()
              {
                helpButton,
                exitButton
              };

            //Bar
            barPlastica = new Bar(_game, _graphics, _content, "bars/plasticBar", "Plastica", new Vector2(10,0));
            barCarta = new Bar(_game, _graphics, _content, "bars/plasticBar", "Carta", new Vector2(10, 50));
            barVetro = new Bar(_game, _graphics, _content, "bars/plasticBar", "Vetro", new Vector2(10, 100));
            barSecco = new Bar(_game, _graphics, _content, "bars/plasticBar", "Secco", new Vector2(10, 150));
            barUmido = new Bar(_game, _graphics, _content, "bars/plasticBar", "Umido", new Vector2(10, 200));
            barSpeciale = new Bar(_game, _graphics, _content, "bars/plasticBar", "Speciale", new Vector2(10, 250));


            //Objects 
            allObjects = new List<Item>();
            allObjects.Add(new Item(_game, _graphics, _content, "oggetti/banana", new Vector2(0, 0), new Vector2(0, 0), 0.2, Item.Type.UMIDO, true));
            allObjects.Add(new Item(_game, _graphics, _content, "oggetti/foglie", new Vector2(0, 0), new Vector2(0, 0), 0.2, Item.Type.UMIDO, true));
            allObjects.Add(new Item(_game, _graphics, _content, "oggetti/latta", new Vector2(0, 0), new Vector2(0, 0), 0.2, Item.Type.PLASTICA, true));
            allObjects.Add(new Item(_game, _graphics, _content, "oggetti/lattina", new Vector2(0, 0), new Vector2(0, 0), 0.2, Item.Type.PLASTICA, true));
            allObjects.Add(new Item(_game, _graphics, _content, "oggetti/mela", new Vector2(0, 0), new Vector2(0, 0), 0.2, Item.Type.UMIDO, true));
            allObjects.Add(new Item(_game, _graphics, _content, "oggetti/giornale", new Vector2(0, 0), new Vector2(0, 0), 0.2, Item.Type.CARTA, true));
            allObjects.Add(new Item(_game, _graphics, _content, "oggetti/accendino", new Vector2(0, 0), new Vector2(0, 0), 0.2, Item.Type.SECCO, true));
            allObjects.Add(new Item(_game, _graphics, _content, "oggetti/bomboletta", new Vector2(0, 0), new Vector2(0, 0), 0.2, Item.Type.SECCO, true));
            allObjects.Add(new Item(_game, _graphics, _content, "oggetti/scatola-cartone", new Vector2(0, 0), new Vector2(0, 0), 0.2, Item.Type.CARTA, true));
            allObjects.Add(new Item(_game, _graphics, _content, "oggetti/pizza", new Vector2(0, 0), new Vector2(0, 0), 0.2, Item.Type.UMIDO, true));
            allObjects.Add(new Item(_game, _graphics, _content, "oggetti/bottiglia-vetro", new Vector2(0, 0), new Vector2(0, 0), 0.2, Item.Type.VETRO, true));
            allObjects.Add(new Item(_game, _graphics, _content, "oggetti/nucleare", new Vector2(0, 0), new Vector2(0, 0), 0.05, Item.Type.SPECIALE, true));

            objects = new List<Item>();

            //Load effect
            coinSound = _content.Load<SoundEffect>("soundEffect/coin-dropped");

            window = new Windows(_game, _graphics, _content, new Vector2(0, 0), "Finestra/finestra_daUsare(sSBP)", 1);

            _currentState = state1;
        }

        private void Character_Action(object sender, CharEventArgs e)
        {
            switch (e.a)
            {
                case 9775:
                    //premi space per aprire la finestra
                    ban.text = "Premi space per aprire la finestra";
                    ban.isVisible = true;
                    break;
                default:
                    space_button_action = null;
                    ban.isVisible = false;
                    break;
            }
        }

        private void Click_help(object sender, EventArgs e)
        {
             _game.Exit();
        }
        private void Click_exit(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            background.Draw();
            foreach (Item obj in objects)
                obj.Draw();

            foreach (var button in _buttons)
                button.Draw();

            alice.Draw();
            mainChar.Draw();
            barPlastica.Draw();
            barUmido.Draw();
            barSecco.Draw();
            barVetro.Draw();
            barSpeciale.Draw();
            barCarta.Draw();

            window.Draw();

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime, KeyboardState kbState)
        {
            keyboardMgnt(kbState, gameTime);
            background.update(mainChar);
            ban.update(gameTime);
            gDebug.Update(mainChar);
            mainChar.update(gameTime, background);
            alice.update(gameTime, background);

            if (!barPlastica.Update(mainChar.Inventory.Count(x => x.type == Item.Type.PLASTICA), plasticaTarget))
                plasticaTarget += plasticaTarget * 5;
            if (!barUmido.Update(mainChar.Inventory.Count(x => x.type == Item.Type.UMIDO), umidoTarget))
                umidoTarget += umidoTarget * 5;
            if (!barSecco.Update(mainChar.Inventory.Count(x => x.type == Item.Type.SECCO), seccoTarget))
                seccoTarget += seccoTarget * 5;
            if (!barVetro.Update(mainChar.Inventory.Count(x => x.type == Item.Type.VETRO), vetroTarget))
                vetroTarget += vetroTarget * 5;
            if (!barSpeciale.Update(mainChar.Inventory.Count(x => x.type == Item.Type.SPECIALE), specialeTarget))
                specialeTarget += specialeTarget * 5;
            if (!barCarta.Update(mainChar.Inventory.Count(x => x.type == Item.Type.CARTA), cartaTarget))
                cartaTarget += cartaTarget * 5;
            
            //bottoni
            foreach (var button in _buttons)
                button.update();

            // Spawn spazzatura
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timerAnimated -= elapsed;
            if (timerAnimated < 0)
            {
                timerAnimated = 100;
                int[,] walkableTile = (ConstVar.layers.Find(t => Equals(t.name, "street"))).tileMap;
                int x = rnd.Next(1, 77);
                int y = rnd.Next(1, 78);
                if(walkableTile[y, x] != 0 && !objects.Exists(b => b.getTilePos(background) == new Vector2(x, y)))
                {
                    Item obj = allObjects[rnd.Next(0, allObjects.Count())].Clone();
                    obj.setTilePos(x, y, background);
                    objects.Add(obj);
                }
            }

            for (int i = objects.Count; i > 0; i--)
            {
                objects[i - 1].Update(gameTime, background);
                if (objects[i - 1].getTilePos(background) == mainChar.getTilePos(background))
                {
                    coinSound.Play();
                    mainChar.collect(objects[i - 1]);
                    objects.RemoveAt(i - 1);
                }
            }

            _currentState(gameTime);
        }

        public void keyboardMgnt(KeyboardState kbState, GameTime gameTime)
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
                    _game.Exit();
                }
            }

            if (!KeyPressed.Contains(Keys.Space))
            {
                if (OldKeyPressed.Contains(Keys.Space) && ban.isVisible)
                {
                    ban.isVisible = false;
                    ban.isActive = false;
                    mainChar.move = false;
                    //_game.ChangeState(ConstVar.chooseBucket);
                    window.show = true;
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
         
        public void changeState(state nextState)
        {
            _currentState = nextState;
        }

        public void state0(GameTime gameTime)
        {

        }

        public void state1(GameTime gameTime)
        {
            alice.think(Character.thinkType.THINK);
            if(isAround(mainChar,alice))
            {
                space_button_action = delegate ()
                    {
                        alice.think(Character.thinkType.NONE);
                        changeState(state2);
                    };
            }
        }
        
        public void state2(GameTime gameTime)
        {
            space_button_action = null;
            if (speechIntroAlice.Count() != 0 && !alice.isSpeaking)
            {
                alice.speak(speechIntroAlice[0]);
                speechIntroAlice.RemoveAt(0);
            }
            if (speechIntroAlice.Count() == 0 && !alice.isSpeaking)
            {
                mainChar.think(Character.thinkType.ESCLAMATIVE);
                changeState(state3);
            }
        }
        
        public void state3(GameTime gameTime)
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
        };
    }
}


