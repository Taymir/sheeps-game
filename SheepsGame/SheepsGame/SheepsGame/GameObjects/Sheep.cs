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
    class Sheep : GameObject
    {
        const string textureName = "sheep";

        public float movement = 0f;
        
        const float maxspeed = 75f;
        AI.SheepAI ai;

        enum State
        {
            General, Abducting, Falling
        }
        State state = State.General;

        float falling_velocity;
        Vector2 abduction_target;

        public Sheep(Vector2 position) : base(position, textureName) 
        {
            ai = new AI.SheepAI(this);
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch(state)
            {
                case State.General:
                    ai.Update(gameTime);
                    break;

                case State.Abducting://@TODO: связать со временем
                    if (position.X > abduction_target.X + 5)
                        position.X--;
                    else if (position.X < abduction_target.X - 5)
                        position.X++;

                    position.Y--;

                    this.scale -= .01f;

                    if (position.Y < abduction_target.Y || scale < .01f)
                        this.dead = true;
                    break;

                case State.Falling://@TODO: связать со временем
                    if (position.Y < getStandartSheepY())
                    {
                        falling_velocity += 1f;
                        position.Y += falling_velocity;
                        scale += .01f * falling_velocity;
                    }
                    else
                    {
                        if (falling_velocity > 10f)
                        {
                            //Анимация смерти
                            this.dead = true;
                        }
                        else
                        {
                            state = State.General;
                            scale = 1f;
                        }
                    }
                    break;
            }

            // updating position
            position.X += movement * maxspeed * elapsed;

            // updating orientation
            if (movement > 0)
                flipped = false;
            else if (movement < 0)
                flipped = true;

            // nulling movement for next frame
            movement = 0f;
        }

        public void startAbduction(Vector2 abduction_target)
        {
            if (state != State.Abducting)
            {
                this.abduction_target = abduction_target;

                state = State.Abducting;
            }
        }

        public void abort_abduction()
        {
            if (state == State.Abducting)
            {
                state = State.Falling;
                falling_velocity = 0f;
            }
        }

        public static int getStandartSheepY() //@TMP
        {
            return Game1.level1.groundY - 55;
        }
    }
}
