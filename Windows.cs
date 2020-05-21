﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Gioco_generico
{
    public class Windows : Component
    {
        public bool show;
        public List<Item> widget;
        Texture2D background;
        private List<Button> arrows;
        private List<Button> puzzle;
        Vector2 displayPos;
        private int indexCard;
        Rectangle rect;
        Texture2D cardNessunRifiuto;

        public Windows(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, Vector2 displayPos, string texture, double scale = 1) : base(_game, _graphics, _content)
        {
            this.displayPos = displayPos;   //finestra centrata
            background = _content.Load<Texture2D>(texture);
            rect = new Rectangle((int)displayPos.X, (int)displayPos.Y, (int)(background.Width * scale), (int)(background.Height * scale));

            widget = new List<Item>();

            indexCard = 0;

            //dichiarazioni bottoni frecce
            // var arrowLeft = new Button(_game, _graphics, _content, "Finestra/freccia_sinistraMax", new Vector2(ConstVar.displayDim.X - 1820, 990), 1);
            var arrowLeft = new Button(_game, _graphics, _content, "Finestra/freccia_sinistraMax", new Vector2((float)(rect.X + rect.Width * 0.1), (float)(rect.Y + rect.Height * 0.95)), 1);
            arrowLeft.Click += PreviousCard;
            var arrowRight = new Button(_game, _graphics, _content, "Finestra/freccia_destraMax", new Vector2((float)(rect.X + rect.Width * 0.25), (float)(rect.Y + rect.Height * 0.95)), 1);
            arrowRight.Click += NextCard;

            arrows = new List<Button>()
              {
                arrowLeft,
                arrowRight
              };

            //dichiarazioni bottoni puzzle
            var puzzleCarta = new Button(_game, _graphics, _content, "Finestra/puzzleCarta", new Vector2((float)(ConstVar.displayDim.X * 0.40625), (float)(ConstVar.displayDim.Y * 0.321759)), 1);
            puzzleCarta.Click += PressCarta;
            var puzzleVetro = new Button(_game, _graphics, _content, "Finestra/puzzleVetro", new Vector2((float)(ConstVar.displayDim.X * 0.561198), (float)(ConstVar.displayDim.Y * 0.321759)), 1);
            puzzleVetro.Click += PressVetro;
            var puzzleFarmaci = new Button(_game, _graphics, _content, "Finestra/puzzleFarmaci", new Vector2((float)(ConstVar.displayDim.X * 0.71484), (float)(ConstVar.displayDim.Y * 0.321759)), 1);
            puzzleFarmaci.Click += PressFarmaci;
            var puzzleAbiti = new Button(_game, _graphics, _content, "Finestra/puzzleAbiti", new Vector2((float)(ConstVar.displayDim.X * 0.869792), (float)(ConstVar.displayDim.Y * 0.321759)), 1);
            puzzleAbiti.Click += PressAbiti;
            var puzzleSecco = new Button(_game, _graphics, _content, "Finestra/puzzleSecco", new Vector2((float)(ConstVar.displayDim.X * 0.40625), (float)(ConstVar.displayDim.Y * 0.555556)), 1);
            puzzleSecco.Click += PressSecco;
            var puzzleOrganico = new Button(_game, _graphics, _content, "Finestra/puzzleOrganico", new Vector2((float)(ConstVar.displayDim.X * 0.561198), (float)(ConstVar.displayDim.Y * 0.555556)), 1);
            puzzleOrganico.Click += PressOrganico;
            var puzzleStradali = new Button(_game, _graphics, _content, "Finestra/puzzleStradali", new Vector2((float)(ConstVar.displayDim.X * 0.71484), (float)(ConstVar.displayDim.Y * 0.555556)), 1);
            puzzleStradali.Click += PressStradali;
            var puzzleBatterie = new Button(_game, _graphics, _content, "Finestra/puzzleBatterie", new Vector2((float)(ConstVar.displayDim.X * 0.869792), (float)(ConstVar.displayDim.Y * 0.555556)), 1);
            puzzleBatterie.Click += PressBatterie;
            var puzzlePlastMetal = new Button(_game, _graphics, _content, "Finestra/puzzlePlastMetal", new Vector2((float)(ConstVar.displayDim.X * 0.483073), (float)(ConstVar.displayDim.Y * 0.789352)), 1);
            puzzlePlastMetal.Click += PressPlastMetal;
            var puzzleToner = new Button(_game, _graphics, _content, "Finestra/puzzleToner", new Vector2((float)(ConstVar.displayDim.X * 0.71484), (float)(ConstVar.displayDim.Y * 0.789352)), 1);
            puzzleToner.Click += PressToner;
            var puzzleCentroRac = new Button(_game, _graphics, _content, "Finestra/puzzleCentroRac", new Vector2((float)(ConstVar.displayDim.X * 0.869792), (float)(ConstVar.displayDim.Y * 0.789352)), 1);
            puzzleCentroRac.Click += PressCentroRac;

            puzzle = new List<Button>()
              {
                puzzleCarta,
                puzzleVetro,
                puzzleFarmaci,
                puzzleAbiti,
                puzzleSecco,
                puzzleOrganico,
                puzzleStradali,
                puzzleBatterie,
                puzzlePlastMetal,
                puzzleToner,
                puzzleCentroRac
              };

            show = false;

            cardNessunRifiuto = _content.Load<Texture2D>("Finestra/scheda_nessunRifiuto");
        }

        public void Draw()
        {
            if (show == true)
            {
                ConstVar.sb.Draw(background, rect, null, Color.White, 0, new Vector2(0, 0), new SpriteEffects(), 0);
                foreach (var button1 in arrows)
                    button1.Draw();
                foreach (var button2 in puzzle)
                    button2.Draw();

                if (widget.Count == 0)
                {
                    ConstVar.sb.Draw(cardNessunRifiuto, new Rectangle((int)((ConstVar.displayDim.X - 1820) * 1), (int)((ConstVar.displayDim.Y - 860) * 1), cardNessunRifiuto.Width, cardNessunRifiuto.Height), null, Color.White);
                }
                else
                {
                    widget[indexCard].DrawCard(0.052f, 0.203f);
                }
            }

            
        }

        public void Update(GameTime gameTime)
        {
            foreach (var button in arrows)
                button.update();
            foreach (var button in puzzle)
                button.update();
        }
        /*public void windowDraw()
        {
            if (show == true)
            {
                ConstVar.sb.Draw(background, rect, null, Color.White, 0, new Vector2(0, 0), new SpriteEffects(), 0);
                //widget[indexCard].DrawCard(1, 1);
                foreach (var button1 in arrows)
                    button1.Draw();
                foreach (var button2 in puzzle)
                    button2.Draw();

                if (widget.Count == 0)
                {
                    ConstVar.sb.Draw(cardNessunRifiuto, new Rectangle((int)((ConstVar.displayDim.X - 1820) * 1), (int)((ConstVar.displayDim.Y - 860) * 1), cardNessunRifiuto.Width, cardNessunRifiuto.Height), null, Color.White);
                }


            }
        }*/

        private void PreviousCard(object sender, EventArgs e)
        {
            //widget[indexCard - 1].DrawCard(1, 1);
            if(indexCard > 0)
                indexCard--;
        }

        private void NextCard(object sender, EventArgs e)
        {
            //widget[indexCard + 1].DrawCard(1, 1);
            if(indexCard < widget.Count - 1)
                indexCard++;
        }

        private void PressCarta(object sender, EventArgs e)
        {

        }

        private void PressVetro(object sender, EventArgs e)
        {

        }

        private void PressFarmaci(object sender, EventArgs e)
        {

        }

        private void PressAbiti(object sender, EventArgs e)
        {

        }

        private void PressSecco(object sender, EventArgs e)
        {

        }

        private void PressOrganico(object sender, EventArgs e)
        {

        }

        private void PressStradali(object sender, EventArgs e)
        {

        }

        private void PressBatterie(object sender, EventArgs e)
        {

        }

        private void PressPlastMetal(object sender, EventArgs e)
        {

        }

        private void PressToner(object sender, EventArgs e)
        {

        }

        private void PressCentroRac(object sender, EventArgs e)
        {

        }
    }
}
