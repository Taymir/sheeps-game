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

        private const float MoveAcceleration = 100f;// pixels per second
        private const float MoveFriction = 2.1f;   // percents per second
        private const float maxspeed = 16.0f;
        private const float rayWorkingSpeed = 0.1f; // Максимальная скорость при которой работает луч

        private GameObjects.Ufo.Ray ray;
        private Sheep current_sheep;

        public Ufo(Vector2 position) : base(position, textureName)
        {
            this.velocity = Vector2.Zero;
            this.ray = new Ray();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            ray.LoadContent();
            this.SetOriginInCenter();
        }

        public void fire()
        {
            if (Math.Abs(velocity.X) < rayWorkingSpeed && Math.Abs(velocity.Y) < rayWorkingSpeed)
            {
                ray.visible = true;

                Sheep sheep = findSheepInRay();
                if (sheep != null)
                {
                    current_sheep = sheep;
                    sheep.startAbduction(this.position);
                }
            }
        }

        private Sheep findSheepInRay()
        {
            foreach (Sheep sheep in Game1.game.sheeps)
            {

                if (this.ray.Bounds.Intersects(sheep.Bounds))
                {
                    return sheep;
                }
            }

            return null;
        }

        /*private float getAngleBetweenObjs(GameObject obj1, GameObject obj2)
        {
            return (float)Math.Atan2(obj2.position.X - obj1.position.X, obj2.position.Y - obj1.position.Y);
        }

        private float getDistanceBetweenObjs(GameObject obj1, GameObject obj2)
        {
            return (float)Math.Sqrt(Math.Pow(obj2.position.X - obj1.position.X, 2) + Math.Pow(obj2.position.Y - obj1.position.Y, 2));
        }*/



        public void stopFire()
        {
            ray.visible = false;
            if (current_sheep != null)
            {
                current_sheep.abort_abduction();
                current_sheep = null;
            }
        }

        public override void Update(GameTime gameTime)
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
            UpdatePosition(velocity);

            // Проверка на нахождение в пределах игрового экрана
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

            // Луч, следует за кораблем
            if (Math.Abs(velocity.X) >= rayWorkingSpeed || Math.Abs(velocity.Y) >= rayWorkingSpeed)
                ray.visible = false;
            ray.position = this.position;
            ray.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Game1.level1.TrackPosition(position);
            ray.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
