using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Druid
    {

        #region ENUMS

        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        #endregion

        #region FIELDS

        private ContentManager _contentManager;
        private string _spriteName;
        private Texture2D _sprite;
        private Texture2D _spriteLeft;
        private Texture2D _spriteRight;
        //private int _radius;
        private Vector2 _position;
        private Vector2 _center;
        private bool _active;
        private Direction _DirectionOfTravel;
        private int _spriteWidth;
        private int _spriteHeight;
        private Rectangle _boundingRectangle;
        private int _speedHorizontal;
        private int _speedVertical;

        public Direction DruidDirection
        {
            get { return _DirectionOfTravel; }
            set { _DirectionOfTravel = value; }
        }


        #endregion

        #region PROPERTIES

        public ContentManager ContentManager
        {
            get { return _contentManager; }
            set { _contentManager = value; }
        }

        public string SpriteName
        {
            get { return _spriteName; }
            set { _spriteName = value; }
        }

        public Texture2D Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        //public int Radius
        //{
        //    get { return _radius; }
        //    set { _radius = value; }
        //}

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _center = new Vector2(_position.X + (_spriteWidth / 2), _position.Y + (_spriteHeight / 2));
                _boundingRectangle = new Rectangle((int)_position.X, (int)_position.Y, _spriteWidth, _spriteHeight);
            }
        }
        
        public int SpeedHorizontal
        {
            get { return _speedHorizontal; }
            set { _speedHorizontal = value; }
        }

        public int SpeedVertical
        {
            get { return _speedVertical; }
            set { _speedVertical = value; }
        }

        public Rectangle BoundingRectangle
        {
            get { return _boundingRectangle; }
            set { _boundingRectangle = value; }
        }

        public Vector2 Center
        {
            get { return _center; }
            set { _center = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// instantiate a new ball
        /// </summary>
        /// <param name="contentManager">game content manager object</param>
        /// <param name="spriteName">file name of sprite</param>
        /// <param name="position">vector position of druid</param>
        public Druid(ContentManager contentManager, string spriteName, Vector2 position)
        {
            _contentManager = contentManager;
            _spriteName = spriteName;
            //_radius = radius;
            _position = position;
            //_center = position + new Vector2(radius, radius);

            // load the ball image into the Texture2D for the ball sprite
            _sprite = _contentManager.Load<Texture2D>(_spriteName);
            _spriteLeft = _contentManager.Load<Texture2D>("druid_left");
            _spriteRight = _contentManager.Load<Texture2D>("druid_right");

            _spriteWidth = _spriteLeft.Width;
            _spriteHeight = _spriteLeft.Height;

            // set the initial center and bounding rectangle for the player
            _center = new Vector2(position.X + (_spriteWidth / 2), position.Y + (_spriteHeight / 2));
            _boundingRectangle = new Rectangle((int)position.X, (int)position.Y, _spriteWidth, _spriteHeight);
            
        }

        #endregion

        #region METHODS
        /// <summary>
        /// add sprite to the SpriteBatch object
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // only draw the if sprite is active
            if (_active)
            {
                if (_DirectionOfTravel == Direction.Right)
                {
                    spriteBatch.Draw(_spriteRight, _position, Color.White);
                }
                else if (_DirectionOfTravel == Direction.Left)
                {
                    spriteBatch.Draw(_spriteLeft, _position, Color.White);
                }
                else if (_DirectionOfTravel == Direction.Up)
                {
                    spriteBatch.Draw(_spriteRight, _position, Color.White);
                }
                else if (_DirectionOfTravel == Direction.Down)
                {
                    spriteBatch.Draw(_spriteRight, _position, Color.White);
                }

            }
        }


        #endregion

    }
}
