using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace SheepsGame.GameObjects
{
    class Sheep : GameObject
    {
        const string textureName = "sheep";

        public float movement = 0f;
        const float maxspeed = 75f;


        public Sheep(Vector2 position) : base(position, textureName) { }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // updating position
            position.X += movement * maxspeed * elapsed;

            // nulling movement for next frame
            movement = 0f;
        }
    }
}
