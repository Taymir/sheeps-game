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

        public Platform(Vector2 position) : base(position, textureName) { }

    }
}
