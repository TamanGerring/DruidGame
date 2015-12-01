using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using System;

namespace Game1
{
    public enum GameAction
    {
        None,
        Quit,
        PlayerRight,
        PlayerLeft,
        PlayerUp,
        PlayerDown
    }


    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

        private const int CELL_WIDTH = 64;
        private const int CELL_HEIGHT = 64;

        // set the map size in cells
        private const int MAP_CELL_ROW_COUNT = 8;
        private const int MAP_CELL_COLUMN_COUNT = 10;

        // set the window size
        private const int WINDOW_WIDTH = MAP_CELL_COLUMN_COUNT * CELL_WIDTH;
        private const int WINDOW_HEIGHT = MAP_CELL_ROW_COUNT * CELL_HEIGHT;

        private const int SCORE_X_POSITION = 500;
        private const int SCORE_Y_POSITION = 20;

        private const int SCORE_TO_WIN = 10;

        private const double TIME_LIMIT = 100;
        
        // variable for score
        private int score;

        //variable for timer
        private double timer = TIME_LIMIT;

        // SpriteFont for on-screen score
        private SpriteFont scoreFont;

        GameAction playerKeyPress;

        KeyboardState newState;
        KeyboardState oldState;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Druid druid;
        private PenaltyObject penalty;
        private PointObject point;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = MAP_CELL_COLUMN_COUNT * CELL_WIDTH;
            graphics.PreferredBackBufferHeight = MAP_CELL_ROW_COUNT * CELL_HEIGHT;
            
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // set the background's initial position
            //_backgroundPosition = new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

            Random rand = new Random();

            druid = new Druid(Content, "druid_right", 32, new Vector2(50, 400));
            druid.Active = true;

            penalty = new PenaltyObject(Content, "penalty_object", 13, new Vector2(rand.Next(0, WINDOW_WIDTH - 13), rand.Next(0, WINDOW_HEIGHT - 13)));
            penalty.Active = true;

            point = new PointObject(Content, "point_object", 13, new Vector2(rand.Next(0, WINDOW_WIDTH - 13), rand.Next(0, WINDOW_HEIGHT - 13)));
            point.Active = true;
            

            IsMouseVisible = true;

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            scoreFont = Content.Load<SpriteFont>("ScoreFont");

            // TODO: use this.Content to load your game content here
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

      
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            

            if ((score != SCORE_TO_WIN) && (timer > 0))
            {
                playerKeyPress = GetKeyboardEvents();

                GetPlayerAction(playerKeyPress);

                base.Update(gameTime);
            }
            else if (score == SCORE_TO_WIN)
            {
                DisplayWinScreen();
            }
            else if (timer <= 0)
            {
                DisplayTimeOutMessage();
            }
            
        }

        private void GetPlayerAction(GameAction playerKeyPress)
        {
            switch (playerKeyPress)
            {
                case GameAction.None:
                    break;
                case GameAction.Quit:
                    Exit();
                    break;
                case GameAction.PlayerRight:
                    druid.DruidDirection = Druid.Direction.Right;
                    druid.Position = new Vector2(druid.Position.X + 2, druid.Position.Y);
                    break;
                case GameAction.PlayerLeft:
                    druid.DruidDirection = Druid.Direction.Left;
                    druid.Position = new Vector2(druid.Position.X - 2, druid.Position.Y);
                    break;
                case GameAction.PlayerUp:
                    druid.DruidDirection = Druid.Direction.Up;
                    druid.Position = new Vector2(druid.Position.X, druid.Position.Y - 2);
                    break;
                case GameAction.PlayerDown:
                    druid.DruidDirection = Druid.Direction.Down;
                    druid.Position = new Vector2(druid.Position.X, druid.Position.Y + 2);
                    break;
                default:
                    break;
            }
        }

        private GameAction GetKeyboardEvents()
        {
            GameAction playerKeyPress = GameAction.None;

            newState = Keyboard.GetState();

            if (KeyCheck(Keys.Right) == true && offScreenCheck("RIGHT") == true)
            {
                playerKeyPress = GameAction.PlayerRight;
            }
            else if (KeyCheck(Keys.Left) == true && offScreenCheck("LEFT") == true)
            {
                playerKeyPress = GameAction.PlayerLeft;
            }
            else if (KeyCheck(Keys.Up) == true && offScreenCheck("UP") == true)
            {
                playerKeyPress = GameAction.PlayerUp;
            }
            else if (KeyCheck(Keys.Down) == true && offScreenCheck("DOWN") == true)
            {
                playerKeyPress = GameAction.PlayerDown;
            }
            else if (KeyCheck(Keys.Escape))
            {
                playerKeyPress = GameAction.Quit;
            }

            oldState = newState;

            return playerKeyPress;
        }

        private bool KeyCheck(Keys pressedKey)
        {
            //allows key to be held down
            return newState.IsKeyDown(pressedKey);

            //must continue to tap the key
            //return oldState.IsKeyDown(pressedKey) && newState.IsKeyUp(presssedKey);
        }

        //private void HandleKeyboardEvents()
        //{
        //    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        //    {
        //        // demonstrate the use of a Window's message box to display information
        //        MessageBox(new IntPtr(0), "Escape key pressed Click OK to exit.", "Debug Message", 0);
        //        Exit();
        //    }

        //    KeyboardState keyboardState = Keyboard.GetState();

        //    if ((keyboardState.IsKeyDown(Keys.Up)))
        //    {
        //        druid.Position = druid.Position + (new Vector2(0, -1));
        //    }
        //    else if ((keyboardState.IsKeyDown(Keys.Down)))
        //    {
        //        druid.Position = druid.Position + (new Vector2(0, +1));
        //    }

        //    if ((keyboardState.IsKeyDown(Keys.Left)))
        //    {
        //        druid.Position = druid.Position + (new Vector2(-1, 0));

        //    }
        //    else if ((keyboardState.IsKeyDown(Keys.Right)))
        //    {
        //        druid.Position = druid.Position + (new Vector2(+1, 0));
        //    }
        //}
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Crimson);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            druid.Draw(spriteBatch);
            point.Draw(spriteBatch);
            penalty.Draw(spriteBatch);

            DrawScoreTimer();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool offScreenCheck(string direction)
        {
            if (direction == "UP")
                if (druid.Center.Y - druid.Radius > 0) // If top of druid is below top of window, return true
                    return true;
            if (direction == "DOWN")
                if (druid.Center.Y + druid.Radius < WINDOW_HEIGHT) //If bottom of druid is above the bottom of window, return true
                    return true;
            if (direction == "LEFT")
                if (druid.Center.X - druid.Radius > 0) //If left of druid is to the right of the left of window, return true
                    return true;
            if (direction == "RIGHT")
                if (druid.Center.X + druid.Radius < WINDOW_WIDTH) //If right of druid is to the left of the right of window, return true
                    return true;

            return false;
        }

        private void DisplayWinScreen()
        {
            MessageBox(new IntPtr(0), "You have won the game! \n Press any button to exit.", "You Win.", 0);
            Exit();
        }

        private void DisplayTimeOutMessage()
        {
            MessageBox(new IntPtr(0), "Sorry, you ran out of time.\n Press any key to exit.", "Wah Wah", 0);
            Exit();
        }

        private void UpdateTimer(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void DrawScoreTimer()
        {
            spriteBatch.DrawString(scoreFont, "Score: " + score, new Vector2(SCORE_X_POSITION, SCORE_Y_POSITION), Color.Black);
            spriteBatch.DrawString(scoreFont, "Time: " + timer.ToString("000"), new Vector2(SCORE_X_POSITION, SCORE_Y_POSITION + 25), Color.Black);
        }
    }
}
