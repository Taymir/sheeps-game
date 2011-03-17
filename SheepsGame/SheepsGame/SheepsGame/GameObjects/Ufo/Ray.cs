using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SheepsGame.GameObjects.Ufo
{
    class Ray
    {
        public Vector2 position = new Vector2(0, 0);
        public bool visible = false;

        private const string textureName = "ray";
        private Texture2D texture;
        private byte alpha = 100;
        private bool alphaGrow = false;


        public Ray()
        {

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

        public void Draw(SpriteBatch sprite)
        {
            if(visible)
            {
                sprite.Draw(texture, position, null, new Color(255, 255, 255, alpha), 0f, new Vector2(texture.Width / 2, 0), 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
