using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenSystemLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SimpleGameXNA.Screens
{
    public class InputScreen : GameScreen
    {
        //
        SpriteFont sf;
        SpriteBatch sb;
        string USERINPUT = "";

        public override bool AcceptsInput
        {
            get { return true; }
        }

        public InputScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(1);
            EventInput.EventInput.CharEntered += new EventInput.CharEnteredHandler(EventInput_CharEntered);
        }

        void EventInput_CharEntered(object sender, EventInput.CharacterEventArgs e)
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
            
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            //base.Update(gameTime);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenSystem.Content;
            sf = content.Load<SpriteFont>("MainFont");
        }

        public override void UnloadContent()
        {
            sf = null;
        }

        protected override void DrawScreen(GameTime gameTime)
        {
            ScreenSystem.GraphicsDevice.Clear(Color.DarkRed);
            sb = new SpriteBatch(ScreenSystem.GraphicsDevice);
            sb.Begin();
            int mid = ((int)ScreenSystem.GraphicsDevice.Viewport.Width - (int)sf.MeasureString("INVENTORY").Length()) / 2;
            sb.DrawString(sf, "INVENTORY", new Vector2(mid, 0), Color.White);
            sb.DrawString(sf, USERINPUT + "|", new Vector2(0, 45), Color.White);
            if (USERINPUT.Contains('\r'))
            {
                string cmd = USERINPUT.Trim('\r').ToUpper();
                if (cmd == "Q")
                { 
                    this.ExitScreen();
                    ScreenSystem.AddScreen(new InventoryScreen());
                }
            }
            sb.End();
        }
        //
    }
}
