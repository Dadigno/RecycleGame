﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Gioco_generico
{
    public class Windows : Component
    {
        public bool show;
        public List<Item> widget;
        Texture2D background;
        private List<Button> arrows;
        //private List<Button> puzzle;
        private List<Button> puzzle1;
        private List<Button> puzzle2;
        private List<Button> puzzle3;
        Vector2 displayPos;
        private int indexCard;
        Rectangle rect;
        Texture2D cardNessunRifiuto;
        Texture2D cardPross1;
        Texture2D cardPross2;
        public enum Answer { NONE, CORRECT, ERROR};
        Answer answer = Answer.NONE;
        static float BLINK_TIME = 250;
        float timer = 0;
        bool blinkCorrect = false;
        bool blinkError = false;
        Texture2D contornoRosso;
        Texture2D contornoVerde;
        int blinkCounter = 0;
        private SoundEffect effectAnswerCorrect;
        private SoundEffect effectAnswerWrong;
        private SoundEffect effectScrollArrow;

        public Windows(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, Vector2 displayPos, string texture, double scale = 1) : base(_game, _graphics, _content)
        {
            this.displayPos = displayPos;   //finestra centrata
            background = _content.Load<Texture2D>(texture);
            rect = new Rectangle((int)displayPos.X, (int)displayPos.Y, (int)(background.Width * scale), (int)(background.Height * scale));

            widget = new List<Item>();

            indexCard = 0;

            //dichiarazioni bottoni frecce
            var arrowLeft = new Button(_game, _graphics, _content, "Finestra/freccia_sinistraMax", new Vector2((float)(rect.X + rect.Width * 0.1), (float)(rect.Y + rect.Height * 0.95)));
            arrowLeft.Action += PreviousCard;
            var arrowRight = new Button(_game, _graphics, _content, "Finestra/freccia_destraMax", new Vector2((float)(rect.X + rect.Width * 0.25), (float)(rect.Y + rect.Height * 0.95)));
            arrowRight.Action += NextCard;

            arrows = new List<Button>()
              {
                arrowLeft,
                arrowRight
              };

            //dichiarazioni bottoni puzzle
            var puzzleCarta = new Button(_game, _graphics, _content, "Finestra/puzzleCarta", new Vector2((float)(ConstVar.displayDim.X * 0.40625), (float)(ConstVar.displayDim.Y * 0.321759)), Item.Type.CARTA);
            puzzleCarta.Action += ButtonAction;
            var puzzleVetro = new Button(_game, _graphics, _content, "Finestra/puzzleVetro", new Vector2((float)(ConstVar.displayDim.X * 0.561198), (float)(ConstVar.displayDim.Y * 0.321759)), Item.Type.VETRO);
            puzzleVetro.Action += ButtonAction;
            var puzzleFarmaci = new Button(_game, _graphics, _content, "Finestra/puzzleFarmaci", new Vector2((float)(ConstVar.displayDim.X * 0.71484), (float)(ConstVar.displayDim.Y * 0.321759)), Item.Type.FARMACI);
            puzzleFarmaci.Action += ButtonAction;
            var puzzleAbiti = new Button(_game, _graphics, _content, "Finestra/puzzleAbiti", new Vector2((float)(ConstVar.displayDim.X * 0.869792), (float)(ConstVar.displayDim.Y * 0.321759)), Item.Type.ABITI);
            puzzleAbiti.Action += ButtonAction;
            var puzzleSecco = new Button(_game, _graphics, _content, "Finestra/puzzleSecco", new Vector2((float)(ConstVar.displayDim.X * 0.40625), (float)(ConstVar.displayDim.Y * 0.555556)), Item.Type.SECCO);
            puzzleSecco.Action += ButtonAction;
            var puzzleOrganico = new Button(_game, _graphics, _content, "Finestra/puzzleOrganico", new Vector2((float)(ConstVar.displayDim.X * 0.561198), (float)(ConstVar.displayDim.Y * 0.555556)), Item.Type.ORGANICO);
            puzzleOrganico.Action += ButtonAction;
            var puzzleStradali = new Button(_game, _graphics, _content, "Finestra/puzzleStradali", new Vector2((float)(ConstVar.displayDim.X * 0.71484), (float)(ConstVar.displayDim.Y * 0.555556)), Item.Type.STRADALI);
            puzzleStradali.Action += ButtonAction;
            var puzzleBatterie = new Button(_game, _graphics, _content, "Finestra/puzzleBatterie", new Vector2((float)(ConstVar.displayDim.X * 0.869792), (float)(ConstVar.displayDim.Y * 0.555556)), Item.Type.BATTERIE);
            puzzleBatterie.Action += ButtonAction;
            var puzzlePlastMetal = new Button(_game, _graphics, _content, "Finestra/puzzlePlastMetal", new Vector2((float)(ConstVar.displayDim.X * 0.483073), (float)(ConstVar.displayDim.Y * 0.789352)), Item.Type.PLASTICA_MET);
            puzzlePlastMetal.Action += ButtonAction;
            var puzzleToner = new Button(_game, _graphics, _content, "Finestra/puzzleToner", new Vector2((float)(ConstVar.displayDim.X * 0.71484), (float)(ConstVar.displayDim.Y * 0.789352)), Item.Type.TONER);
            puzzleToner.Action += ButtonAction;
            var puzzleCentroRac = new Button(_game, _graphics, _content, "Finestra/puzzleCentroRac", new Vector2((float)(ConstVar.displayDim.X * 0.869792), (float)(ConstVar.displayDim.Y * 0.789352)), Item.Type.CENTRORACCOLTA);
            puzzleCentroRac.Action += ButtonAction;

            puzzle1 = new List<Button>()
              {
                puzzleCarta,
                puzzleVetro,
                puzzleSecco,
                puzzleOrganico,
                puzzlePlastMetal
              };

            puzzle2 = new List<Button>()
              { puzzleFarmaci,
                puzzleAbiti,
                puzzleStradali,
                puzzleBatterie,
                puzzleToner
              };

            puzzle3 = new List<Button>()
              {
                puzzleCentroRac
              };

            show = false;

            cardNessunRifiuto = _content.Load<Texture2D>("Finestra/scheda_nessunRifiuto");
            cardPross1 = _content.Load<Texture2D>("Finestra/Pross_Livello1");
            cardPross2 = _content.Load<Texture2D>("Finestra/Pross_Livello2");
            contornoRosso = _content.Load<Texture2D>("Finestra/contornoRosso");
            contornoVerde = _content.Load<Texture2D>("Finestra/contornoVerde");

            effectAnswerCorrect = _content.Load<SoundEffect>("soundEffect/effectAnswerCorrect");
            effectAnswerWrong = _content.Load<SoundEffect>("soundEffect/effectAnswerWrong");
            effectScrollArrow = _content.Load<SoundEffect>("soundEffect/effectScrollArrow");
        }

        public void Draw()
        {
            if (show == true)
            {
                ConstVar.sb.Draw(background, rect, null, Color.White, 0, new Vector2(0, 0), new SpriteEffects(), 0);
                if (widget.Count == 0)
                {
                    ConstVar.sb.Draw(cardNessunRifiuto, new Rectangle((int)((ConstVar.displayDim.X - 1820) * 1), (int)((ConstVar.displayDim.Y - 860) * 1), cardNessunRifiuto.Width, cardNessunRifiuto.Height), null, Color.White);
                }
                else
                {
                    widget[indexCard].DrawCard(0.052f, 0.203f);
                }


                if(blinkError)
                    ConstVar.sb.Draw(contornoRosso, rect, null, Color.White, 0, new Vector2(0, 0), new SpriteEffects(), 0);
                if(blinkCorrect)
                    ConstVar.sb.Draw(contornoVerde, rect, null, Color.White, 0, new Vector2(0, 0), new SpriteEffects(), 0);



                foreach (var button1 in arrows)
                    button1.Draw();
                

                if (_game.GameLevel.name == "Livello1")
                {
                    Draw1();
                }
                else if (_game.GameLevel.name == "Livello2")
                {
                    Draw2();
                }
                else if (_game.GameLevel.name == "Livello3")
                {
                    Draw3();
                }
                ConstVar.UI.Draw();
               
            }

            
        }
        
        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (answer != Answer.NONE)
            {
                timer -= elapsed;
                if (timer < 0)
                {
                    timer = BLINK_TIME;
                    if (blinkCounter < 4)
                    {
                        if (answer == Answer.CORRECT)
                        {
                            blinkCorrect = !blinkCorrect;
                        }
                        else if (answer == Answer.ERROR)
                        {
                            blinkError = !blinkError;
                        }
                        else if (answer == Answer.NONE)
                        {
                            blinkCorrect = false;
                            blinkError = false;
                        }

                        blinkCounter++;
                    }
                    else
                    {
                        blinkCounter = 0;
                        answer = Answer.NONE;
                    }
                }
            }


            foreach (var button in arrows)
            button.update();

            if (_game.GameLevel.name == "Livello1")
            {
                foreach (var button in puzzle1)
                    button.update();
            }
            else if (_game.GameLevel.name == "Livello2")
            {
                foreach (var button in puzzle1)
                    button.update();
                foreach (var button in puzzle2)
                    button.update();
            }
            else if (_game.GameLevel.name == "Livello3")
            {
                foreach (var button in puzzle1)
                    button.update();
                foreach (var button in puzzle2)
                    button.update();
                foreach (var button in puzzle3)
                    button.update();

            }

            ConstVar.UI.Update(gameTime);
        }

        private void PreviousCard(object sender, EventArgs e)
        {
            if(indexCard > 0)
                indexCard--;
            effectScrollArrow.Play();
        }

        private void NextCard(object sender, EventArgs e)
        {
            if(indexCard < widget.Count - 1)
                indexCard++;
            effectScrollArrow.Play();
        }

        private void ButtonAction(object sender, ButtEventArgs e)
        {
            if (widget.Count > 0)
            {
                if (widget[indexCard].type == e.T)
                {
                    answer = Answer.CORRECT;
                    effectAnswerCorrect.Play();
                    _game.Score += _game.GameLevel.POINT;
                    widget.RemoveAt(indexCard);
                    if (indexCard == widget.Count && widget.Count != 0)
                    {
                        indexCard -= 1;
                    }
                }
                else
                {
                    answer = Answer.ERROR;
                    effectAnswerWrong.Play();
                    if (_game.Score < _game.GameLevel.POINT)
                    {
                        _game.Score = 0;
                    }
                    else
                    {
                        _game.Score -= _game.GameLevel.POINT;
                    }
                }
            }
        }


        
        private void Draw1()
        {
            foreach (var button2 in puzzle1)
                button2.Draw();

            ConstVar.sb.Draw(cardPross1, new Rectangle((int)((ConstVar.displayDim.X - 695) * 1), (int)((ConstVar.displayDim.Y - 860) * 1), cardPross1.Width, cardPross1.Height), null, Color.White);

        }

        private void Draw2()
        {
            foreach (var button2 in puzzle1)
                button2.Draw();
            foreach (var button3 in puzzle2)
                button3.Draw();

            ConstVar.sb.Draw(cardPross2, new Rectangle((int)((ConstVar.displayDim.X - 400) * 1), (int)((ConstVar.displayDim.Y - 355) * 1), cardPross2.Width, cardPross2.Height), null, Color.White);
        }

        private void Draw3()
        {
            foreach (var button2 in puzzle1)
                button2.Draw();
            foreach (var button3 in puzzle2)
                button3.Draw();
            foreach (var button4 in puzzle3)
                button4.Draw();

        }
    }
}
