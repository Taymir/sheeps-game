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
    class TestingGirl: GameObject
    {
        const string textureName = "girl";

        public TestingGirl(Vector2 position)
            : base(position, textureName, 140, 240, 7, 18)
        {
            this.frameRate = 16;
            this.flipped = true;
            this.scale = 0.5f;
            this.originRelative = Origin.BottomCenter;
        }

        public override void Update(GameTime gameTime)
        {
            const float walkingSpeed = 75f;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.position.X += elapsed * walkingSpeed;
            base.Update(gameTime);
        }
    }
}
