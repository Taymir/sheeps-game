using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SheepsGame.GameObjects.Ufo
{
    class Ray : GameObject
    {
        private const string textureName = "ray";

        private byte alpha = 100;
        private bool alphaGrow = false;

        public Ray() : base(Vector2.Zero, textureName) 
        {
            visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (visible)
            {
                // Анимция луча
                if (alphaGrow)
                    alpha += 5;
                else
                    alpha -= 5;

                if (alpha <= 50 || alpha >= 130)
                    alphaGrow = !alphaGrow;

                
            }
                
        }

        public override void Draw(SpriteBatch sprite)
        {
            if(visible)
            {
                Vector2 screenPosition = Game1.level1.GetScreenPosition(position);
                sprite.Draw(texture, screenPosition, null, new Color(255, 255, 255, alpha), 0f, new Vector2(texture.Width / 2, 0), 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
