using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ClickFruit
{
    public class Fruit
    {

        #region FIELDS

        private ContentManager _contentManager;
        private string _spriteName;
        private Texture2D _sprite;
        private int _radius;
        private Vector2 _position;
        private Vector2 _velocity;
        private Vector2 _center;
        private bool _active;

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

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        #endregion

        #region CONSTRUCTORS
        
        public Fruit(
            ContentManager contentManager,
            string spriteName,
            int radius,
            Vector2 positiion,
            Vector2 velocity
            )
        {
            _contentManager = contentManager;
            _spriteName = spriteName;
            _radius = radius;
            _position = positiion;
            _velocity = velocity;

            // load the fruit image into the Texture2D for the fruit sprite
            _sprite = _contentManager.Load<Texture2D>(_spriteName);
        }

        #endregion

        #region METHODS

        public void Draw(SpriteBatch spriteBatch)
        {
            // only draw the fruit if it is active
            if (_active)
            {
                spriteBatch.Draw(_sprite, _position, Color.White);
            }
        }


        #endregion

    }
}
