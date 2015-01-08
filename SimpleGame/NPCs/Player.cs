using System;
using System.IO;
using SimpleGameCliCore.Items;
using SimpleGameCliCore.Items.BaseClasses;

namespace SimpleGameCliCore
{
    public enum PlayerStatus
    {
        OK, Dead
    }

	public class Player : LivingEntity
	{
		public double PlayerHeight {get; set;}
		public double PlayerWeight {get; set;}
		public Gender PlayerGender {get; set;}
        public Weapon EquippedWeapon { get; set; }
        public PlayerStatus CurrentPlayerStatus { get; set; }

		public Player()
		{
			ID = 0;
			Name = "Fido";
			CurrentHitpoints = 10;
			MaximumHitpoints = 10;
            EquippedWeapon = new WeaponNull();
            CurrentPlayerStatus = PlayerStatus.OK;
		}
		
		//we have to write the player's information and 
		//the inventory items
		public void WriteToFile(string destination)
		{
			using(var sw = new StreamWriter(destination))
			{
                sw.WriteLine(Name);
                sw.WriteLine(PlayerHeight);
                sw.WriteLine(PlayerWeight);
                sw.WriteLine(PlayerGender);
                sw.WriteLine(MaximumHitpoints);
                sw.WriteLine(CurrentHitpoints);
                if (EquippedWeapon.ID == -2)
                    sw.WriteLine(-2);
                else
                    sw.WriteLine(EquippedWeapon.InventoryIndex);
                sw.WriteLine((int)CurrentPlayerStatus);
                sw.Flush();
                sw.Close();
			}
		}
        public void ReadFromFile(string file)
        {
            using(var sr = new StreamReader(file))
            {
                ///Line mapping
                ///0: name
                ///1: height
                ///2: weight
                ///3: gender
                ///4: maxhp
                ///5: curhp
                ///6: EquippedWeapon ID
                ///7: CurrentPlayerStatus as int
                string line;
                int lineCount = 0;
                while((line = sr.ReadLine()) != null)
                {
                    switch(lineCount)
                    {
                        case(0):
                            Name = line;
                            break;
                        case(1):
                            try
                            {
                                double h = double.Parse(line);
                                PlayerHeight = h;
                            }
                            catch
                            {
                                Room err = new Room(RoomType.ErrorMessage, 
                                    "An error occurred while reading your save file for height", 
                                    "Error in ReadFromFile trying to parse '" + line + "'", 
                                    "Stop messing around with your saves!");
                            }
                            break;
                        case(2):
                            try
                            {
                                double w = double.Parse(line);
                                PlayerWeight = w;
                            }
                            catch
                            {
                                Room err = new Room(RoomType.ErrorMessage,
                                    "An error occurred while reading your save file for weight",
                                    "Error in ReadFromFile trying to parse '" + line + "'",
                                    "Stop messing around with your saves!");
                            }
                            break;
                        case(3):
                            try
                            {
                                Gender gen = GenderClass.ParseGender(line);
                                PlayerGender = gen;
                            }
                            catch
                            {
                                Room err = new Room(RoomType.ErrorMessage,
                                    "An error occurred while reading your save file for gender",
                                    "Error in ReadFromFile trying to parse '" + line + "'",
                                    "Stop messing around with your saves!");
                            }
                            break;
                        case(4):
                            try
                            {
                                int maxHP = int.Parse(line);
                                MaximumHitpoints = maxHP;
                            }
                            catch
                            {
                                Room err = new Room(RoomType.ErrorMessage,
                                    "An error occurred while reading your save file for MaximumHitpoints",
                                    "Error in ReadFromFile trying to parse '" + line + "'",
                                    "Stop messing around with your saves!");
                            }
                            break;
                        case(5):
                            try
                            {
                                int curHP = int.Parse(line);
                                CurrentHitpoints = curHP;
                            }
                            catch
                            {
                                Room err = new Room(RoomType.ErrorMessage,
                                    "An error occurred while reading your save file for CurrHitpoints",
                                    "Error in ReadFromFile trying to parse '" + line + "'",
                                    "Stop messing around with your saves!");
                            }
                            break;
                        case(6): //equipped weapon
                            try
                            {
                                int ID = int.Parse(line);
                                if (ID == -2)
                                    EquippedWeapon = new WeaponNull();
                                else
                                    EquippedWeapon = (Weapon)CliProgram.MAINPLAYERINVENTORY.RetrieveItem(ID);
                            }
                            catch
                            {
                                Room err = new Room(RoomType.ErrorMessage,
                                    "An error occurred while reading your save file for EquippedWeapon",
                                    "Error in ReadFromFile trying to parse '" + line + "'",
                                    "Stop messing around with your saves!");
                            }
                            break;
                        case(7):
                            try
                            {
                                int status = int.Parse(line);
                                CurrentPlayerStatus = (PlayerStatus)status;
                            }
                            catch
                            {
                                Room err = new Room(RoomType.ErrorMessage,
                                    "An error occurred while reading your save file for CurrentPlayerStatus",
                                    "Error in ReadFromFile trying to parse '" + line + "'",
                                    "Stop messing around with your saves!");
                            }
                            break;
                    }
                    lineCount++;
                }
            }
        }
		//
	}
}
