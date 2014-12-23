using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenSystemLibrary;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using EventInput;
using System.IO;

namespace SimpleGameXNA.Screens
{
    public class LoadSaveScreen : GameScreen
    {
        SpriteBatch sb;
        SpriteFont sf;
        string USERINPUT = "";
        List<string> saves = new List<string>();
        int saveCount = 0;
        int exitCode = 0;

		public override bool AcceptsInput
        {
            get { return true; }
        }

        public LoadSaveScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            EventInput.EventInput.CharEntered += new CharEnteredHandler(EventInput_CharEntered);
            this.Removing += new EventHandler(TestScreen_Removing);
        }

        private void EventInput_CharEntered(object sender, CharacterEventArgs e)
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

        public override void Initialize()
        {
            //for load save, we'll be loading the list of saves from here so we only have to iterate through a list and not through directories
            saves.Clear();

            foreach (var i in System.IO.Directory.GetDirectories(Program.GamesSaveDirectory))
            {
                if (System.IO.File.Exists(i + System.IO.Path.DirectorySeparatorChar + "player.sav"))
                {
                    //Console.WriteLine("{0}: {1}\n", saveCount, System.IO.Path.GetFileNameWithoutExtension(i));
                    saves.Add(i);
                    saveCount++;
                }
            }
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            //base.Update(gameTime);
        }

        protected override void DrawScreen(GameTime gameTime)
        {
            ScreenSystem.GraphicsDevice.Clear(Color.White);
            sb = new SpriteBatch(ScreenSystem.GraphicsDevice);
            sb.Begin();
            int mid = ((int)ScreenSystem.GraphicsDevice.Viewport.Width - (int)sf.MeasureString("LOADSAVE").Length()) / 2;
            sb.DrawString(sf, "LOADSAVE", new Vector2(mid, 0), Color.Black);

            int curYPos = 45;
            int count = 0;

            foreach (var i in saves)
            {
                sb.DrawString(sf, string.Format("{0}. {1}", count, System.IO.Path.GetFileNameWithoutExtension(i)),
                    new Vector2(0, curYPos), Color.Black);
                count++;
                curYPos += 20;
            }
            curYPos += 20;
            sb.DrawString(sf, "Enter the corresponding number or type q to quit", new Vector2(0, curYPos), Color.Black);
            ///TODO: Accept the user's input properly, for now I'll just do a temp one
            sb.DrawString(sf, USERINPUT + "|", new Vector2(0, curYPos + 20), Color.Black);

            if (USERINPUT.Contains('\r'))
            {
                string cmd = USERINPUT.Trim('\r').ToUpper();
                if (cmd == "Q")
                {
                    this.ExitScreen();
                    Environment.Exit(0);
                }
                else
                {
                    int res = TryLoadSave(cmd);
                    if (res == 0)
                    {
                        exitCode = res;
                        this.ExitScreen();
                    }
                    else
                    {
                        exitCode = res;
                        this.ExitScreen();
                    }
                }
            }

            sb.End();
        }

        private int TryLoadSave(string indexAsString)
        {
            try
            {
                int index = int.Parse(indexAsString);

                string directory = saves[index]; //This should return a full path, something like C:\Users\Mike\Documents\My Games\C#RPG\Mike

                Program.MAINPLAYER.ReadFromFile(directory + Path.DirectorySeparatorChar + "player.sav");
                Program.MAINPLAYERINVENTORY.ReadFromFile(directory + Path.DirectorySeparatorChar + "player.inv");

                System.Windows.Forms.MessageBox.Show("Success");
                return 0;
            }
            catch
            {
                this.ExitScreen();
                ScreenSystem.AddScreen(new LoadSaveScreen());
                return 1;
            }
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenSystem.Content;
            sf = content.Load<SpriteFont>("MainFont");
        }

        private void TestScreen_Removing(object sender, EventArgs e)
        {
            if (exitCode == 1)
            {
                ScreenSystem.AddScreen(new LoadSaveScreen());
            }
            else if (exitCode == 0)
            {
                ///TODO: whether or not to load another screen
                ScreenSystem.AddScreen(new InputScreen());
            }
        }

        //
    }
}
