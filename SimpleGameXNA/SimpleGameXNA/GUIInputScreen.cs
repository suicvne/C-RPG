using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SimpleGameCliCore.Items;
using SimpleGameCliCore.Rooms;
using SimpleGameCliCore;

namespace SimpleGameXNA
{
    public class GUIInputScreen : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont mainGameFont;
        public string USERINPUT;

        public GUIInputScreen()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            EventInput.EventInput.CharEntered += new EventInput.CharEnteredHandler(EventInput_CharEntered);
            try
            {
                //EventInput.EventInput.Initialize(this.Window);
            }
            catch
            {
                //no big deal, it's just already been initialized that's all!
            }
            base.Initialize();
        }

        private void EventInput_CharEntered(object sender, EventInput.CharacterEventArgs e)
        {
            if (e.Character == '\b')
            {
                int count = USERINPUT.Length - 1;
                if (count < USERINPUT.Length)
                    if (count != -1)
                        USERINPUT = USERINPUT.Remove(count);
            }
            else if (e.Character == '\t')
            { }
            else if (e.Character == 0x002)
            { }
            else if (e.Character == 0x015)
            { }
            else if (e.Character == 0x00f)
            { }
            else if (e.Character == '\r')
            {
                USERINPUT += '\r';
            }
            else
            {
                USERINPUT += e.Character;
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mainGameFont = this.Content.Load<SpriteFont>("MainGame");
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);

            //handle commands here

            base.Draw(gameTime);
        }

        public string DrawInputScreen(SpriteBatch spriteBatch, GraphicsDevice GraphicsDevice, SpriteFont mainGameFont)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(mainGameFont, "INPUT", new Vector2(GraphicsDevice.DisplayMode.Width / 2 / 2, 0), Color.White);

            string enterCommandText = "Please enter the command you'd like to execute: ";
            spriteBatch.DrawString(mainGameFont, enterCommandText, new Vector2(0, 45), Color.White);
            spriteBatch.DrawString(mainGameFont, USERINPUT + "|", new Vector2(0, 65), Color.White);

            if (USERINPUT.Contains('\r'))
            {
                spriteBatch.End();
                return USERINPUT;
            }

            spriteBatch.End();
            return null;
        }
        //
    }
}
