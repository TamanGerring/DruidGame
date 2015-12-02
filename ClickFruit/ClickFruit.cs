using System; // add to allow Windows message box
using System.Runtime.InteropServices; // add to allow Windows message box
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
//test
namespace ClickFruit
{
    public class ClickFruit : Game
    {
        // add code to allow Windows message boxes when running in a Windows environment
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

        // set the cell size in pixels
        private const int CELL_WIDTH = 64;
        private const int CELL_HEIGHT = 64;

        // set the map size in cells
        private const int MAP_CELL_ROW_COUNT = 8;
        private const int MAP_CELL_COLUMN_COUNT = 10;

        // set the window size
        private const int WINDOW_WIDTH = MAP_CELL_COLUMN_COUNT * CELL_WIDTH;
        private const int WINDOW_HEIGHT = MAP_CELL_ROW_COUNT * CELL_HEIGHT;

        // set the location for the score
        private const int SCORE_X_POSTION = 500;
        private const int SCORE_Y_POSTION = 20;

        // set the winning score
        private const int WINNING_SCORE = 10;

        // set the time limit seconds
        private const double TIME_LIMIT = 50;

        // create a random number set
        private Random _randomNumbers = new Random();

        // declare instance variables for the sprites
        private List<Fruit> _fruits;

        // declare instance variables for the background
        private Texture2D _background;
        private Rectangle _backgroundPosition;

        // declare instance variables for the sprites
        private Fruit _fruit;

        // declare a spriteBatch object
        private SpriteBatch _spriteBatch;

        // declare a MouseState object to get mouse information
        private MouseState _mouseOldState;
        private MouseState _mouseNewState;

        // declare a SpriteFont for the on-screen score
        private SpriteFont _scoreFont;

        // declare a variable to store the score
        private int _score;

        // declare a variable for the timer
        private double _timer = TIME_LIMIT;

        // declare a SoundEffect for the explosion
        private SoundEffect _explosion;

        private GraphicsDeviceManager _graphics;

        
        public ClickFruit()
        {
            _graphics = new GraphicsDeviceManager(this);

            // set the window size 
            _graphics.PreferredBackBufferWidth = MAP_CELL_COLUMN_COUNT * CELL_WIDTH;
            _graphics.PreferredBackBufferHeight = MAP_CELL_ROW_COUNT * CELL_HEIGHT;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // set the background's initial position
            _backgroundPosition = new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

            // create a fruit object
            _fruits = new List<Fruit>();

            // add fruit to the list
            Fruit fruit01 = new Fruit(Content, "Fruit01", 32, new Vector2(300, 200), new Vector2(3, 4));
            _fruits.Add(fruit01);
            Fruit fruit02 = new Fruit(Content, "Fruit02", 32, new Vector2(100, 50), new Vector2(-1, -1));
            _fruits.Add(fruit02);
            Fruit fruit03 = new Fruit(Content, "Fruit03", 32, new Vector2(400, 150), new Vector2(2, 2));
            _fruits.Add(fruit03);

            // make the fruit active
            fruit01.Active = true;
            fruit02.Active = true;
            fruit03.Active = true;

            // make mouse visible on game
            this.IsMouseVisible = true;

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // load the background sprite
            // fruit sprite loaded when instantiated
            _background = Content.Load<Texture2D>("BackgroundSandyStained");

            // load the font for the score
            _scoreFont = Content.Load<SpriteFont>("ScoreFont");

            // load the explosion
            _explosion = Content.Load<SoundEffect>("Explosion01");
        }

        
        protected override void UnloadContent()
        {
            // Unload any non ContentManager content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            // player still playing
            if ((_score != WINNING_SCORE) && (_timer > 0))
            {
                HandleKeyboardEvents();
                HandleMouseEvents();
                UpdateFruitMovement(_fruits);
                UpdateTimer(gameTime);

                base.Update(gameTime);
            }
            // player wins
            else if (_score == WINNING_SCORE)
            {
                DisplayWinScreen();
            }
            // player out of time
            else if (_timer <= 0)
            {
                DisplayOutOfTimeMessage();
            }

        }

        /// <summary>
        /// method to add all of the current sprites to the next game screen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // draw the background and the fruit
            _spriteBatch.Draw(_background, _backgroundPosition, Color.White);

            foreach (Fruit fruit in _fruits)
            {
                fruit.Draw(_spriteBatch);
            }

            DrawScoreTimer();

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        #region HELPER METHODS

        /// <summary>
        /// method to bounce fruit of walls
        /// </summary>
        public void BounceOffWalls(Fruit fruit)
        {
            // fruit is at the top or bottom of the window, change the Y direction
            if ((fruit.Position.Y > WINDOW_HEIGHT - CELL_HEIGHT) || (fruit.Position.Y < 0))
            {
                fruit.Velocity = new Vector2(fruit.Velocity.X, -fruit.Velocity.Y);
            }
            // fruit is at the left or right of the window, change the X direction
            else if ((fruit.Position.X > WINDOW_WIDTH - CELL_WIDTH) || (fruit.Position.X < 0))
            {
                fruit.Velocity = new Vector2(-fruit.Velocity.X, fruit.Velocity.Y);
            }
        }

        /// <summary>
        /// method to determine if the mouse is on the fruit
        /// </summary>
        /// <returns></returns>
        private bool MouseClickOnFruit(Fruit fruit)
        {
            bool mouseClickedOnFruit = false;

            // get the current state of the mouse
            _mouseNewState = Mouse.GetState();

            // left mouse button was a click
            if (_mouseNewState.LeftButton == ButtonState.Pressed && _mouseOldState.LeftButton == ButtonState.Released)
            {
                // mouse over fruit
                if ((_mouseNewState.X > fruit.Position.X) &&
                    (_mouseNewState.X < (fruit.Position.X + 64)) &&
                    (_mouseNewState.Y > fruit.Position.Y) &&
                    (_mouseNewState.Y < (fruit.Position.Y + 64)))
                {
                    mouseClickedOnFruit = true;

                    _score++;
                }
            }

            // store the current state of the mouse as the old state
            _mouseOldState = _mouseNewState;

            return mouseClickedOnFruit;
        }
        
        private void Spawn(Fruit fruit)
        {
            // find a valid location to spawn the fruit
            int fruitXPosition = _randomNumbers.Next(WINDOW_WIDTH - CELL_WIDTH);
            int fruitYPosition = _randomNumbers.Next(WINDOW_HEIGHT - CELL_HEIGHT);

            // set fruit's new position and reverse direction
            fruit.Position = new Vector2(fruitXPosition, fruitYPosition);
            fruit.Velocity = -fruit.Velocity;
        }
        
        private void HandleKeyboardEvents()
        {
            // detect an Escape key press to end the game
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                // demonstrate the use of a Window's message box to display information
                MessageBox(new IntPtr(0), "Escape key pressed Click OK to exit.", "Debug Message", 0);
                Exit();
            }
        }
        
        private void HandleMouseEvents()
        {

            // get the current state of the mouse
            _mouseNewState = Mouse.GetState();

            // left mouse button was a click
            if (_mouseNewState.LeftButton == ButtonState.Pressed && _mouseOldState.LeftButton == ButtonState.Released)
            {
                // check all fruits
                foreach (var fruit in _fruits)
                {
                    // if the mouse is over the fruit and left button is clicked, destroy and spawn new fruit
                    if (MouseOnFruit(fruit))
                    {
                        _explosion.CreateInstance().Play();
                        Spawn(fruit);
                        _score++;
                    }
                }
            }

            // store the current state of the mouse as the old state
            _mouseOldState = _mouseNewState;
        }

        /// <summary>
        /// method to determine if the mouse is on the fruit
        /// </summary>
        /// <returns></returns>
        private bool MouseOnFruit(Fruit fruit)
        {
            bool mouseClickedOnFruit = false;

            // get the current state of the mouse
            MouseState mouseState = Mouse.GetState();

            // mouse over fruit
            if ((_mouseNewState.X > fruit.Position.X) &&
                (_mouseNewState.X < (fruit.Position.X + 64)) &&
                (_mouseNewState.Y > fruit.Position.Y) &&
                (_mouseNewState.Y < (fruit.Position.Y + 64)))
            {
                mouseClickedOnFruit = true;
            }

            return mouseClickedOnFruit;
        }
        
        private void UpdateFruitMovement(List<Fruit> _fruits)
        {
            foreach (Fruit fruit in this._fruits)
            {
                if (fruit.Active)
                {
                    BounceOffWalls(fruit);
                    fruit.Position += fruit.Velocity;
                }
            }
        }
        
        private void DrawScoreTimer()
        {
            _spriteBatch.DrawString(_scoreFont, "Score: " + _score, new Vector2(SCORE_X_POSTION, SCORE_Y_POSTION), Color.Black);
            _spriteBatch.DrawString(_scoreFont, "Time: " + _timer.ToString("000"), new Vector2(SCORE_X_POSTION, SCORE_Y_POSTION + 25), Color.Black);
        }
        
        private void DisplayWinScreen()
        {
            MessageBox(new IntPtr(0), "You have won the game!\n Press any key to exit.", "Debug Message", 0);
            Exit();
        }
        
        private void UpdateTimer(GameTime gameTime)
        {
            _timer -= gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        private void DisplayOutOfTimeMessage()
        {
            MessageBox(new IntPtr(0), "Sorry, you ran out of time.\n Press any key to exit.", "Debug Message", 0);
            Exit();
        }

        #endregion


    }
}
