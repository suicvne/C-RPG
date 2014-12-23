using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenSystemLibrary;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using EventInput;

namespace SimpleGameXNA.Screens
{
    public class <CLASSNAMEHERE> : GameScreen
    {
		string USERINPUT = "";
		SpriteBatch sb;
		SpriteFont sf;
	
		public override bool AcceptsInput
        {
            get { return true; }
        }

        public InventoryScreen()
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
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            //base.Update(gameTime);
        }

        protected override void DrawScreen(GameTime gameTime)
        {
            sb = new SpriteBatch(ScreenSystem.GraphicsDevice);
            sb.Begin();
            sb.End();
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenSystem.Content;
            sf = content.Load<SpriteFont>("MainFont");
        }

        private void TestScreen_Removing(object sender, EventArgs e)
        {
			///TODO: whether or not to load another screen
            //ScreenSystem.AddScreen(new InputScreen());
        }
    }
}
