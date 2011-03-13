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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        MainMenu mainMenu;
        Guard guard;

        SpriteBatch joystickSpriteBatch;
        Texture2D textureJoystick;
        Vector2 positionJoystick;
        Ufo ufo;

        public static Game1 game;
        private GameObjects.Sheep sheep;
        public SpriteFont spriteFont;

        Texture2D background;

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
            texture = Content.Load<Texture2D>("logo");
            rect = new Rectangle(0, 0, texture.Width, texture.Height);
            guard = new Guard(Content.Load<Texture2D>("oxotnik"), Content.Load<Texture2D>("bullet"));
            guard.position.Y = graphics.PreferredBackBufferHeight - guard.texture.Height - 5;
            spriteFont = Content.Load<SpriteFont>("default");

            mainMenu = new MainMenu();
            background = Content.Load<Texture2D>("fon_2");

            joystickSpriteBatch = new SpriteBatch(GraphicsDevice);
            textureJoystick = this.Content.Load<Texture2D>("Joystick");
            positionJoystick = new Vector2(GraphicsDevice.Viewport.Width - 175, GraphicsDevice.Viewport.Height / 2 - 60);

            ufo = new Ufo(this.Content.Load<Texture2D>("Ufo"), new Vector2(0, 0));

            sheep.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            sheep.UnloadContent();
        }
        Rectangle rect;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            TouchCollection touches = TouchPanel.GetState();
            if (touches.Count > 0)
            {
                guard.FireBullet();
                //guard.position.X = touches[0].Position.X;
                //guard.position.Y = touches[0].Position.Y;

            }

            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
                Exit();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            guard.Update(gameTime);
            mainMenu.Update(gameTime, touches);

            // Работа с джойстиком (стас)
            TouchCollection touchCollection = TouchPanel.GetState();

            if (touchCollection.Count > 0)//@REFACTOR
            {
                // Движение вправо
                if (touchCollection[0].Position.X <= this.GraphicsDevice.Viewport.Width - 40 &&
                    touchCollection[0].Position.X >= this.GraphicsDevice.Viewport.Width - 90 &&
                    touchCollection[0].Position.Y >= this.GraphicsDevice.Viewport.Height / 2 - 30 &&
                    touchCollection[0].Position.Y <= this.GraphicsDevice.Viewport.Height / 2 + 30)
                {
                    ufo.goRight();
                }
                // Движение влево
                else if (touchCollection[0].Position.X <= this.GraphicsDevice.Viewport.Width - 120 &&
                  touchCollection[0].Position.X >= this.GraphicsDevice.Viewport.Width - 170 &&
                  touchCollection[0].Position.Y >= this.GraphicsDevice.Viewport.Height / 2 - 30 &&
                  touchCollection[0].Position.Y <= this.GraphicsDevice.Viewport.Height / 2 + 30) // Движение влево
                {
                    ufo.goLeft();
                }
                // Движение вверх
                else if (touchCollection[0].Position.X <= this.GraphicsDevice.Viewport.Width - 90 &&
                  touchCollection[0].Position.X >= this.GraphicsDevice.Viewport.Width - 140 &&
                  touchCollection[0].Position.Y >= this.GraphicsDevice.Viewport.Height / 2 - 60 &&
                  touchCollection[0].Position.Y <= this.GraphicsDevice.Viewport.Height / 2 + 30) //Движение вверх
                {
                    ufo.goUp();
                }
                // Движение вниз
                else if (touchCollection[0].Position.X <= this.GraphicsDevice.Viewport.Width - 90 &&
                  touchCollection[0].Position.X >= this.GraphicsDevice.Viewport.Width - 140 &&
                  touchCollection[0].Position.Y >= this.GraphicsDevice.Viewport.Height / 2 - 90 &&
                  touchCollection[0].Position.Y <= this.GraphicsDevice.Viewport.Height / 2 + 60) // Движение вниз
                {
                    ufo.goDown();
                }
            }
            //ufo.UpdatePosition(new Vector2(touchCollection[0].Position.X, touchCollection[0].Position.Y));


            //(ТИМ)
            ufo.Update(gameTime);
            sheep.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int i = 0; i < 30; i++)
            {
                spriteBatch.Draw(background, new Rectangle(i * background.Width, 0, background.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
            }
            //guard.Draw(spriteBatch);
            //mainMenu.Draw(spriteBatch);

            //(ТИМ)
            sheep.Draw(spriteBatch);
            ufo.Draw(spriteBatch);

            spriteBatch.End();

            //(СТАС)
            joystickSpriteBatch.Begin();
            joystickSpriteBatch.Draw(textureJoystick, positionJoystick, Color.White);
            joystickSpriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
