using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    public class PenaltyObject
    {
        /* Things we need
        --Position (derived)
        --Value
        --Sprite
        */
        private ContentManager _contentManager;
        private Texture2D _sprite;
        private string _spriteName;
        private Vector2 _position;
        private Vector2 _center;
        private bool _active;
        private Rectangle _boundingRectangle;
        private int _spriteHeight;
        private int _spriteWidth;
        private object startingPosition;
        private int _speedVertical;
        private int _speedHorizontal;

        public ContentManager ContentManager
        {
            get { return _contentManager; }
            set { _contentManager = value; }
        }

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

        public Vector2 Center
        {
            get { return _center; }
            set { _center = value; }
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

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public PenaltyObject(
            ContentManager contentManager,
            string spriteName,
            Vector2 position
    )
        {
            _contentManager = contentManager;
            _spriteName = spriteName;
            _position = position;
            // _center = position + new Vector2(radius, radius);

            // load the ball image into the Texture2D for the ball sprite
            _sprite = _contentManager.Load<Texture2D>(_spriteName);

            _spriteWidth = _sprite.Width;
            _spriteHeight = _sprite.Height;

            // set the initial center and bounding rectangle for the player
            _center = new Vector2(position.X + (_spriteWidth / 2), position.Y + (_spriteHeight / 2));
            _boundingRectangle = new Rectangle((int)position.X, (int)position.Y, _spriteWidth, _spriteHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // only draw the  if it is active
            if (_active)
            {
                spriteBatch.Draw(_sprite, _position, Color.White);
            }
        }
    }
}

