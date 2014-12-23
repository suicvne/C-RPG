using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ScreenSystemLibrary;
using SimpleGameXNA.Screens;

namespace SimpleGameXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont sf;
        ScreenSystem screenSystem;
        KeyboardState old_kbs;
        KeyboardState kbs = Keyboard.GetState();

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";

            EventInput.EventInput.Initialize(this.Window);

            screenSystem = new ScreenSystem(this);
            Components.Add(screenSystem);
        }

        protected override void Initialize()
        {
            screenSystem.AddScreen(new LoadSaveScreen());
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sf = Content.Load<SpriteFont>("MainFont");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            old_kbs = kbs;
            kbs = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (kbs.IsKeyDown(Keys.F11))
            {
                if (!graphics.IsFullScreen)
                {
                    graphics.IsFullScreen = true;
                    graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                    graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                }
                else
                {
                    graphics.IsFullScreen = false;
                    graphics.PreferredBackBufferWidth = 800;
                    graphics.PreferredBackBufferHeight = 600;
                }
                
                graphics.ApplyChanges();
            }
            else if (old_kbs.IsKeyDown(Keys.F11))
            { }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkCyan);

            spriteBatch.Begin();
            float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Math.Round(frameRate) <= 30)
                spriteBatch.DrawString(sf, Math.Round(frameRate, 2) + " FPS", new Vector2(0, 0), Color.Red);
            else
                spriteBatch.DrawString(sf, Math.Round(frameRate, 2) + " FPS", new Vector2(0, 0), Color.LightGreen);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
