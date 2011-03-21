using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SheepsGame.GameObjects.Guard
{
    class Bullet : GameObject
    {
        const string textureName = "bullet";
        const float maxspeed = 400f;   // pixels (hor,ver) per second
        public Vector2 velocity;


        public Bullet() : base(Vector2.Zero, textureName)
        {
            this.visible = false;
            this.scale = 0.1f;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            this.SetOriginInCenter();
        }

        public override void Update(GameTime gameTime)
        {
            if (visible)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

                position.X += velocity.X * elapsed;
                position.Y += velocity.Y *elapsed;
                if (!
                    new Rectangle(0, 0, Game1.game.graphics.GraphicsDevice.Viewport.Width, Game1.game.graphics.GraphicsDevice.Viewport.Height).Contains
                    (new Rectangle((int)position.X, (int)position.Y, 10, 10)))//@REFACTOR
                {
                    visible = false;
                }
            }
        }

        public void Fire(Vector2 position, float shootingAngle)
        {
            if (!visible)
            {
                this.position = position;
                this.velocity = new Vector2((float)Math.Cos(shootingAngle) * maxspeed, -(float)Math.Sin(shootingAngle) * maxspeed);
                this.rotation = -shootingAngle;
                visible = true;
            }
        }
    }
}
