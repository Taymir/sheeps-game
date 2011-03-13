using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SheepsGame
{
    class Bullet
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;

        public bool alive = false;
    }
}
