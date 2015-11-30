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

        //This is a test of the NMC Alert system. Please be advised NMC will explode momentarily... 
        //This is test number 2...

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

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
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

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            playerKeyPress = GetKeyboardEvents();

            switch (playerKeyPress)
            {
                case GameAction.None:
                    break;
                case GameAction.Quit:
                    Exit();
                    break;
                case GameAction.PlayerRight:
                    druid.DruidDirection = Druid.Direction.Right;
                    druid.Position = new Vector2(druid.Position.X + 1, druid.Position.Y);
                    break;
                case GameAction.PlayerLeft:
                    druid.DruidDirection = Druid.Direction.Left;
                    druid.Position = new Vector2(druid.Position.X - 1, druid.Position.Y);
                    break;
                case GameAction.PlayerUp:
                    druid.DruidDirection = Druid.Direction.Up;
                    druid.Position = new Vector2(druid.Position.X, druid.Position.Y - 1);
                    break;
                case GameAction.PlayerDown:
                    druid.DruidDirection = Druid.Direction.Down;
                    druid.Position = new Vector2(druid.Position.X, druid.Position.Y + 1);
                    break;
                default:
                    break;
            }


            base.Update(gameTime);
            
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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Crimson);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            druid.Draw(spriteBatch);
            point.Draw(spriteBatch);
            penalty.Draw(spriteBatch);

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
    }
}
