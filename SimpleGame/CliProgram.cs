using SimpleGameCliCore.Rooms;
using System;

namespace SimpleGameCliCore
{
	public class CliProgram
	{
		static string dirSepChar = System.IO.Path.DirectorySeparatorChar.ToString();
		public static string GamesSaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			+ dirSepChar + "My Games" + dirSepChar + "C#RPG";
        //
        public static InventoryHandler MAINPLAYERINVENTORY = new InventoryHandler(20);
        static Player mainPlayer = new Player();
        // You can obtain the player's directory post loading a save simply by referencing the following
        // GamesSaveDirectory + dirSepChar + mainPlayer.Name + dirSepChar + "player.sav"
        // Inventory works the same way, except change "player.sav" to "player.inv"
			
		public static void Main(string[] args)
		{
            VERYBEGINNING:
            if(!System.IO.Directory.Exists(GamesSaveDirectory))
				FirstRunEvents();

            if (LoadSave.SAVESEXIST())
            {
                string loadedChar = LoadSave.DrawLoadSaveView();
                if (loadedChar == "DONTLOADASAVECREATEACHARACTER")
                {
                    Player createdChar = CharCreation.CreateCharacter();
                    createdChar.WriteToFile(GamesSaveDirectory + dirSepChar + createdChar.Name + dirSepChar + "player.sav");
                    MAINPLAYERINVENTORY.WriteToFile(GamesSaveDirectory + dirSepChar + createdChar.Name + dirSepChar + "player.inv");
                    goto VERYBEGINNING;
                }
                else
                {
                    MAINPLAYERINVENTORY.ReadFromFile(loadedChar + dirSepChar + "player.inv");
                    mainPlayer.ReadFromFile(loadedChar + dirSepChar + "player.sav");
                }
            }
            else
            {
                Console.Clear();
                Console.Write("No characters could be found. You will have to make one now. Is this okay? (y/n, no will exit the game): ");
                string input = Console.ReadLine().ToUpper();
                if (input == "Y")
                {
                    Player createdChar = CharCreation.CreateCharacter();
                    createdChar.WriteToFile(GamesSaveDirectory + dirSepChar + createdChar.Name + dirSepChar + "player.sav");
                    MAINPLAYERINVENTORY.WriteToFile(GamesSaveDirectory + dirSepChar + createdChar.Name + dirSepChar + "player.inv");
                    goto VERYBEGINNING;
                }
                else
                {
                    Environment.Exit(0);
                }
                goto VERYBEGINNING;
            }

            while (true)
            {
                GetCommandInput();
            }
		}

        public static void blah()
        {

        }

        public static void GetCommandInput()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            for (int i = 0; i < 33; i++)
                Console.Write("#");
            Console.Write("INPUT");
            for (int i = 0; i < 40; i++)
                Console.Write("#");

            Console.WriteLine("\n\nPlease enter the command you'd like to execute: ");
            string input = Console.ReadLine().ToUpper();
            switch (input)
            {
                case ("INVENTORY"):
                    InventoryScreen.DrawInventoryScreen(mainPlayer, MAINPLAYERINVENTORY);
                    break;
                case("STATUS"):
                    PlayerStatusScreen.DrawStatusScreen(mainPlayer, MAINPLAYERINVENTORY);
                    break;
                case("FORGE"):
                    WeaponForgery.DrawForgeryScreen(mainPlayer, MAINPLAYERINVENTORY);
                    break;
                case ("QUIT"):
                    ExitEvents();
                    Environment.Exit(0);
                    break;
                case ("EXIT"):
                    ExitEvents();
                    Environment.Exit(0);
                    break;
                case ("BYE"):
                    ExitEvents();
                    Environment.Exit(0);
                    break;
            }
        }

        static void ExitEvents()
        {
            //Save everything
            mainPlayer.WriteToFile(GamesSaveDirectory + dirSepChar + mainPlayer.Name + dirSepChar + "player.sav");
            MAINPLAYERINVENTORY.WriteToFile(GamesSaveDirectory + dirSepChar + mainPlayer.Name + dirSepChar + "player.inv");
        }

		static void FirstRunEvents()
		{
			try
			{
				System.IO.Directory.CreateDirectory(
					Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + dirSepChar
					+ "My Games"
				);
				System.IO.Directory.CreateDirectory(GamesSaveDirectory);
			}
			catch(UnauthorizedAccessException ex)
			{
				Room r = new Room(RoomType.ErrorMessage, "An error occurred while trying to create the saves directory!",
				                  "UnauthorizedAccessException in FirstRunEvents",
				                  "Try running the program as administrator to allow access to the Documents directory"
				                 );
				Environment.Exit(-6);
			}
			
			//
			
		}
	}
}