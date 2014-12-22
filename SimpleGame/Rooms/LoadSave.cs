using System;
using System.Collections.Generic;

namespace SimpleGame.Rooms
{
	/// <summary>
	/// Description of LoadSave.
	/// </summary>
	public class LoadSave : Room
	{
		static List<string> saves = new List<string>();
		
		public static string DrawLoadSaveView()
		{
            VERYBEGINNING:
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Clear();
			//
			for(int i = 0; i < 33; i++)
			{
				Console.Write("#");
			}
			Console.Write("LOADSAVE");
			for(int i = 0; i < 37; i++)
			{
				Console.Write("#");
			}
			//
			int saveCount = 0;

            Console.WriteLine("\n\n");

            saves.Clear();

			foreach(var i in System.IO.Directory.GetDirectories(Program.GamesSaveDirectory))
			{
                if(System.IO.File.Exists(i + System.IO.Path.DirectorySeparatorChar + "player.sav"))
                {
                    Console.WriteLine("{0}: {1}\n", saveCount, System.IO.Path.GetFileNameWithoutExtension(i));
                    saves.Add(i);
                    saveCount++;
                }
			}
			
			Console.Write("\n\nSelect the number of the save, or type c to make a new character: ");
			string selection = Console.ReadLine();
            if (selection.ToUpper() == "C")
                return "DONTLOADASAVECREATEACHARACTER";
			int sel = 0;
			try
			{
				sel = int.Parse(selection);
                string selectedSave = saves[sel];
                return selectedSave; //this is the directory. from here, we can add + "player.inv" or + "player.sav"
			}
			catch
			{
				Room r = new Room(RoomType.ErrorMessage,
				                  "An error occurred while trying to parse your selection '" + selection + "'",
				                  "Unavailable",
				                  "Only enter a number in the selection");
				//ResetView();
                //return null;
                goto VERYBEGINNING;
			}
			//
		}

        public static bool SAVESEXIST()
        {
            saves.Clear();
            int saveCount = 0;
            foreach (var i in System.IO.Directory.GetDirectories(Program.GamesSaveDirectory))
            {
                //we look for .sav because that's the important thing. .inv is disposable and could be regenerated later if need be
                if (System.IO.File.Exists(i + System.IO.Path.DirectorySeparatorChar + "player.sav"))
                {
                    Console.WriteLine("{0}: {1}\n", saveCount, System.IO.Path.GetFileNameWithoutExtension(i));
                    saves.Add(i);
                    saveCount++;
                }
            }
            if (saveCount > 0)
                return true;
            else
                return false;
        }
        //
	}
}
