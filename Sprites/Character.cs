using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Gioco_generico
{
    public class Character : AnimatedSprite
    {
        public enum walk { UP, DOWN, LEFT, RIGHT, NOP };
        public enum thinkType { QUESTION, ESCLAMATIVE, HEART, THINK, NONE};
        protected walk state;
        public bool move = true;
        public event EventHandler<CharEventArgs> Action;
        public int walkSpeed = 2;
        public bool collide;
        public bool lockDisplay = true;
        public bool isSpeaking;
        private bool isThinking;
        private CharEventArgs args;
        private List<Item> inventory = new List<Item>();
        private AnimatedSprite thinkingBubble;
        private AnimatedSprite esclamativeBubble;
        private AnimatedSprite questionBubble;
        private AnimatedSprite heartBubble;
        private AnimatedSprite Think;
        private SpeechBubble speechBubble;

        public Character(Game1 _game, GraphicsDeviceManager _graphics, ContentManager _content, String nameTex, Vector2 origin, Vector2 initGamePosition, int colums, int totFrames, int frameWidth, int frameHeight) : base(_game, _graphics, _content, nameTex, origin, initGamePosition, colums, totFrames, frameWidth, frameHeight)
        {
            thinkingBubble = new AnimatedSprite(_game, _graphics, _content, "bubble/bubble-thinking", new Vector2(0, 0), new Vector2(0, 0), 8, 8, 30, 30);
            esclamativeBubble = new AnimatedSprite(_game, _graphics, _content, "bubble/bubble-esclamative", new Vector2(0, 0), new Vector2(0, 0), 8, 8, 30, 30);
            questionBubble = new AnimatedSprite(_game, _graphics, _content, "bubble/bubble-question", new Vector2(0, 0), new Vector2(0, 0), 8, 8, 30, 30);
            heartBubble = new AnimatedSprite(_game, _graphics, _content, "bubble/bubble-heart", new Vector2(0, 0), new Vector2(0, 0), 8, 8, 30, 30);
            speechBubble = new SpeechBubble(_game, _graphics, _content, "bubble/bubble-speech", "Fonts/speechFont", 0.5);

            state = walk.NOP;
            isThinking = false;
            isSpeaking = false;
        }

        public void setAction(walk state)
        {
            this.state = state;
        }

        public void update(GameTime gameTime, Background background)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timerAnimated -= elapsed;
            thinkingBubble.Update(gameTime);
            heartBubble.Update(gameTime);
            esclamativeBubble.Update(gameTime);
            questionBubble.Update(gameTime);
            speechBubble.Update(gameTime, getPos());

            int[,] activeTile = (ConstVar.layers.Find(t => Equals(t.name, "active"))).tileMap;
            if (activeTile != null)
            {
                args = new CharEventArgs();
                if (activeTile[(int)getTilePos(background).Y, (int)getTilePos(background).X] != 0)
                {
                    args.a = activeTile[(int)getTilePos(background).Y, (int)getTilePos(background).X];
                }
                Action?.Invoke(this, args);
            }

            int[,] obstacleTile = (ConstVar.layers.Find(t => Equals(t.name, "obstacle"))).tileMap;
            if (move)
            {
                switch (state)
                {
                    case walk.UP:
                        if (obstacleTile != null)
                        {
                            if (obstacleTile[(int)getTilePos(background).Y - 1, (int)getTilePos(background).X] == 0 || gamePos.Y > background.tileDim * getTilePos(background).Y + background.tileDim / 2)
                            {
                                collide = false;
                                isRunning = true;
                                currentRow = 3;
                                if (lockDisplay)
                                {
                                    if (rect.Y > ConstVar.gameArea.Y || (gamePos.Y < ConstVar.gameArea.Y && rect.Y > 0))
                                        stepPos(0, -walkSpeed);
                                    if (!(gamePos.Y < 0))
                                        stepGamePos(0, -walkSpeed);
                                }
                                else
                                {
                                    stepGamePos(0, -walkSpeed);
                                }
                            }
                            else
                            {
                                collide = true;
                            }
                        }
                        else
                        {
                            isRunning = true;
                            currentRow = 3;
                            stepGamePos(0, -walkSpeed);
                        }
                        break;
                    case walk.DOWN:
                        if (obstacleTile != null)
                        {
                            if (obstacleTile[(int)getTilePos(background).Y + 1, (int)getTilePos(background).X] == 0 || gamePos.Y < background.tileDim * getTilePos(background).Y + background.tileDim * 0.9)
                            {
                                collide = false;
                                isRunning = true;
                                currentRow = 0;
                                if (lockDisplay)
                                {
                                    if (rect.Y < ConstVar.displayDim.Y - ConstVar.gameArea.Y || (gamePos.Y > background.getRect().Height - ConstVar.gameArea.Y && rect.Y < ConstVar.displayDim.Y))
                                        stepPos(0, walkSpeed);
                                    if (gamePos.Y < background.getRect().Height)
                                        stepGamePos(0, walkSpeed);
                                }
                                else
                                {
                                    stepGamePos(0, walkSpeed);
                                }
                            }
                            else
                            {
                                collide = true;
                            }
                        }
                        else
                        {
                            isRunning = true;
                            currentRow = 0;
                            stepGamePos(0, walkSpeed);
                        }
                        break;
                    case walk.LEFT:
                        if (obstacleTile != null)
                        {
                            if (obstacleTile[(int)getTilePos(background).Y, (int)getTilePos(background).X - 1] == 0 || gamePos.X > background.tileDim * getTilePos(background).X + background.tileDim / 2)
                            {
                                collide = false;
                                isRunning = true;
                                currentRow = 1;
                                if (lockDisplay)
                                {
                                    if (rect.X > ConstVar.gameArea.X || (gamePos.X < ConstVar.gameArea.X && rect.X > 0))
                                        stepPos(-walkSpeed, 0);
                                    if (!(gamePos.X < 0))
                                        stepGamePos(-walkSpeed, 0);
                                }
                                else
                                {
                                    stepGamePos(-walkSpeed, 0);
                                }
                            }
                            else
                            {
                                collide = true;
                            }
                        }
                        else
                        {
                            isRunning = true;
                            currentRow = 1;
                            stepGamePos(-walkSpeed, 0);
                        }
                        break;
                    case walk.RIGHT:
                        if (obstacleTile != null)
                        {
                            if (obstacleTile[(int)getTilePos(background).Y, (int)getTilePos(background).X + 1] == 0 || gamePos.X < background.tileDim * getTilePos(background).X + background.tileDim / 2)
                            {
                                collide = false;
                                isRunning = true;
                                currentRow = 2;
                                if (lockDisplay)
                                {
                                    if (rect.X < ConstVar.displayDim.X - ConstVar.gameArea.X ||
                                        (gamePos.X > background.getRect().Width - ConstVar.gameArea.X && rect.X < ConstVar.displayDim.X))
                                        stepPos(walkSpeed, 0);

                                    if (gamePos.X < background.getRect().Width)
                                        stepGamePos(walkSpeed, 0);
                                }
                                else
                                {
                                    stepGamePos(walkSpeed, 0);
                                }
                            }
                            else
                            {
                                collide = true;
                            }
                        }
                        else
                        {
                            isRunning = true;
                            currentRow = 2;
                            stepGamePos(walkSpeed, 0);
                        }
                        break;
                    case walk.NOP:
                        isRunning = false;
                        break;
                }
            }
            if (!lockDisplay)
            {
                rect.X = background.getRect().X + (int)gamePos.X;
                rect.Y = background.getRect().Y + (int)gamePos.Y;
            }
        }

        public void collect(Item obj)
        {
            inventory.Add(obj);
        }
        
        public new void Draw()
        {
            if (isThinking)
            {
                Think.isRunning = true;
                Think.setPos(rect.X, rect.Y - ConstVar.animatedSpriteHeigth - thinkingBubble.rect.Height);
                Think.Draw();
            }
            if (isSpeaking)
            {
                isSpeaking = speechBubble.Draw();
            }
            base.Draw();
        }
        public void think(Character.thinkType t)
        {
            switch (t) {
                case Character.thinkType.NONE:
                    isThinking = false;
                    break;
                case Character.thinkType.THINK:
                    Think = thinkingBubble;
                    isThinking = true;
                    break;
                case Character.thinkType.HEART:
                    Think = heartBubble;
                    isThinking = true;
                    break;
                case Character.thinkType.ESCLAMATIVE:
                    Think = esclamativeBubble;
                    isThinking = true;
                    break;
                case Character.thinkType.QUESTION:
                    Think = esclamativeBubble;
                    isThinking = true;
                    break;
            }
           
        }

        public void speak(string text)
        {
            speechBubble.Text = text;
            isSpeaking = true;
        }

        public List<Item> Inventory
        {
            get => inventory;
        }

    }

    public class CharEventArgs : EventArgs
    {
        public Character ch;
        public int a{ get; set; }
    }


    
}
