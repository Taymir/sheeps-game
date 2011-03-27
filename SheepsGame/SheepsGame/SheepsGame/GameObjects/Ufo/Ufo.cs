using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SheepsGame.GameObjects.Ufo
{
    public class Ufo : GameObject
    {
        private const string textureName = "ufo";
        public Vector2 velocity;

        public float horizontalAcceleration = 0.0f;
        public float verticalAcceleration = 0.0f;

        private const float MoveAcceleration = 60f;// pixels per second
        private const float MoveFriction = 4.0f;   // percents per second
        private const float maxspeed = 14.0f;
        private const float rayWorkingSpeed = 0.1f; // Максимальная скорость при которой работает луч

        private GameObjects.Ufo.Ray ray;

        Sheep sheep_onboard;

        public bool hasSheep()
        {
            return sheep_onboard != null;
        }

        public Ufo(Vector2 position) : base(position, textureName)
        {
            this.velocity = Vector2.Zero;
            this.ray = new Ray();
            this.originRelative = Origin.MiddleCenter;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            ray.LoadContent();
        }

        public void fire()
        {
            if (isAtRayWorkingSpeed())
                this.ray.fire();
        }

        public void stopFire()
        {
            ray.stopFire();
        }

        public bool setSheep(Sheep sheep)
        {
            if (hasSheep())
                return false;

            this.sheep_onboard = sheep;
            return true;
        }

        public Sheep popSheep()
        {
            Sheep sheep = sheep_onboard;
            sheep_onboard = null;

            return sheep;
        }

        public override void Update(GameTime gameTime)
        {

            // acceleration
            acceleration(gameTime);

            // speed limit
            speedLimit();

            // correcting small velocity
            correctLowVelocity();

            // Отклонение из-за силы инерции
            inertiaDeflection();

            // Обновление координат
            UpdatePosition(velocity);

            // Проверка на нахождение в пределах игрового экрана
            handleLevelBorders();

            // Луч, следует за кораблем
            UpdateRow(gameTime);
        }

        #region Update SubMethods
        private void acceleration(GameTime gameTime)
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
        }

        private void speedLimit()
        {
            velocity.X = MathHelper.Clamp(velocity.X, -maxspeed, maxspeed);
            velocity.Y = MathHelper.Clamp(velocity.Y, -maxspeed, maxspeed);
        }

        private void correctLowVelocity()
        {
            if (Math.Abs(velocity.X) < .5f)
                velocity.X = 0;
            if (Math.Abs(velocity.Y) < .5f)
                velocity.Y = 0;
        }

        private void inertiaDeflection()
        {
            this.rotation = (float)MathHelper.ToRadians(velocity.X);
        }

        private void handleLevelBorders()
        {
            if (position.X + Width / 2 > Game1.level1.levelLenght)
            {
                position.X = Game1.level1.levelLenght - Width / 2;
                velocity.X *= -1;
            }
            else if (position.X - Width / 2 < 0)
            {
                position.X = Width / 2;
                velocity.X *= -1;
            }

            if (position.Y + Height / 2 > Game1.level1.groundY)
            {
                position.Y = Game1.level1.groundY - Height / 2;
                velocity.Y *= -1;
            }
            else if (position.Y - Height / 2 < 0)
            {
                position.Y = Height / 2;
                velocity.Y *= -1;
            }
        }

        private void UpdateRow(GameTime gameTime)
        {
            if (!isAtRayWorkingSpeed())
                ray.stopFire();
            ray.position = this.position;
            ray.Update(gameTime);
        }

        private bool isAtRayWorkingSpeed()
        {
            return (Math.Abs(velocity.X) < rayWorkingSpeed &&
                    Math.Abs(velocity.Y) < rayWorkingSpeed);
        }
        #endregion

        public override void Draw(SpriteBatch spriteBatch)
        {
            Game1.level1.TrackPosition(position);
            ray.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
