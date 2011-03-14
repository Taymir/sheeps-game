using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SheepsGame
{
    class Guard
    {
        public Texture2D texture;

        public Vector2 position = new Vector2(100, 0);
        bool goRight = true;
        Bullet bullet = new Bullet();
        float angle = (float)(Math.PI / 4);

        public void FireBullet()
        {
            if (!bullet.alive)
            {
                bullet.position = this.position;
                bullet.velocity = new Vector2((float)Math.Cos(angle) * 5.0f, -(float)Math.Sin(angle) * 5.0f);
                bullet.alive = true;
            }
        }

        public void UpdateBullets()
        {
            if (bullet.alive)
            {
                bullet.position += bullet.velocity;
                if (!new Rectangle(0, 0, Game1.game.graphics.GraphicsDevice.Viewport.Width, Game1.game.graphics.GraphicsDevice.Viewport.Height).Contains
                    (new Rectangle((int)bullet.position.X, (int)bullet.position.Y, 10, 10)))
                {
                    bullet.alive = false;
                }
            }
        }

        public Guard(Texture2D texture, Texture2D bullet)
        {
            this.texture = texture;
            this.bullet.texture = bullet;
        }

        public void Update(GameTime gameTime)
        {
            if (position.X + texture.Width > Game1.game.graphics.PreferredBackBufferWidth)
                goRight = false;
            if (position.X < 0)
                goRight = true;

            if (goRight)
                position.X += 1 * gameTime.ElapsedGameTime.Milliseconds / 10;
            else
                position.X -= 1 * gameTime.ElapsedGameTime.Milliseconds / 10;
            UpdateBullets();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

          if(bullet.alive)
            spriteBatch.Draw(bullet.texture, new Rectangle((int)bullet.position.X, (int)bullet.position.Y, 30, 30), null, Color.White, (float)Math.PI*2 - angle, 
                new Vector2(bullet.texture.Width/2, bullet.texture.Height/2), SpriteEffects.None, 1.0f);
        }
    }
}
 