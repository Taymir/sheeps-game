using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SheepsGame.Menus
{
    class MenuEntry
    {
        public Rectangle rect;
        public string text;
        public Texture2D texture;
        public Vector2 position;


        public MenuEntry(string text, SpriteFont sf, Vector2 position)
        {
            this.position = position;
            this.text = text;

            Vector2 textSize = sf.MeasureString(text);
            rect = new Rectangle((int)position.X, (int)position.Y, (int)textSize.X, (int)textSize.Y);
        }
    }
}
