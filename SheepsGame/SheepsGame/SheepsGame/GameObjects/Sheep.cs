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
    class Sheep
    {
        const string textureName = "sheep";
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;
        float speed = 2.5f;

        Common.Timer idleTimer;
        Random random;

        private Vector2 ai_position;
        enum AI_state {
            Idle, Approach
        };

        AI_state ai_state;

        public Sheep()
        {
            random = new Random();
            idleTimer = new Common.Timer();
            this.ai_pre_idle_state();
            

            position = new Vector2(0, 320);
            velocity = Vector2.Zero;
        }

        public void goLeft()
        {
            velocity.X = -speed;
        }

        public void goRight()
        {
            velocity.X = +speed;
        }

        public void goUp()
        {
            velocity.Y = -speed;
        }

        public void goDown()
        {
            velocity.Y = +speed;
        }

        public void slowDown()
        {
            velocity = Vector2.Zero;
        }

        public void LoadContent()
        {
            texture = Game1.game.Content.Load<Texture2D>(textureName);
        }

        public void UnloadContent()
        {
            //TODO
        }

        private void UpdatePosition()
        {
            position += velocity;
        }

        public void Update(GameTime gameTime)
        {
            /*// Отражение от стенок
            if (this.position.X + this.texture.Width > Game1.game.GraphicsDevice.Viewport.Width
                ||
                this.position.X < 0)
            {
                this.velocity *= -1;
            }*/

            switch (this.ai_state)
            {
                case AI_state.Idle:
                    ai_idle_state();
                    break;
                case AI_state.Approach:
                    ai_approach_position_state();
                    break;
            }

            UpdatePosition();
        }

        private void ai_pre_idle_state()
        {
            idleTimer.SecondsUntilExpire = random.Next(3, 7);
            idleTimer.Start();

            ai_state = AI_state.Idle;

        }

        private void ai_idle_state()
        {
            // Не делаем ничего
            slowDown();

            // Проверяем, прошло ли 3-7 сек
            if (idleTimer.Expired)
            {
                ai_pre_approach_state();
            }
        }

        private void ai_pre_approach_state()
        {
            float new_X = random.Next(50, 600);
            ai_position = new Vector2(new_X, position.Y);
            ai_state = AI_state.Approach;
        }

        private void ai_approach_position_state()
        {
            // Приближаемся к позиции
            if (ai_position.X > position.X + 10)
                // Правее
                goRight();
            else if (ai_position.X < position.X - 10)
                // Левее
                goLeft();
            else
            {
                // Равно
                ai_pre_idle_state();
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            SpriteEffects effects = (velocity.X > 0? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            Vector2 screenPos = Game1.level1.GetScreenVector(position);
            sprite.Draw(texture, screenPos, null, Color.White, 0f, Vector2.Zero, 1f, effects, 0f);
        }


    }
}
