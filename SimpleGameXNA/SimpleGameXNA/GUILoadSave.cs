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
using SimpleGameCliCore.Items;
using SimpleGameCliCore.Rooms;
using SimpleGameCliCore;
using System.Timers;

namespace SimpleGameXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GUILoadSave : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch drawInputCursor;
        SpriteFont mainGameFont;
        string USERINPUT = "";
        string SaveToLoad = "";
        int curYPos = 25;
        int doLoad = 0;

        public GUILoadSave()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            EventInput.EventInput.CharEntered += new EventInput.CharEnteredHandler(EventInput_CharEntered);
            EventInput.EventInput.Initialize(this.Window);
            base.Initialize();
        }

        void EventInput_CharEntered(object sender, EventInput.CharacterEventArgs e)
        {
            /*char inf = e.Character;
            System.Windows.Forms.MessageBox.Show("You pressed " + inf);*/
            if (e.Character == '\b')
            {
                int count = USERINPUT.Length - 1;
                if (count < USERINPUT.Length)
                    if(count != -1)
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
            drawInputCursor = new SpriteBatch(GraphicsDevice);
            mainGameFont = this.Content.Load<SpriteFont>("MainGame");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);

            if(doLoad == 0)
                SaveToLoad = DrawLoadSaveScreen();
            else if (doLoad == 1)
            {
                this.Exit();
            }

            base.Draw(gameTime);
        }

        public string DrawLoadSaveScreen()
        {
            if (GraphicsDevice == null)
                this.Run();

            VERYBEGINNING:
            List<string> saves = new List<string>();

            int width = GraphicsDevice.DisplayMode.Width;
            int height = GraphicsDevice.DisplayMode.Height;
            //
            spriteBatch.Begin();
            spriteBatch.DrawString(mainGameFont, "LOADSAVE", new Vector2((width / 2 / 2), 0), Color.White);
            //
            if (!LoadSave.SAVESEXIST())
            {
                spriteBatch.DrawString(mainGameFont, "No saves could be found!", new Vector2(3, 20), Color.White);
            }
            else
            {
                int saveCount = 0;
                curYPos = 25;
                saves.Clear();
                foreach (var i in System.IO.Directory.GetDirectories(Program.GamesSaveDirectory))
                {
                    if (System.IO.File.Exists(i + System.IO.Path.DirectorySeparatorChar + "player.sav"))
                    {
                        spriteBatch.DrawString(mainGameFont, 
                            string.Format("{0}: {1}", saveCount, System.IO.Path.GetFileNameWithoutExtension(i)), 
                            new Vector2(3, curYPos), 
                            Color.White);

                        saves.Add(i);
                        saveCount++;
                        curYPos += 20;
                    }
                }
                ///TODO: figure out how 2 user input
                spriteBatch.DrawString(mainGameFont, USERINPUT + "|", new Vector2(3, curYPos += 20), Color.White);
                if (USERINPUT.Contains('\r'))
                {
                    ///TODO: Loading
                    spriteBatch.End();
                    doLoad = 1;
                    try
                    {
                        int sel = int.Parse(USERINPUT);
                        return saves[sel];
                    }
                    catch
                    { goto VERYBEGINNING; }
                }
            }
            spriteBatch.End();
            return null;
        }

        //
        void blink_Elapsed(object sender, ElapsedEventArgs e)
        {
            int test = 0;
            drawInputCursor.Begin();
            if (test == 0)
            {
                drawInputCursor.DrawString(mainGameFont, USERINPUT + "|", new Vector2(3, curYPos += 20), Color.White);
                test++;
            }
            else if (test == 1)
            {
                drawInputCursor.DrawString(mainGameFont, USERINPUT, new Vector2(3, curYPos += 20), Color.White);
                test--;
            }
            drawInputCursor.End();
        }
        //
    }
}
