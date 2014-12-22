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
    public class GUIProgram : Microsoft.Xna.Framework.Game
    {
        static string dirSepChar = System.IO.Path.DirectorySeparatorChar.ToString();
        public static string GamesSaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            + dirSepChar + "My Games" + dirSepChar + "C#RPG";
        public static Player MAINPLAYER = new Player();
        public static InventoryHandler MAINPLAYERINVENTORY = new InventoryHandler(20);
        //
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont mainGameFont;
        public string USERINPUT = "";

        public GUIProgram()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            MAINPLAYER.ReadFromFile(GamesSaveDirectory + dirSepChar + "Mike" + dirSepChar + "player.sav");
            MAINPLAYERINVENTORY.ReadFromFile(GamesSaveDirectory + dirSepChar + "Mike" + dirSepChar + "player.inv");
        }

        protected override void Initialize()
        {
            EventInput.EventInput.CharEntered += new EventInput.CharEnteredHandler(EventInput_CharEntered);
            EventInput.EventInput.Initialize(this.Window);
            base.Initialize();
        }

        public void EventInput_CharEntered(object sender, EventInput.CharacterEventArgs e)
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
            GraphicsDevice.Clear(Color.Turquoise);

            GUIInputScreen guip = new GUIInputScreen();
            guip.USERINPUT = this.USERINPUT;
            string command = guip.DrawInputScreen(spriteBatch, GraphicsDevice, mainGameFont);

            if (command != null)
            {
                if (command.Contains('\r'))
                {
                    string upperCmd = command.ToUpper().Trim('\r');
                    switch (upperCmd)
                    {
                        case("STATUS"):
                            GUIStatusScreen guiss = new GUIStatusScreen();
                            USERINPUT = "";
                            string statusCommand = guiss.DrawStatusScreen(spriteBatch, GraphicsDevice, mainGameFont);
                            
                            break;
                        case("EXIT"):
                            MAINPLAYER.WriteToFile(GamesSaveDirectory + dirSepChar + MAINPLAYER.Name + dirSepChar + "player.sav");
                            MAINPLAYERINVENTORY.WriteToFile(GamesSaveDirectory + dirSepChar + MAINPLAYER.Name + dirSepChar + "player.inv");
                            Environment.Exit(0);
                            break;
                    }
                }
            }

            base.Draw(gameTime);
        }

        //
    }
}
