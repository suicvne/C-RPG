using System;
using System.Threading;

namespace SimpleGameCliCore
{
	/// <summary>
	/// Description of Room.
	/// </summary>
	public class Room
	{
		//80 * 30 is default room size
		public Room()
		{
			Console.Clear();
			for (int h = 0; h < 25; h++)
			{
				for(int i = 0; i < 80; i++)
				{
					Console.Write("#");
					if(i == 79)
					{	/*Console.Write("\n");*/}
				}
			}
		}
		
		public Room(RoomType rt)
		{
			Console.Clear();
			switch(rt)
			{
				case(RoomType.ErrorMessage):
					DrawErrorMessage();
					break;
				case(RoomType.Dialog):
					Console.Clear();
					Console.WriteLine("Wrong one dummy!");
					Console.Read();
					break;
			}
		}
		//
		public Room(RoomType rt, string _main, string _stack, string _solu)
		{
			Console.Clear();
			if(rt == RoomType.ErrorMessage)
			{
				DrawErrorMessage(_main, _stack, _solu);
			}
			else
				Console.WriteLine("You're using the wrong declaration stupid!"); Console.Read();
		}
		public Room(RoomType rt, string _charac, string _message)
		{
			Console.Clear();
			if(rt == RoomType.Dialog)
			{
				DrawDialogScreen(_charac, _message);
			}
			else
				Console.WriteLine("Quit being dumb and use the right declaration!"); Console.Read();
		}
		//
		private void DrawErrorMessage()
		{
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Clear();
			for(int i = 0; i < 33; i++)
			{
				//ERROR
				Console.Write("#");
			}
			Console.Write("ERROR");
			for(int i = 0; i<40; i++)
			{
				Console.Write("#");
			}
			Console.WriteLine();
			
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("I have no idea what happened this time!");
			Console.WriteLine("Stack trace: nothing man");
			Console.WriteLine("\n\nPress any key to let the program crash..");
			
			Console.Read();
			Environment.Exit(-3);

		}
		
		private void DrawErrorMessage(string MainError, string StackTrace, string PossibleSolutions)
		{
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Clear();
			for(int i = 0; i < 33; i++)
			{
				Console.Write("#");
			}
			Console.Write("ERROR");
			for(int i = 0; i < 40; i++)
			{
				Console.Write("#");
			}
			Console.WriteLine();
			//
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("The program has encountered an error");
			Console.WriteLine("\n{0}", MainError);
			Console.WriteLine("Stack Trace: {0}", StackTrace);
			Console.WriteLine("\nPossible Solutions: {0}\n\n\n\n\n", PossibleSolutions);
			Console.WriteLine("Press enter to continue..");
			
			Console.Read();
		}
		
		private void DrawDialogScreen(string charac, string message)
		{
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			//
			Console.WriteLine("\n\nDialog with: {0}\n", charac);
			
			Console.Write("\"");
			for(int i = 0; i < message.Length; i++)
			{
				Console.Write(message[i]);
				Thread.Sleep(0100); //if 1000 is a second
			}
			Console.Write("\"");

            Thread.Sleep(0080);

			Console.WriteLine("\n\nPress enter to continue..");
			Console.Read();
			//
		}
    }
	//
	public enum RoomType
	{
		ErrorMessage, CharacterCreation, Dialog
	}
}
