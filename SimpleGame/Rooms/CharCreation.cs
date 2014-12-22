using System;

namespace SimpleGame
{
	public class CharCreation : Room
	{
		static Player res;
		static Random _ran = new Random((int)(DateTime.Now.Millisecond));
		
		public static Player CreateCharacter()
		{
			string _name;
			double _height = 0;
			double _weight = 0;
			Gender _gen;
			//Welcome message
			ResetView();
			Console.WriteLine("\n\nWelcome to 'The Meme Adventures of Misty and Nicole'\nThis is a text based game created by Mike Santiago.");
			//Name
			ResetView();
			Console.WriteLine("\n\nWe will now proceed to create your character");
			Console.Write("\nWhat is your name?: ");
			_name = Console.ReadLine();
			//Height
		GetPlayerHeight:
			ResetView();
			Console.WriteLine("\n\nNice to meet you, {0}.", _name);
			Console.Write("\nWhat is your height? (type ran to generate random height): ");
			string resu = Console.ReadLine();
			if(resu == "ran")
				_height = ranHeight(4.3, 6.5);
			else
			{
				try
				{
					_height = double.Parse(resu);
				}
				catch
				{
					Room err = new Room(RoomType.ErrorMessage, "An error occurred while parsing the player's height",
					                    string.Format("Error in CreateCharacter\nTried to parse {0} and failed.", resu), 
					                    "Only enter single decimal numbers and no letters, save for typing ran to create a random height");
					goto GetPlayerHeight;
				}
			}
			//Weight
		GetPlayerWeight:
			ResetView();
			Console.WriteLine("\n\nYou are {0}, interesting", _height);
			Console.Write("\nHow much do you weigh? (type ran to generate random weight): ");
			resu = Console.ReadLine();
			if(resu == "ran")
				_weight = ranHeight(90, 250);
			else
			{
				try
				{
					_weight = double.Parse(resu);
				}
				catch
				{
					Room err = new Room(RoomType.ErrorMessage, "An error occurred while parsing the player's weight",
					                    string.Format("Error in CreateCharacter\nTried to parse {0} and failed.", resu), 
					                    "Only enter single decimal numbers and no letters, save for typing ran to create a random weight");
					goto GetPlayerWeight;
				}
			}
			//Gender
			ResetView();
			Console.WriteLine("\n\nYou weigh {0}.", _weight);
			Console.Write("\nWhat is your gender? (m/f/u): ");
			resu = Console.ReadLine();
			if(resu == "m")
				_gen = Gender.Male;
			else if(resu == "f")
				_gen = Gender.Female;
			else
				_gen = Gender.Unknown;
			//
		RepeatInput:
			ResetView();
			Console.WriteLine("\n\nBefore we move on, let's take a moment to review");
			Console.WriteLine("Name: {0}\nHeight: {1}\nWeight: {2}\nGender: {3}\n\n", _name, _height, _weight, _gen);
			Console.Write("Is this ok? (y/n/c), cancel will close the program, no will redo generation: ");
			resu = Console.ReadLine();
			if(resu == "y")
			{//continue and create the character!
			}
			else if(resu == "n")
				CreateCharacter();
			else if(resu == "c")
				Environment.Exit(0);
			else
				goto RepeatInput;
			//
			res = new Player();
			res.Name = _name;
			res.PlayerGender = _gen;
			res.PlayerHeight = _height;
			res.PlayerWeight = _weight;
            //
            System.IO.Directory.CreateDirectory(Program.GamesSaveDirectory + System.IO.Path.DirectorySeparatorChar + _name);
            res.WriteToFile(Program.GamesSaveDirectory + System.IO.Path.DirectorySeparatorChar + _name + System.IO.Path.DirectorySeparatorChar + "player.sav");
            //
			return res;
		}
		
		private static double ranHeight(double min, double max)
		{
			//_ran = new Random((int)(DateTime.Now.Millisecond));
			if (min >= max)
            	throw new ArgumentOutOfRangeException();    
			return Math.Round(_ran.NextDouble() * (Math.Abs(max-min)) + min, 1);
		}
		
		private static void ResetView()
		{
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.DarkMagenta;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			
			Console.ForegroundColor = ConsoleColor.Green;
			for (int i = 0; i < 24; i++)
			{
				Console.Write("#");
			}
			Console.Write("CHARACTERCREATE");
			for(int i = 0; i < 39; i++)
			{
				Console.Write("#");
			}
			Console.ForegroundColor = ConsoleColor.White;
			//
		}
	}
}
