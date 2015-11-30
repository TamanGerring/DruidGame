using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    public class PenaltyObject : ScoringObjects
    {
        /* Things we need
        --Position (derived)
        --Value
        --Sprite
        */

        private ContentManager _contentManager;
        private Texture2D _sprite;
        private int _radius;
        private Vector2 _position;
        private Vector2 _center;

        public PenaltyObject(
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
    }
}

