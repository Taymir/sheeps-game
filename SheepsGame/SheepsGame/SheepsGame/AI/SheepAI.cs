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
        Random random;
        enum State
        {
            Idle, Approach
        };
        State state;



        public SheepAI(GameObjects.Sheep sheep)
        {
            this.random = new Random();
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
            idleTimer.SecondsUntilExpire = random.Next(3, 7);
            idleTimer.Start();

            state = State.Idle;

        }

        private void idle_state()
        {
            // Не делаем ничего
            

            // Проверяем, прошло ли 3-7 сек
            if (idleTimer.Expired)
            {
                pre_approach_state();
            }
        }

        private void pre_approach_state()
        {
            float new_X = random.Next(50, 600);
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
