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
        public Vector2 position;
        public Vector2 velocity;

        public float movement = 0f;

        private const float maxspeed = 75f;

        SpriteEffects flip = SpriteEffects.None;


        public Sheep(Vector2 position)
        {
            this.position = position;
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

        public void Update(GameTime gameTime)
        {
            /*// Отражение от стенок
            if (this.position.X + this.texture.Width > Game1.game.GraphicsDevice.Viewport.Width
                ||
                this.position.X < 0)
            {
                this.velocity *= -1;
            }*/
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // setting velocity
            velocity.X = movement * maxspeed * elapsed;
            velocity.Y = 0;

            // nulling movement for next frame
            movement = 0f;

            // updating position
            position += velocity;
        }

 

        public void Draw(SpriteBatch sprite)
        {
            if (velocity.X > 0)
                flip = SpriteEffects.None;
            else if (velocity.X < 0)
                flip = SpriteEffects.FlipHorizontally;
            
            Vector2 screenPos = Game1.level1.GetScreenVector(position);
            sprite.Draw(texture, screenPos, null, Color.White, 0f, Vector2.Zero, 1f, flip, 0f);
        }


    }
}
