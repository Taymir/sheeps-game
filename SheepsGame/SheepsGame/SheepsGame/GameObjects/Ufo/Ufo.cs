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
        private Texture2D texture;
        public Vector2 position = new Vector2(100, 100);
        public Vector2 velocity;

        private float speed = 3.5f;
        private float friction = 0.93f;
        private float maxspeed = 16.0f;
        private float rotation = 0.0f;

        public float x
        {
            get
            {
                return position.X;
            }
        }

        public float y
        {
            get
            {
                return position.Y;
            }
        }

        public Ufo(Texture2D texture)
        {
            this.texture = texture;
        }

        public void goLeft()
        {
            velocity.X -= speed;
        }

        public void goRight()
        {
            velocity.X += speed;
        }

        public void goUp()
        {
            velocity.Y -= speed;
        }

        public void goDown()
        {
            velocity.Y += speed;
        }

        public void slowDown()
        {
            velocity *= friction;
        }

        private void correctLowVelocity()
        {
            if (velocity.X < .5f && velocity.X > -.5f)
                velocity.X = 0;
            if (velocity.Y < .5f && velocity.Y > -.5f)
                velocity.Y = 0;
        }

        private void UpdatePosition()
        {
            if (!Game1.level1.Scroll(velocity.X))
                position.X += velocity.X;
            position.Y += velocity.Y;
        }

        private void limitSpeed()
        {
            if (velocity.X > maxspeed)
                velocity.X = maxspeed;
            else if (velocity.X < -maxspeed)
                velocity.X = -maxspeed;

            if (velocity.Y > maxspeed)
                velocity.Y = maxspeed;
            else if (velocity.Y < -maxspeed)
                velocity.Y = -maxspeed;
        }

        // Отклонение из-за силы инерции
        private void inertiaDeviation()
        {
            this.rotation = (float)deg2rad(velocity.X);
        }

        private double deg2rad(double deg)
        {
            return deg * Math.PI / 180;
        }

        private double rad2deg(double rad)
        {
            return rad * 180 / Math.PI;
        }

        public void Update(GameTime time)
        {
            slowDown();
            correctLowVelocity();
            UpdatePosition();
            limitSpeed();
            inertiaDeviation();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
        }
    }
}
