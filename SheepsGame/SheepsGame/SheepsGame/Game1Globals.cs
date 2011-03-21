using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace SheepsGame
{
    /*
     * Часть класса Game1:
     * Хранилище для объектов, доступ к которым будет осуществляться из других классов
     * Напр., объект текущего уровня, списки вражеских объектов и т.п.
     */
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {
        public static Game1 game;
        public static Level level1;

        public GraphicsDeviceManager graphics;
        public SpriteFont spriteFont;
    }
}