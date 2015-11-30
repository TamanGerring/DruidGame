using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    public class PointObject
    {
        /* Things we need
        --Position (derived)
        --Value
        --Sprite
        */
        private ContentManager _contentManager;
        private Texture2D _sprite;
        private int _radius;
        private string _spriteName;
        private Vector2 _position;
        private Vector2 _center;
        private bool _active;

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }


        public ContentManager ContentManager
        {
            get { return _contentManager; }
            set { _contentManager = value; }
        }

        public Texture2D Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        public string SpriteName
        {
            get { return _spriteName; }
            set { _spriteName = value; }
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

        public PointObject(
            ContentManager contentManager,
            string spriteName,
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

