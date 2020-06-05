using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Gioco_generico.States;


namespace Gioco_generico
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


        //Ogni oggetto è 100 punti, ogni errore -50
        //LIVELLO 1 
        //LIVELLO 2 
        //LIVELLO 3 
        static ConstVar.LEVEL LEVEL3 = new ConstVar.LEVEL(200000, 2000, 20000, "Livello3", 30);
        static ConstVar.LEVEL LEVEL2 = new ConstVar.LEVEL(20000, 200, 2000, "Livello2", 20, LEVEL3);
        static ConstVar.LEVEL LEVEL1 = new ConstVar.LEVEL(500, 20, 200, "Livello1", 10, LEVEL2);
        
        

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
            
            base.Initialize();

        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            loadTMX("content/maps/mondo1.tmx", ConstVar.layers);
            GameLevel = LEVEL1;
            ConstVar.sb = spriteBatch;
            ConstVar.font = Content.Load<SpriteFont>("Fonts/font");
            ConstVar.menu = new MenuState(this, graphics, Content, 0);
            ConstVar.main = new MainState(this, graphics, Content, 1);
            ConstVar.chooseBucket = new ChooseBucket(this, graphics, Content, 2);
            ConstVar.UI = new UI(this, graphics, Content);
            _currentState = ConstVar.menu;
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
                GameLevel = GameLevel.NEXT_LEVEL;
        }
    }
}
