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
using SheepsGame.GameObjects;

namespace SheepsGame
{
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {
        SpriteBatch spriteBatch;
        MainMenu mainMenu;

        Joystick joystick;

        public Game1()
        {
            game = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            level1 = new Level(3000);

            sheeps = new GameObjectList();
            sheeps.Add(new Sheep(new Vector2(100, Sheep.getStandartSheepY())));
            sheeps.Add(new Sheep(new Vector2(250, Sheep.getStandartSheepY())));
            sheeps.Add(new Sheep(new Vector2(330, Sheep.getStandartSheepY())));
            sheeps.Add(new Sheep(new Vector2(430, Sheep.getStandartSheepY())));
            sheeps.Add(new Sheep(new Vector2(500, Sheep.getStandartSheepY())));

            hostiles = new GameObjectList();
            //hostiles.Add( new GameObjects.Guard.Guard(new Vector2(100, 320)) );

            player = new GameObjects.Ufo.Ufo(new Vector2(GraphicsDevice.Viewport.Width / 2, 100));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteFont = Content.Load<SpriteFont>("default");

            mainMenu = new MainMenu();
            joystick = new Joystick(Content.Load<Texture2D>("Joystick"), new Vector2(GraphicsDevice.Viewport.Width - 175, GraphicsDevice.Viewport.Height / 2 - 60));
            
            
            level1.backgroundTexture = Content.Load<Texture2D>("fon_2");

            sheeps.LoadContent();
            hostiles.LoadContent();
            player.LoadContent();
        }

 
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            Common.TimerManager.Instance.Update(gameTime);
            TouchCollection touches = TouchPanel.GetState();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            mainMenu.Update(gameTime, touches);
            joystick.Update(touches, player);

            sheeps.Update(gameTime);
            hostiles.Update(gameTime);
            player.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            level1.Draw(spriteBatch);

            sheeps.Draw(spriteBatch);
            hostiles.Draw(spriteBatch);
            player.Draw(spriteBatch);

            joystick.Draw(spriteBatch);
            //mainMenu.Draw(spriteBatch);

            // FPS COUNTER.
            //float fps = (1000.0f / gameTime.ElapsedGameTime.Milliseconds);
            //spriteBatch.DrawString(spriteFont, fps.ToString(), new Vector2(GraphicsDevice.Viewport.Width - 70, 0), Color.Green);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
