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
using SimpleGameCliCore.Items;
using SimpleGameCliCore.Items.BaseClasses;

namespace SimpleGameXNA.Screens
{
    public class InventoryScreen : GameScreen
    {
        //
        SpriteFont sf;
        SpriteBatch sb;
        string USERINPUT = "";
        string USERINPUT2 = ""; //this is for entering the index
        bool input2InUse = false;

        public override bool AcceptsInput
        {
            get { return true; }
        }

        public InventoryScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(1);
            EventInput.EventInput.CharEntered += new EventInput.CharEnteredHandler(EventInput_CharEntered);
            this.Exiting += new TransitionEventHandler(InventoryScreen_Exiting);
        }

        void InventoryScreen_Exiting(object sender, TransitionEventArgs tea)
        {
            if (input2InUse) //this means we were getting the information for an item
                ScreenSystem.AddScreen(new InventoryScreen());
            else
                ScreenSystem.AddScreen(new InputScreen());

            
        }

        void EventInput_CharEntered(object sender, EventInput.CharacterEventArgs e)
        {
            if (!input2InUse)
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
            else
            {
                if (e.Character == '\b')
                {
                    int count = USERINPUT2.Length - 1;
                    if (count < USERINPUT2.Length)
                        if (count != -1)
                            USERINPUT = USERINPUT2.Remove(count);
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
                    USERINPUT2 += '\r';
                }
                else
                {
                    USERINPUT2 += e.Character;
                }
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
            //Drawing of the inventory items
            int curYPos = 45;

            if (Program.MAINPLAYERINVENTORY.GetCount() != 0)
            {
                bool drawRightSide = false;

                for (int i = 0; i < Program.MAINPLAYERINVENTORY.GetCount(); i++)
                {
                    string outt = string.Format("{0}. {1}", i, Program.MAINPLAYERINVENTORY.RetrieveItem(i).Name);
                    if (curYPos + 300 > ScreenSystem.GraphicsDevice.Viewport.Height)
                    {
                        drawRightSide = true;
                        curYPos = 45;
                    }
                    if (drawRightSide)
                    {
                        sb.DrawString(sf, outt, new Vector2(ScreenSystem.GraphicsDevice.Viewport.Width / 2, curYPos), Color.White);
                    }
                    else
                    {
                        sb.DrawString(sf, outt, new Vector2(0, curYPos), Color.White);
                    }
                    curYPos += 20;
                }
            }
            else
                sb.DrawString(sf, "Your inventory is empty!", new Vector2(0, curYPos), Color.White);
            //

            //
            sb.DrawString(sf, "Type i for information on an item or q to exit", new Vector2(0, curYPos + 200), Color.White);
            sb.DrawString(sf, USERINPUT + "|", new Vector2(0, curYPos + 220), Color.White);
            if (USERINPUT.Contains('\r'))
            {
                string cmd = USERINPUT.Trim('\r').ToUpper();
                if (cmd == "Q")
                { 
                    this.ExitScreen();
                }
                if (cmd == "I")
                {
                    input2InUse = true;
                    sb.DrawString(sf, "Enter the inventory slot number of the item", new Vector2(0, curYPos + 240), Color.White);
                    sb.DrawString(sf, USERINPUT2 + "|", new Vector2(0, curYPos + 260), Color.White);
                    ///TODO INFORMATION SCREEN
#if DEBUG
                    if(USERINPUT2.Contains('\r'))
                    {
                        string indxString = USERINPUT2.Trim('\r');
                        int indx = int.Parse(indxString);
                        GetDebugItem(indx);
                    }
                    this.ExitScreen();
#endif
                }
            }
            sb.End();
        }
        //
        private void GetDebugItem(int indexString)
        {
            Item retrievedItem = Program.MAINPLAYERINVENTORY.RetrieveItem(indexString);
            string message = "";

            if (retrievedItem.ItemType == ItemType.BasicItem)
            {
                message = string.Format("Name: {0}\nPlural Name: {1}\nDescription: {2}",
                    retrievedItem.Name,
                    retrievedItem.PluralName,
                    retrievedItem.Description);
                
            }
            else if (retrievedItem.ItemType == ItemType.Healing)
            {
                HealingItem retrievedHealing = (HealingItem)retrievedItem;

                message = string.Format("Name: {0}\nPlural Name: {1}\nDescription: {2}\nHitpoints Healed: {3}HP",
                    retrievedHealing.Name,
                    retrievedHealing.PluralName,
                    retrievedHealing.Description,
                    retrievedHealing.HitpointsHealed);
            }
            else if (retrievedItem.ItemType == ItemType.Weapon)
            {
                Weapon retrievedWeapon = (Weapon)retrievedItem;

                message = string.Format("Name: {0}\nPlural Name: {1}\nDescription: {2}\nDamage Dealt: {3}HP\nWeapon Type: {4}",
                    retrievedWeapon.Name,
                    retrievedWeapon.PluralName,
                    retrievedWeapon.Description,
                    retrievedWeapon.Damage,
                    retrievedWeapon.Type);
            }
            else if (retrievedItem.ItemType == ItemType.Glitch)
            {
                message = string.Format("Name: {0}\nPlural Name: {1}\nDescription: {2}",
                    retrievedItem.Name,
                    retrievedItem.PluralName,
                    retrievedItem.Description);
            }
            System.Windows.Forms.MessageBox.Show(message);

            this.ExitScreen();
        }
        //
    }
}
