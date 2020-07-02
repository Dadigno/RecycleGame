﻿using Recycle_game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recycle_game
{
    public class Tutorial : Component
    {
        Sprite background;

        //Button
        private List<Button> arrows;
        private Button start;

        //Scritte
        SpriteFont font;
        SpriteFont titlefont;

        //Sound effect
        SoundEffect effectScrollPage;

        //Draw page
        delegate void DrawPage();
        private DrawPage _DrawPage;

        public Tutorial(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content) : base(_game, _graphics, _content)
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

            start = new Button(_game, _graphics, _content, "button/play_button", new Vector2(background.getPos().X, background.getPos().Y + background.getRect().Height * 0.45f), Item.Type.NONE, 0.80);
            start.Action += NewGame;

            effectScrollPage = _content.Load<SoundEffect>("soundEffect/turnPage");

            titlefont = _content.Load<SpriteFont>("Fonts/bigFont");
            font = _content.Load<SpriteFont>("Fonts/fontSimbol");
            _DrawPage = DrawPage1;

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
            start.update();
        }


        private void PreviousPage(object sender, EventArgs e)
        {
            arrows[1].Enable = true;
            arrows[0].Enable = true;
            if (_DrawPage == DrawPage3)
            {
                effectScrollPage.Play();
                _DrawPage = DrawPage2;
            }
            else if (_DrawPage == DrawPage2)
            {
                effectScrollPage.Play();
                _DrawPage = DrawPage1;
                arrows[0].Enable = false;
            }
            else if (_DrawPage == DrawPage4)
            {
                effectScrollPage.Play();
                _DrawPage = DrawPage3;

            }
            else if (_DrawPage == DrawPage5)
            {
                effectScrollPage.Play();
                _DrawPage = DrawPage4;
            }
        }

        private void NextPage(object sender, EventArgs e)
        {
            arrows[1].Enable = true;
            arrows[0].Enable = true;
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
            else if (_DrawPage == DrawPage4)
            {
                effectScrollPage.Play();
                _DrawPage = DrawPage5;
                arrows[1].Enable = false;
            }
        }

        void DrawPage1()
        {
            foreach (var button in arrows)
                button.Draw();

            string title = "Title 1";
            ConstVar.sb.DrawString(titlefont, title, new Vector2(background.getPos().X - titlefont.MeasureString(title).X / 2, background.getPos().Y - background.getRect().Height * 0.43f), Color.Black);

            StaticDraw(page1);
        }
        void DrawPage2()
        {
            foreach (var button in arrows)
                button.Draw();

            string title = "Title 2";
            ConstVar.sb.DrawString(titlefont, title, new Vector2(background.getPos().X - titlefont.MeasureString(title).X / 2, background.getPos().Y - background.getRect().Height * 0.43f), Color.Black);

            StaticDraw(page2);
        }
        void DrawPage3()
        {
            foreach (var button in arrows)
                button.Draw();

            string title = "Title 3";
            ConstVar.sb.DrawString(titlefont, title, new Vector2(background.getPos().X - titlefont.MeasureString(title).X / 2, background.getPos().Y - background.getRect().Height * 0.43f), Color.Black);

            StaticDraw(page3);
        }
        void DrawPage4()
        {
            foreach (var button in arrows)
                button.Draw();

            string title = "Title 4";
            ConstVar.sb.DrawString(titlefont, title, new Vector2(background.getPos().X - titlefont.MeasureString(title).X / 2, background.getPos().Y - background.getRect().Height * 0.43f), Color.Black);
            StaticDraw(page4);
        }
        void DrawPage5()
        {
            foreach (var button in arrows)
                button.Draw();
            start.Draw();

            string title = "Title 5";
            ConstVar.sb.DrawString(titlefont, title, new Vector2(background.getPos().X - titlefont.MeasureString(title).X / 2, background.getPos().Y - background.getRect().Height * 0.43f), Color.Black);
            StaticDraw(page5);
        }

        public void StaticDraw(string text)
        {
            int row = 1;
            string temp = "";
            List<char> l = text.ToList<char>();
            while (l.Count() > 0)
            {
                temp += l[0];
                float a = font.MeasureString(temp.Replace("\r\n", "")).X /( background.getRect().Width * 0.90f );
                if (a > row)
                {
                    if (l[0] != ' ' && l[1] != ' ')
                    {
                        temp += "           ";
                    }
                    if (l[1] == ' ')
                    {
                        l.RemoveAt(1);
                    }

                    temp += Environment.NewLine;
                    row++;
                }
                l.RemoveAt(0);
            }
            row = 1;
            ConstVar.sb.DrawString(font, temp, new Vector2(background.getRect().X - background.getRect().Width * 0.45f, background.getRect().Y - background.getRect().Width * 0.45f), Color.Black);
        }

        string page1 = "";
        string page2 = "";
        string page3 = "";
        string page4 = "";
        string page5 = "";


        private void NewGame(object sender, EventArgs e)
        {
            _game.ChangeState(ConstVar.main);
        }
    }
}


