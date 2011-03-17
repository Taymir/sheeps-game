using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SheepsGame.GameObjects.Ufo
{
    class Ufo
    {
        private const string textureName = "ufo";
        private Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;

        public float rotation = 0.0f;
        public float horizontalAcceleration = 0.0f;
        public float verticalAcceleration = 0.0f;

        private const float MoveAcceleration = 100f;// pixels per second
        private const float MoveFriction = 2.1f;   // percents per second
        private const float maxspeed = 16.0f;

        private GameObjects.Ufo.Ray ray;

        public Ufo(Vector2 position)
        {
            this.position = position;
            this.velocity = Vector2.Zero;
            this.ray = new Ray();
        }

        public void LoadContent()
        {
            texture = Game1.game.Content.Load<Texture2D>(textureName);
            ray.LoadContent();
        }

        public void UnloadContent()
        {
            //TODO
            ray.UnloadContent();
        }

        public void fire()
        {
            if(Math.Abs(velocity.X) < 1f && Math.Abs(velocity.Y) < 1f)
                ray.visible = true;
        }

        public void stopFire()
        {
            ray.visible = false;
        }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // accelerating velocity
            velocity.X += horizontalAcceleration * MoveAcceleration * elapsed;
            velocity.Y += verticalAcceleration * MoveAcceleration * elapsed;

            // deaccelerating
            if (Math.Abs(horizontalAcceleration) < .1f)
                velocity.X *= (1 - MoveFriction * elapsed);
            if (Math.Abs(verticalAcceleration) < .1f)
                velocity.Y *= (1 - MoveFriction * elapsed);

            // nulling acceleration for next frame
            horizontalAcceleration = 0.0f;
            verticalAcceleration = 0.0f;

            // speed limit
            velocity.X = MathHelper.Clamp(velocity.X, -maxspeed, maxspeed);
            velocity.Y = MathHelper.Clamp(velocity.Y, -maxspeed, maxspeed);

            // correcting small velocity
            if (Math.Abs(velocity.X) < .5f)
                velocity.X = 0;
            if (Math.Abs(velocity.Y) < .5f)
                velocity.Y = 0;

            // Отклонение из-за силы инерции
            this.rotation = (float)MathHelper.ToRadians(velocity.X);

            // Обновление координат
            if (!Game1.level1.Scroll(velocity.X)) //@TMP
                position.X += velocity.X;
            position.Y += velocity.Y;

            // Луч, следует за кораблем
            if (Math.Abs(velocity.X) >= 1f || Math.Abs(velocity.Y) >= 1f)
                ray.visible = false;
            ray.position = this.position;
            ray.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ray.Draw(spriteBatch);
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
        }
    }
}
