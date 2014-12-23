using System;
using SimpleGameCliCore;

namespace SimpleGameXNA
{
#if WINDOWS || XBOX
    static class Program
    {
        static string dirSepChar = System.IO.Path.DirectorySeparatorChar.ToString();
        public static string GamesSaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            + dirSepChar + "My Games" + dirSepChar + "C#RPG";
        static Player MAINPLAYER = new Player();
        static InventoryHandler MAINPLAYERINVENTORY = new InventoryHandler(20);

        static void Main(string[] args)
        {
            Game MainGame = new Game();
            MainGame.Run();
        }
    }
#endif
}

