using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace SheepsGame.Input
{
    class Joystick
    {
        Texture2D textureJoystick;
        Vector2 positionJoystick;

        public Joystick(Texture2D texture, Vector2 position)
        {
            textureJoystick = texture;
            positionJoystick = position;
        }

        public void Update(TouchCollection touches, GameObjects.Ufo.Ufo ufo)
        {
            // Движение вправо
            if (touches[0].Position.X <= Game1.game.graphics.GraphicsDevice.Viewport.Width - 40 &&
                touches[0].Position.X >= Game1.game.graphics.GraphicsDevice.Viewport.Width - 90 &&
                touches[0].Position.Y >= Game1.game.graphics.GraphicsDevice.Viewport.Height / 2 - 30 &&
                touches[0].Position.Y <= Game1.game.graphics.GraphicsDevice.Viewport.Height / 2 + 30)
            {
                ufo.goRight();
            }
            // Движение влево
            else if (touches[0].Position.X <= Game1.game.graphics.GraphicsDevice.Viewport.Width - 120 &&
              touches[0].Position.X >= Game1.game.graphics.GraphicsDevice.Viewport.Width - 170 &&
              touches[0].Position.Y >= Game1.game.graphics.GraphicsDevice.Viewport.Height / 2 - 30 &&
              touches[0].Position.Y <= Game1.game.graphics.GraphicsDevice.Viewport.Height / 2 + 30) // Движение влево
            {
                ufo.goLeft();
            }
            // Движение вверх
            else if (touches[0].Position.X <= Game1.game.graphics.GraphicsDevice.Viewport.Width - 90 &&
              touches[0].Position.X >= Game1.game.graphics.GraphicsDevice.Viewport.Width - 140 &&
              touches[0].Position.Y >= Game1.game.graphics.GraphicsDevice.Viewport.Height / 2 - 60 &&
              touches[0].Position.Y <= Game1.game.graphics.GraphicsDevice.Viewport.Height / 2 + 30) //Движение вверх
            {
                ufo.goUp();
            }
            // Движение вниз
            else if (touches[0].Position.X <= Game1.game.graphics.GraphicsDevice.Viewport.Width - 90 &&
              touches[0].Position.X >= Game1.game.graphics.GraphicsDevice.Viewport.Width - 140 &&
              touches[0].Position.Y >= Game1.game.graphics.GraphicsDevice.Viewport.Height / 2 - 90 &&
              touches[0].Position.Y <= Game1.game.graphics.GraphicsDevice.Viewport.Height / 2 + 60) // Движение вниз
            {
                ufo.goDown();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureJoystick, positionJoystick, Color.White);
        }
    }
}
