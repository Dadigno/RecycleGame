using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Recycle_game.States;
using Recycle_game.Interaction;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Recycle_game;

namespace Recycle_game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    
    public partial class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ConfigFile settingFile;
        private State _currentState;
        private State _nextState;
        private SoundEffect effectLevelUp;
        private SoundEffect effectMissionCompleted;
        private bool varStopEffect;


        ConstVar.LEVEL LEVEL1;
        ConstVar.LEVEL LEVEL2;
        ConstVar.LEVEL LEVEL3;

        public List<ConstVar.LEVEL> LEVELS;
        public ConstVar.LEVEL GameLevel;
        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                if(_score < GameLevel.POINT_TARGET)
                {
                    _score = value;
                }
            }
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        protected override void Initialize()
        {
            settingFile = new ConfigFile("Settings.ini");

            graphics.PreferredBackBufferWidth = (int)ConstVar.displayDim.X;
            graphics.PreferredBackBufferHeight = (int)ConstVar.displayDim.Y;

            graphics.IsFullScreen = false;
            IsMouseVisible = true;
            graphics.ApplyChanges();

            LEVEL3 = new ConstVar.LEVEL(200000, 2000, 20000, "Livello3", 30, new List<Item.Type>() { Item.Type.CENTRORACCOLTA }, null, LEVEL2);
            LEVEL2 = new ConstVar.LEVEL(20000, 200, 2000, "Livello2", 20, new List<Item.Type>() { Item.Type.ABITI, Item.Type.OLIOSPECIFICO, Item.Type.TONER, Item.Type.FARMACI }, LEVEL3, LEVEL1);
            LEVEL1 = new ConstVar.LEVEL(2000, 20, 200, "Livello1", 10, new List<Item.Type>() { Item.Type.ORGANICO, Item.Type.SECCO, Item.Type.VETRO, Item.Type.PLASTICA_MET, Item.Type.CARTA }, LEVEL2);

            LEVELS = new List<ConstVar.LEVEL>();
            LEVELS.Add(LEVEL1);
            LEVELS.Add(LEVEL2);
            LEVELS.Add(LEVEL3);
            varStopEffect = true;

            base.Initialize();

        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            loadTMX("content/maps/mondo.tmx", ConstVar.layers);
            loadAdvices();
            loadNarratorText();
            
            GameLevel = LEVEL1;
            ConstVar.sb = spriteBatch;
            ConstVar.font = Content.Load<SpriteFont>("Fonts/font");
            ConstVar.menu = new MenuState(this, graphics, Content, 0);
            ConstVar.main = new MainState(this, graphics, Content, 1);
            ConstVar.chooseBucket = new ChooseBucket(this, graphics, Content, 2);
            ConstVar.tutorialState = new TutorialState(this, graphics, Content, 3);

            ConstVar.UI = new UI(this, graphics, Content);
            ConstVar.vocabulary = new Vocabulary(this, graphics, Content);
            ConstVar.tutorial = new Tutorial(this, graphics, Content);
            _currentState = ConstVar.menu;
            effectLevelUp = Content.Load<SoundEffect>("soundEffect/effectLevelUp");
            effectMissionCompleted = Content.Load<SoundEffect>("soundEffect/effectMissionCompleted");
        }

     
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }
            KeyboardState kbState = Keyboard.GetState();
            _currentState.Update(gameTime, kbState);


            HandleLevel();
            if (Score >= 200000 & varStopEffect == true)
            {
                effectMissionCompleted.Play();
                varStopEffect = false;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            _currentState.Draw(gameTime, spriteBatch);
            
            base.Draw(gameTime);
        }


        void HandleLevel()
        {
            if (Score >= GameLevel.POINT_TARGET && GameLevel.NEXT_LEVEL != null)
            {
                GameLevel = GameLevel.NEXT_LEVEL;
                effectLevelUp.Play();
            }
        }
    }
}
