using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Recycle_game
{
    public class UI : Component
    {
        //Interaction
        List<Button> _buttons;
        public Narrator narrator;
        bool adviceEnable = false;
        public bool vocabularyEnable = false;
        public bool tutorialEnable = false;
        //Bar
        public Bar ScoreBar;
        public Bar InventoryBar;

        SoundEffect effectOpenWindow;

        //Advice bar
        float counter = 0;
        RasterizerState _rasterizerState = new RasterizerState() { ScissorTestEnable = true };
        bool turn = true;
        string text;
        static Vector2 dimAd = new Vector2(700, 40);
        static Vector2 posAd = new Vector2(ConstVar.displayDim.X / 2 - dimAd.X / 2, ConstVar.displayDim.Y * 0.9f);
        Rectangle scissRect = new Rectangle((int)(posAd.X), (int)posAd.Y, (int)dimAd.X, (int)dimAd.Y);
        Random rnd = new Random();

        private SoundEffect surround;
        SoundEffectInstance instance;

        public UI(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content) :  base(_game, _graphics, _content)
        {
            ScoreBar = new Bar(_game, _graphics, _content, "bars/genericBar", "Score", new Vector2(10, 10), Item.Type.NONE);
            InventoryBar = new Bar(_game, _graphics, _content, "bars/genericBar", "Inventory", new Vector2(10, 50), Item.Type.NONE);

            //Bottoni
            var helpButton = new Button(_game, _graphics, _content, "button/vocabulary_button", new Vector2(ConstVar.displayDim.X * 0.96f, ConstVar.displayDim.Y * 0.05f), Item.Type.NONE, 0.7f);
            helpButton.Action += Click_help;
            
            var tutorialButton = new Button(_game, _graphics, _content, "button/info_button", new Vector2(ConstVar.displayDim.X * 0.90f, ConstVar.displayDim.Y * 0.05f), Item.Type.NONE, 0.7f);
            tutorialButton.Action += Click_tutorial;
            _buttons = new List<Button>()
              {
                helpButton,
                tutorialButton
              };

            adviceEnable = true;
            //Narrator
            narrator = new Narrator(_game, _graphics, _content, "character/narrator", new Vector2(0, 0), new Vector2(ConstVar.displayDim.X * 0.08f, ConstVar.displayDim.Y * 0.85f));

            effectOpenWindow = _content.Load<SoundEffect>("soundEffect/effectOpenWindow");

            surround = _content.Load<SoundEffect>("soundEffect/surround");
            instance = surround.CreateInstance();
            //instance.Play();

        }

        public void Draw()
        {
            ScoreBar.Draw();
            InventoryBar.Draw();
            narrator.Draw();
            if (adviceEnable)
            {
                DrawAdviceBar();
            }

            foreach (var button in _buttons)
                button.Draw();

            if (vocabularyEnable && !tutorialEnable)
            {
                ConstVar.main.mainChar.move = false;
                ConstVar.vocabulary.Draw();
            }
            else
            {
                ConstVar.main.mainChar.move = true;
            }

            if (tutorialEnable && !vocabularyEnable)
            {
                ConstVar.main.mainChar.move = false;
                ConstVar.tutorial.Draw();
            }
            else
            {
                ConstVar.main.mainChar.move = true;
            }
        } 
        
        public void Update(GameTime gameTime)
        {
            //Bottoni
            foreach (var button in _buttons)
                button.update();

            ScoreBar.Update(gameTime, _game.Score, _game.GameLevel.POINT_TARGET);
            InventoryBar.Update(gameTime, ConstVar.main.mainChar.Inventory.Count, _game.GameLevel.FULL_INVENTORY);
            //Narratore
            narrator.Update(gameTime);

            //Vocabulary
            if(vocabularyEnable)
                ConstVar.vocabulary.Update();
            //Tutorial
            if(tutorialEnable)
                ConstVar.tutorial.Update();

            if (instance.State != SoundState.Playing)
            {
                instance.Play();
            }
        }

        private void Click_help(object sender, EventArgs e)
        {
            vocabularyEnable = !vocabularyEnable;
            
        }
        private void Click_tutorial(object sender, EventArgs e)
        {
            tutorialEnable = !tutorialEnable;
        }
        private void Click_exit(object sender, EventArgs e)
        {
            _game.Exit();
        }

        

        public void DrawAdviceBar()
        {
            ConstVar.sb.End();

            ConstVar.sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,null, null, _rasterizerState);
            Rectangle currentRect = ConstVar.sb.GraphicsDevice.ScissorRectangle;

            ConstVar.sb.GraphicsDevice.ScissorRectangle = scissRect;
            SpriteFont font = _content.Load<SpriteFont>("Fonts/Font");
            if (turn)
            {
                text = ConstVar.advices[rnd.Next(0,ConstVar.advices.Count)];
                turn = false;
            }
            else
            { 
                ConstVar.sb.DrawString(font, text, new Vector2(posAd.X + dimAd.X - counter, posAd.Y), Color.Black);
                if (counter < font.MeasureString(text).X + dimAd.X)
                {
                    counter += 1f;
                }
                else
                {
                    counter = 0;
                    turn = true;
                }
            }

            ConstVar.sb.GraphicsDevice.ScissorRectangle = currentRect;
            ConstVar.sb.End();

            ConstVar.sb.Begin();

        }
    }
}
