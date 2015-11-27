﻿using Microsoft.Xna.Framework;
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

        private const int WINNING_SCORE = 5;

        private const double TIME_LIMIT = 500;

        // create a random number set
        private Random randomNumbers = new Random();

        // declare instance variables for the background
        private Texture2D _background;
        private Rectangle _backgroundPosition;

        //declare a variable to store the players score
        private int score;

        private double timer = TIME_LIMIT;

        // declare a MouseState object to get mouse information
        private MouseState _mouseOldState;
        private MouseState _mouseNewState;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Druid druid;

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
            _backgroundPosition = new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

            druid = new Druid(Content, "Druid", 32, new Vector2(50, 400));
            druid.Active = true;

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if ((score != WINNING_SCORE) && (timer > 0))
            {
                HandleKeyboardEvents();
                //UpdateTimer();

                base.Update(gameTime);
            }

            
        }

        private void HandleKeyboardEvents()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                // demonstrate the use of a Window's message box to display information
                MessageBox(new IntPtr(0), "Escape key pressed Click OK to exit.", "Debug Message", 0);
                Exit();
            }

            KeyboardState keyboardState = Keyboard.GetState();

            if ((keyboardState.IsKeyDown(Keys.Up)))
            {
                druid.Position = druid.Position + (new Vector2(0, -1));
            }
            else if ((keyboardState.IsKeyDown(Keys.Down)))
            {
                druid.Position = druid.Position + (new Vector2(0, +1));
            }

            if ((keyboardState.IsKeyDown(Keys.Left)))
            {
                druid.Position = druid.Position + (new Vector2(-1, 0));
            }
            else if ((keyboardState.IsKeyDown(Keys.Right)))
            {
                druid.Position = druid.Position + (new Vector2(+1, 0));
            }
        }

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

            spriteBatch.End();

            base.Draw(gameTime);
        }





        //private bool offScreenCheck(string direction)
        //{
        //    if (direction == "UP")
        //        if (druid.Center.Y - druid.Radius > 0) // If top of druid is below top of window, return true
        //            return true;
        //    if (direction == "DOWN")
        //        if (druid.Center.Y + druid.Radius < WINDOW_HEIGHT) //If bottom of druid is above the bottom of window, return true
        //            return true;
        //    if (direction == "LEFT")
        //        if (druid.Center.X - druid.Radius > 0) //If left of druid is to the right of the left of window, return true
        //            return true;
        //    if (direction == "RIGHT")
        //        if (druid.Center.X + druid.Radius < WINDOW_WIDTH) //If right of druid is to the left of the right of window, return true
        //            return true;

        //    return false;
        //}
    }
}