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
    public class InputScreen : GameScreen
    {
        SpriteFont sf;
        SpriteBatch sb;
        string USERINPUT = "";
        string FINALCOMMAND = "";

        public override bool AcceptsInput
        {
            get { return true; }
        }

        public InputScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            EventInput.EventInput.CharEntered += new CharEnteredHandler(EventInput_CharEntered);
            this.Removing += new EventHandler(TestScreen_Removing);
            try
            { ScreenSystem.GraphicsDevice.Reset(); }
            catch { }
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
            try
            {
                ScreenSystem.RemoveScreen(new InputScreen());
                ScreenSystem.RemoveScreen(new LoadSaveScreen());
            }
            catch
            { }
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            //base.Update(gameTime);
        }

        protected override void DrawScreen(GameTime gameTime)
        {
            sb = new SpriteBatch(ScreenSystem.GraphicsDevice);
            
            sb.Begin();
            int mid = ((int)ScreenSystem.GraphicsDevice.Viewport.Width - (int)sf.MeasureString("Testing").Length()) / 2;
            sb.DrawString(sf, "INPUT", new Microsoft.Xna.Framework.Vector2(mid, 0), Color.White);
            sb.DrawString(sf, USERINPUT + "|", new Microsoft.Xna.Framework.Vector2(0, 45), Color.White);
            if(USERINPUT.Contains('\r')) //this means the user hit enter
            {
                string cmd = USERINPUT.Trim('\r').ToUpper();
                if (cmd == "EXIT")
                {
                    sb.DrawString(sf, "Saving character..", new Vector2(0, 60), Color.Red);
                    Program.MAINPLAYER.WriteToFile(Program.GamesSaveDirectory
                        + Path.DirectorySeparatorChar
                        + Program.MAINPLAYER.Name
                        + Path.DirectorySeparatorChar
                        + "player.sav");
                    Program.MAINPLAYERINVENTORY.WriteToFile(Program.GamesSaveDirectory
                        + Path.DirectorySeparatorChar
                        + Program.MAINPLAYER.Name
                        + Path.DirectorySeparatorChar
                        + "player.inv");
                    Environment.Exit(0);
                }
                else if (cmd == "INVENTORY")
                {
                    FINALCOMMAND = cmd;
                    ExitScreen();
                }
            }
            sb.End();
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenSystem.Content;
            sf = content.Load<SpriteFont>("MainFont");
        }

        private void TestScreen_Removing(object sender, EventArgs e)
        {
            if (FINALCOMMAND == "INVENTORY")
            {
                ScreenSystem.AddScreen(new InventoryScreen());
                this.UnloadContent();
                this.State = ScreenState.Inactive;
            }
        }

    }
}
