using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SheepsGame.GameObjects
{
    class Ray
    {
        private const string textureName = "ray";
        private Texture2D texture;
        public Vector2 position = new Vector2(100, 100);

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
            //@TODO
        }

        public void Draw(SpriteBatch sprite)
        {
            Vector2 screenPos = Game1.level1.GetScreenVector(position);
            sprite.Draw(texture, screenPos, null, Color.White);
        }
    }
}
