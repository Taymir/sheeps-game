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
    public class Sheep : GameObject//@REFACTOR
    {
        const string textureName = "sheep";

        public float movement = 0f;
        
        const float maxspeed = 75f;
        AI.SheepAI ai;

        public bool freezed
        {
            get
            {
                return !ai.enabled;
            }

            set
            {
                ai.enabled = !value;
            }
        }

        public Sheep(Vector2 position) : base(position, textureName) 
        {
            ai = new AI.SheepAI(this);
            originRelative = Origin.BottomCenter;
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            ai.Update(gameTime);

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

        public static int getStandartSheepY() //@TMP
        {
            return Game1.level1.groundY;
        }
    }
}
