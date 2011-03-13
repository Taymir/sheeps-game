using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SheepsGame.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace SheepsGame
{
    class MainMenu
    {
        List<MenuEntry> menuEntries = new List<MenuEntry>();

        public MainMenu()
        {
            string[] menus = { "Play", "Settings", "Credits", "Exit" };

            for (int i = 0; i < menus.Length; i++)
            {
                MenuEntry newMenuEntry = new MenuEntry(menus[i], Game1.game.spriteFont, new Vector2(60, 80 + 80*i));
                menuEntries.Add(newMenuEntry);
            }
        }
        bool press = false;
        public void Update(GameTime gameTime, TouchCollection touches)
        {
            if (touches.Count > 0)
            {
                foreach (MenuEntry menuEntry in menuEntries)
                {
                    if (menuEntry.rect.Contains((int)touches[0].Position.X, (int)touches[0].Position.Y))
                    {
                        if (menuEntry.text == "Play") ;
                        if (menuEntry.text == "Exit")
                            Game1.game.Exit();
                    }
                }
            }
            else
            {

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < menuEntries.Count; i++)
            {
                spriteBatch.DrawString(Game1.game.spriteFont, menuEntries[i].text, menuEntries[i].position, Color.White);
            }
        }
        
    }
}
