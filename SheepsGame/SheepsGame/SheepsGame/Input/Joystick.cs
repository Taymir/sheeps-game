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

        Rectangle rightButton = new Rectangle(90, 0, 45, 135);
        Rectangle leftButton = new Rectangle(0, 0, 45, 135);
        Rectangle upButton = new Rectangle(0, 0, 135, 45);
        Rectangle downButton = new Rectangle(0, 90, 135, 45);
        Rectangle centerButton = new Rectangle(45, 45, 45, 45);

        bool centerButtonClicked = false;


        public Joystick(Texture2D texture, Vector2 position)
        {
            textureJoystick = texture;
            positionJoystick = position;
        }

        public void Update(TouchCollection touches, GameObjects.Ufo.Ufo ufo)
        {
            if (touches.Count > 0)
            {
                if (buttonClicked(touches[0], rightButton))
                    ufo.goRight();
                if (buttonClicked(touches[0], leftButton))
                    ufo.goLeft();
                if (buttonClicked(touches[0], upButton))
                    ufo.goUp();
                if (buttonClicked(touches[0], downButton))
                    ufo.goDown();

                if (buttonClicked(touches[0], centerButton))
                {
                    if (!centerButtonClicked)
                    {
                        centerButtonClicked = true;
                        ufo.fire();
                    }
                }
            } else {
                if (centerButtonClicked)
                {
                    centerButtonClicked = false;
                    ufo.stopFire();
                }
            }
        }

        private bool buttonClicked(TouchLocation touch, Rectangle buttonRect)
        {
            buttonRect.Offset((int)positionJoystick.X, (int)positionJoystick.Y);

            if(buttonRect.Contains((int)touch.Position.X, (int)touch.Position.Y))
                return true;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureJoystick, positionJoystick, Color.White);
        }
    }
}
