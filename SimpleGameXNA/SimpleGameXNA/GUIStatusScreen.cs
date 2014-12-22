using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SimpleGameXNA
{
    class GUIStatusScreen : Microsoft.Xna.Framework.Game
    {
        public string USERINPUT = "";

        public string DrawStatusScreen(SpriteBatch spriteBatch, GraphicsDevice GraphicsDevice, SpriteFont mainGameFont)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            spriteBatch.Begin();
            spriteBatch.DrawString(mainGameFont, "PLAYER", new Vector2(GraphicsDevice.DisplayMode.Width / 2 / 2, 0), Color.White);

            string infoScreen = string.Format("Player Status\n\nName: {0}\nGender: {1}\nHitpoints: {2}/{3}\nHeight: {4}\nWeight: {5}\nEquipped Item: {6}",
                GUIProgram.MAINPLAYER.Name,
                GUIProgram.MAINPLAYER.PlayerGender.ToString(),
                GUIProgram.MAINPLAYER.CurrentHitpoints, GUIProgram.MAINPLAYER.MaximumHitpoints,
                GUIProgram.MAINPLAYER.PlayerHeight,
                GUIProgram.MAINPLAYER.PlayerWeight,
                GUIProgram.MAINPLAYER.EquippedWeapon.Name);
            spriteBatch.DrawString(mainGameFont, infoScreen, new Vector2(0, 45), Color.White);
            spriteBatch.DrawString(mainGameFont, "Type e to equip or q to exit", new Vector2(0, 60), Color.White);
            spriteBatch.DrawString(mainGameFont, USERINPUT + "|", new Vector2(40, 500), Color.White);
            if(USERINPUT.Contains('\r'))
            {
                spriteBatch.End();
                return USERINPUT;
            }
            System.Threading.Thread.Sleep(6000);
            spriteBatch.End();
            return null;
        }
    }
}
