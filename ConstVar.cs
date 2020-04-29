using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RecycleGame.States;
using static RecycleGame.Game1;

namespace RecycleGame
{
    public static class ConstVar
    {
        public static SpriteBatch sb;

        //game
        //public static Vector2 displayDim = new Vector2(1600, 900);
        public static Vector2 displayDim;
        //public static double ratioVideo = displayDim.X / displayDim.Y;
        public static Vector2 gameArea;

        
        //public static Vector2 mapDim = new Vector2(2526, 2526);
        //animation
        public static AnimatedSprite animatedSprite;
        public static Texture2D animatedSpriteText;

        //public static int animatedSpriteWidth = 64;
        //public static int animatedSpriteHeigth = 64;
        public static int animatedSpriteWidth = 32;
        public static int animatedSpriteHeigth = 58;
        //public static int animatedSpriteHeigth = 32;
        public static int animatedCols = 4;
        public static int animatedFrame = 16;

        
       

        public static float TIMER_BANNER = 1000;
        //font
        public static SpriteFont font;
        public static AnimatedSprite fontSprite;
        public static Texture2D fontSpriteText;
        public static int fontSpriteWidth = 19;
        public static int fontSpriteHeigth = 20;
        public static int fontCols = 11;
        public static int fontFrame = 88;

        //States
        public static MainState main;
        public static MenuState menu;

        public static List<layer> layers = new List<layer>();
        public static List<layer> layersInside = new List<layer>();

        
    }

    public enum Button_type { HELP, CLOSE, NEWGAME };

}
