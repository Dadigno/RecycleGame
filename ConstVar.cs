using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Recycle_game.States;
using System.Runtime.CompilerServices;
using Recycle_game.Interaction;

namespace Recycle_game
{
    public static class ConstVar
    {
        public static SpriteBatch sb;

        //game
        public static Vector2 displayDim = new Vector2(1920, 1080);
        public static double ratioVideo = displayDim.X / displayDim.Y;
        public static Vector2 gameArea = new Vector2(displayDim.X / 3, displayDim.Y / 3);

        
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
        public static ChooseBucket chooseBucket;
        public static TutorialState tutorialState;
        public static List<Game1.layer> layers = new List<Game1.layer>();
        public static List<Game1.layer> layersInside = new List<Game1.layer>();
        public static List<string> advices = new List<string>();
        public static List<string> narratorScript = new List<string>();
        public static List<Item> allObjects = new List<Item>();
        //User interface
        public static UI UI;

        public static Vocabulary vocabulary;
        public static Tutorial tutorial;

        public class LEVEL
        {
            //livello di partenza del gioco
            public string name;
            public int FULL_INVENTORY;
            public int POINT_TARGET;
            public int POINT_ERROR;
            public int POINT;
            public LEVEL NEXT_LEVEL;
            public List<Item.Type> OBJ_TYPES;
            public LEVEL(int t, int e, int p, string s, int full_inv, List<Item.Type> obj_types, LEVEL next = null)
            {
                name = s;
                POINT_TARGET = t;
                POINT_ERROR = e;
                POINT = p;
                NEXT_LEVEL = next;
                FULL_INVENTORY = full_inv;
                OBJ_TYPES = obj_types;
            }
        }

        public class Result
        {
            public int numeroRipsosteCorrette;
            public int numeroRisposteSbagliate;

            public Result (int nRC, int NRE)
            {
                numeroRipsosteCorrette = nRC;
                numeroRisposteSbagliate = NRE;
            }
        }

        public class Symbol
        {
            public Sprite symbol;
            public string description;

            public Symbol(Sprite s, string t)
            {
                description = t;
                symbol = s;
            }
        }
    }
}
