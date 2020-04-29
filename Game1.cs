using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
            ConstVar.sb = spriteBatch;
            ConstVar.font = Content.Load<SpriteFont>("Fonts/font");
            ConstVar.menu = new MenuState(this, graphics, Content, 0);
            ConstVar.main = new MainState(this, graphics, Content, 1);
            //ConstVar.house = new InsideState(this, graphics, Content, 2);
            ConstVar.chooseBucket = new ChooseBucket(this, graphics, Content, 3);
            _currentState = ConstVar.main;
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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            _currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
