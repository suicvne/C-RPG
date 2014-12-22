using SimpleGameCliCore.Items.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGameCliCore.Rooms
{
    class PlayerStatusScreen : Room
    {
        public static void DrawStatusScreen(Player pl, InventoryHandler iv)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            for(int i = 0; i < 33; i++)
            {
                Console.Write("#");
            }
            Console.Write("PLAYER");
            for(int i = 0; i < 40; i++)
            {
                Console.Write("#");
            }
            //
            Console.WriteLine("\n\n");
            Console.WriteLine("Player Status\n");
            Console.WriteLine("Name: {0}", pl.Name);
            Console.WriteLine("Gender: {0}", pl.PlayerGender.ToString());
            Console.WriteLine("Hitpoints: {0} / {1}", pl.CurrentHitpoints, pl.MaximumHitpoints);
            Console.WriteLine("Height: {0}", pl.PlayerHeight);
            Console.WriteLine("Weight: {0}", pl.PlayerWeight);
            Console.WriteLine("Equipped Item: {0}", pl.EquippedWeapon.Name);
            //
            Console.WriteLine("\n\nEnter e to equip an item or q to quit: ");
            string input = Console.ReadLine().ToUpper();
            if (input == "E")
            { 
                pl.EquippedWeapon = (Weapon)InventoryScreen.SelectWeapon(iv);
                DrawStatusScreen(pl, iv);
            }
            else if (input == "Q")
            { }
            else
            { }
            //
        }
    }
}
