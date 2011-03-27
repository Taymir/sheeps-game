using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SheepsGame.GameObjects
{
    public class Platform : GameObject
    {
        const string textureName = "platform";

        public Platform(Vector2 position) : base(position, textureName)
        {
            this.originRelative = Origin.TopCenter;
        }

        public bool isOverMe(Vector2 objPosition)
        {
            if (objPosition.X > position.X - Width / 2 && objPosition.X < position.X + Width / 2)
                return true;
            return false;

        }
    }
}
