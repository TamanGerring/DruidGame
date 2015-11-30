using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class ScoringObjects
    {
        /* Things we need
        --Position
        --Placement Method
        --Removal Method
        */

        private ContentManager _contentManager;
        private Texture2D _sprite;
        private int _radius;
        private Vector2 _position;
        private Vector2 _center;

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
    }
}
