using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SheepsGame
{
    public class Level
    {
        static float ScrollX = 0;

        public int levelLenght;
        public int groundY;

        public Texture2D backgroundTexture;

        public Level(int levelLength)
        {
            this.levelLenght = levelLength;
            this.groundY = 375;
        }

        public Vector2 GetScreenPosition(Vector2 LevelPosition)
        {
            // Ограничения координат
            LevelPosition.X = MathHelper.Clamp(LevelPosition.X, 0, levelLenght);
            LevelPosition.Y = MathHelper.Clamp(LevelPosition.Y, 0, backgroundTexture.Height);

            Vector2 ScreenPosition = LevelPosition;
            ScreenPosition.X -= ScrollX;

            return ScreenPosition;
        }

        public void TrackPosition(Vector2 TrackPosition)
        {
            float newScrollX = TrackPosition.X - Game1.game.graphics.GraphicsDevice.Viewport.Width / 2;//@TODO: Нло должно уходить в сторону когда достигается предел сцены

            if (newScrollX >= 0 && newScrollX <= levelLenght - Game1.game.graphics.GraphicsDevice.Viewport.Width)
                ScrollX = newScrollX;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < levelLenght / backgroundTexture.Width; i++)
            {
                spriteBatch.Draw(backgroundTexture, new Rectangle(i * backgroundTexture.Width, 0, backgroundTexture.Width, Game1.game.graphics.GraphicsDevice.Viewport.Height), Color.White);
            }
        }
    }
}
