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
        private int _radius;
        private Vector2 _position;
        private Vector2 _center;
        private bool _active;
        private Direction _DirectionOfTravel;

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

        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _center.X = _position.X + _radius;
                _center.Y = _position.Y + _radius;
            }
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
        public Druid(ContentManager contentManager, string spriteName,
            int radius,
            Vector2 position
            )
        {
            _contentManager = contentManager;
            _spriteName = spriteName;
            _radius = radius;
            _position = position;
            _center = position + new Vector2(radius, radius);

            // load the ball image into the Texture2D for the ball sprite
            _sprite = _contentManager.Load<Texture2D>(_spriteName);
            _spriteLeft = _contentManager.Load<Texture2D>("druid_left");
            _spriteRight = _contentManager.Load<Texture2D>("druid_right");
            
        }

        #endregion

        #region METHODS
        /// <summary>
        /// add ball sprite to the SpriteBatch object
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // only draw the  if it is active
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

            }
        }


        #endregion

    }
}
