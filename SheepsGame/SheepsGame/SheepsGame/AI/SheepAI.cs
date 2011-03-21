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

namespace SheepsGame.AI
{
    class SheepAI
    {
        GameObjects.Sheep self;

        Vector2 target_position;
        Common.Timer idleTimer;
        enum State
        {
            Idle, Approach
        };
        State state;



        public SheepAI(GameObjects.Sheep sheep)
        {
            this.self = sheep;
            idleTimer = new Common.Timer();
            pre_idle_state();
        }

        public void Update(GameTime gameTime)
        {
            switch (state)
            {
                case State.Idle:
                    idle_state();
                    break;
                case State.Approach:
                    approach_position_state();
                    break;
            }
        }

        private void pre_idle_state()
        {
            idleTimer.SecondsUntilExpire = Game1.game.random.Next(2, 9);
            idleTimer.Start();

            state = State.Idle;

        }

        private void idle_state()
        {
            // Не делаем ничего
            

            // Проверяем, прошло ли время
            if (idleTimer.Expired)
            {
                pre_approach_state();
            }
        }

        private void pre_approach_state()
        {
            float new_X = Game1.game.random.Next(10, Game1.level1.levelLenght - 10);
            target_position = new Vector2(new_X, self.position.Y);
            state = State.Approach;
        }

        private void approach_position_state()
        {
            // Приближаемся к позиции
            if (target_position.X > self.position.X + 10)
                // Правее
                self.movement = +1f;
            else if (target_position.X < self.position.X - 10)
                // Левее
                self.movement = -1f;
            else
            {
                // Равно
                pre_idle_state();
            }
        }
    }
}
