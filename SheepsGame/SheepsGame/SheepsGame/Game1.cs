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
using SheepsGame.Input;

namespace SheepsGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MainMenu mainMenu;
        Guard guard;

        GameObjects.Ufo.Ufo ufo;
        Joystick joystick;

        public static Game1 game;
        private GameObjects.Sheep sheep;
        public SpriteFont spriteFont;

        public static Level level1;

        public Game1()
        {
            game = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            sheep = new GameObjects.Sheep();
            ufo = new GameObjects.Ufo.Ufo();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            guard = new Guard(Content.Load<Texture2D>("guard"), Content.Load<Texture2D>("bullet"));
            guard.position.Y = graphics.PreferredBackBufferHeight - guard.texture.Height - 5;
            spriteFont = Content.Load<SpriteFont>("default");

            mainMenu = new MainMenu();
            ufo.LoadContent();
            joystick = new Joystick(Content.Load<Texture2D>("Joystick"), new Vector2(GraphicsDevice.Viewport.Width - 175, GraphicsDevice.Viewport.Height / 2 - 60));

            
            ufo.position.X = GraphicsDevice.Viewport.Width / 2;

            sheep.LoadContent();

            level1 = new Level(3000);
            level1.backgroundTexture = Content.Load<Texture2D>("fon_2");

            
        }

 
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            sheep.UnloadContent();
            ufo.UnloadContent();
        }


        protected override void Update(GameTime gameTime)
        {
            Common.TimerManager.Instance.Update(gameTime);
            TouchCollection touches = TouchPanel.GetState();

            //guard.FireBullet();
            joystick.Update(touches, ufo);

            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
                Exit();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //guard.Update(gameTime);
            mainMenu.Update(gameTime, touches);

            //(“»Ã)
            ufo.Update(gameTime);
            sheep.Update(gameTime);

            

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            level1.Draw(spriteBatch);
            //guard.Draw(spriteBatch);
            //mainMenu.Draw(spriteBatch);

            sheep.Draw(spriteBatch);
            ufo.Draw(spriteBatch);
            joystick.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
