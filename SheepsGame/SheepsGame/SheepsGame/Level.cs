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
        public Texture2D backgroundTexture;

        public Level(int levelLength)
        {
            this.levelLenght = levelLength;
        }

        /*public Boolean Scroll(float dx)
        {
            if (ScrollX + dx >= 0 && ScrollX + dx <= levelLenght - Game1.game.GraphicsDevice.Viewport.Width)
            {
                ScrollX += dx;
                return true;
            }
            return false;
        }*/

        public Rectangle GetScreenRect(Rectangle rect)
        {
            Rectangle screenRect = rect;
            screenRect.Offset((int)-ScrollX, 0);

            return screenRect;
        }

        /*public Vector2 GetScreenVector(Vector2 vec)
        {
            Vector2 screenVec = vec;
            screenVec.X -= ScrollX;

            return screenVec;
        }*/

        public Vector2 GetScreenPosition(Vector2 LevelPosition)
        {
            LevelPosition.X = MathHelper.Clamp(LevelPosition.X, 0, levelLenght);
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
