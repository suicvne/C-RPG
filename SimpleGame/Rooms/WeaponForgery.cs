using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SimpleGameCliCore.Rooms
{
    class WeaponForgery : Room
    {
        public static void DrawForgeryScreen(Player pl, InventoryHandler iv)
        {
BEGINNINGOFLOOP:
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            for(int i = 0; i < 33; i++)
            { Console.Write("#"); }
            Console.Write("FORGE");
            for (int i = 0; i < 40; i++)
            { Console.Write("#"); }
            //
            Console.WriteLine("\n\n");
            string forgeWelcomeMessage = "Welcome to the forge! What would you like to do?";
            for(int i = 0; i < forgeWelcomeMessage.Length; i++)
            {
                Console.Write(forgeWelcomeMessage[i]);
                Thread.Sleep(70);
            }
            Console.Write("\nType name to rename a weapon, or q to quit: ");
            string input = Console.ReadLine().ToUpper();
            if (input == "NAME")
            {
                Items.Item itemToRename = InventoryScreen.SelectWeapon(iv);
                if(itemToRename.ID == -2)
                {
                    Console.WriteLine("\nYou can't rename your hands!\n");
                    Console.ReadLine();
                    goto BEGINNINGOFLOOP;
                }
                else if(itemToRename.ItemType == Items.ItemType.Weapon)
                {
                    Console.Write("\nEnter the new item name and press enter: ");
                    string newItemName = Console.ReadLine();
                    itemToRename.CustomItemName = newItemName;
                    iv.RemoveItem(itemToRename.InventoryIndex);
                    itemToRename.InventoryIndex = iv.GetCount() - 1;
                    iv.AddItem(itemToRename);
                    Console.WriteLine("Item '{0}' renamed to '{1}' successfully!", itemToRename.Name, newItemName);
                    Console.ReadLine();
                    goto BEGINNINGOFLOOP;
                }
            }
            else if (input == "Q")
            { }
            else
                goto BEGINNINGOFLOOP;
            //
        }
        //

    }
}
