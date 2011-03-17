using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SheepsGame.GameObjects.Guard
{
    class Guard : GameObject
    {
        private const string textureName = "guard";

        public float movement = 0f;
        const float maxspeed = 90f;
        const float shootingAngle = (float)(Math.PI / 4);

        Bullet bullet;

        public Guard(Vector2 position) : base(position, textureName)
        {
            bullet = new Bullet();
            movement = +1f;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            bullet.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (position.X + texture.Width > Game1.game.graphics.PreferredBackBufferWidth)
                movement = -1f;
            else if (position.X < 0)
                movement = +1f;

            // updating position
            position.X += movement * maxspeed * elapsed;

            // update bullet
            Fire();//@TMP
            bullet.Update(gameTime);
        }

        public void Fire()
        {
            bullet.Fire(position, shootingAngle);
        }

        public override void Draw(SpriteBatch sprite)
        {
            base.Draw(sprite);
            bullet.Draw(sprite);
        }
    }
}
 