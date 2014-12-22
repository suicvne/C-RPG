using SimpleGameCliCore.Items;
using SimpleGameCliCore.Items.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGameCliCore.Rooms
{
    class InventoryScreen : Room
    {
        public static void DrawInventoryScreen(Player pl, InventoryHandler iv)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            for (int i = 0; i < 35; i++)
            {
                Console.Write("#");
            }
            Console.Write("INVENTORY");
            for(int i = 0; i < 36; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine("\n\n");
            //

            if (iv.GetCount() != 0)
            {

                for (int i = 0; i < iv.GetCount(); i++)
                {
                    Console.WriteLine("{0}. {1}", i, iv.RetrieveItem(i).Name);
                }
            }
            else
                Console.WriteLine("Your inventory is empty!");

            //Console.WriteLine("\n\nPress enter to exit..");
            Console.WriteLine("\n\nEnter i for information on an item, rm to delete an item, or q to exit: ");
            string input = Console.ReadLine().ToUpper();
            //
            string item;
            int numberIndex;
            //
            switch(input)
            {
                case("+++ADDITEM+++"):
                    Console.WriteLine("Enter the ID of the item you wish to add: ");
                    string toAdd = Console.ReadLine();
                    numberIndex = int.Parse(toAdd);
                    Item addItem = ItemMapping.GetItemByID(numberIndex);
                    iv.AddItem(addItem);
                    Console.WriteLine("Added 1 {0}", addItem.Name);
                    Console.ReadLine();
                    DrawInventoryScreen(pl, iv);
                    break;
                case ("I"):
                    Console.WriteLine("Enter the inventory slot of the item: ");
                    item = Console.ReadLine();
                    numberIndex = int.Parse(item);
                    ItemInformation(pl, iv, numberIndex);
                    break;
                case("RM"):
                    Console.WriteLine("Enter the inventory slot of the item: ");
                    item = Console.ReadLine();
                    numberIndex = int.Parse(item);
                    Console.WriteLine("Are you sure you wish to remove this item? (y/n): ");
                    string yn = Console.ReadLine().ToUpper();
                    switch(yn)
                    {
                        case("Y"):
                            if (pl.EquippedWeapon == iv.RetrieveItem(numberIndex))
                            {
                                Console.WriteLine("You currently have this weapon equipped, are you absolutely sure you want to remove this? (yes to really delete, anything else to not): ");
                                string yn2 = Console.ReadLine().ToUpper();
                                if(yn2 == "YES")
                                {
                                    Item rmItem = iv.RetrieveItem(numberIndex);
                                    iv.RemoveItem(numberIndex);
                                    pl.EquippedWeapon = new WeaponNull();
                                    Console.WriteLine("Removed {0} and equipped nothing", rmItem.Name);
                                    Console.ReadKey();
                                    DrawInventoryScreen(pl, iv);
                                }
                                else
                                {
                                    DrawInventoryScreen(pl, iv);
                                }
                            }
                            else
                            {
                                Item removedItem = iv.RetrieveItem(numberIndex);
                                iv.RemoveItem(numberIndex);
                                Console.WriteLine("Removed " + removedItem.Name);
                                Console.ReadKey();
                                DrawInventoryScreen(pl, iv);
                            }
                            break;
                        case("N"):
                            DrawInventoryScreen(pl, iv);
                            break;
                    }
                    break;
                case ("Q"):
                    //do nothing, exit
                    break;
            }
        }
        //
        public static void ItemInformation(Player pl, InventoryHandler iv, int itemIndex)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            for(int i = 0; i < 34; i++)
            {
                Console.Write("#");
            }
            Console.Write("INFO");
            for(int i = 0; i < 40; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine("\n\n");
            Item retrieveItem = iv.RetrieveItem(itemIndex);
            string mainInfoScreen = "Error";
            switch(retrieveItem.ItemType)
            {
                case(ItemType.BasicItem):
                    mainInfoScreen = string.Format("Item Name: {0}\nItem Plural Name: {1}\nDescription: {2}\n\n", 
                retrieveItem.Name, retrieveItem.PluralName, retrieveItem.Description);
                    break;
                case(ItemType.Weapon):
                    Weapon retrievedWeapon = (Weapon)retrieveItem;
                    mainInfoScreen = string.Format("Item Name: {0}\nItem Plural Name: {1}\nDescription: {2}\nDamage: {3}\nWeapon Type: {4}",
                        retrievedWeapon.Name, retrievedWeapon.PluralName, retrievedWeapon.Description, retrievedWeapon.Damage, retrievedWeapon.Type);
                    break;
                case(ItemType.Healing):
                    HealingItem retrievedHealer = (HealingItem)retrieveItem;
                    mainInfoScreen = string.Format("Item Name: {0}\nItem Plural Name: {1}\nDescription: {2}\nHeals: {3} HP\n\n",
                        retrievedHealer.Name, retrievedHealer.PluralName, retrievedHealer.Description, retrievedHealer.HitpointsHealed);
                    break;
                case(ItemType.Glitch):
                    mainInfoScreen = string.Format("Item Name: {0}\nItem Plural Name: {1}\nDescription: {2}\nID: {3}\n\n",
                retrieveItem.Name, retrieveItem.PluralName, retrieveItem.Description, retrieveItem.ID);
                    break;
            }
            Console.WriteLine(mainInfoScreen);
            Console.WriteLine("\n\nPress enter to exit..");
            Console.ReadKey();
            DrawInventoryScreen(pl, iv);
        }
        //
        public static Item SelectItem(InventoryHandler iv)
        {
            bool allowSelection = false;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            for (int i = 0; i < 35; i++)
            {
                Console.Write("#");
            }
            Console.Write("SELECT");
            for (int i = 0; i < 36; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine("\n\n");
            //

            if (iv.GetCount() != 0)
            {
                for (int i = 0; i < iv.GetCount(); i++)
                {
                    Console.WriteLine("{0}. {1}", i, iv.RetrieveItem(i).Name);
                }
                allowSelection = true;
            }
            else
            { 
                Console.WriteLine("Your inventory is empty!");
                allowSelection = false;
            }

            Console.WriteLine("\n\nEnter item number or q to quit: ");
            string input = Console.ReadLine().ToUpper();
            if (allowSelection)
            {
                if (input == "Q")
                    return ItemMapping.GetItemByID(-2); //-2 is the weapon that maps to nothing
                else
                {
                    Item ret = iv.RetrieveItem(int.Parse(input));
                    return ret;
                }
            }
            else
                return ItemMapping.GetItemByID(-2);
        }
        //
        public static Item SelectWeapon(InventoryHandler iv)
        {
            bool allowSelection = false;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            for (int i = 0; i < 35; i++)
            {
                Console.Write("#");
            }
            Console.Write("SELECT");
            for (int i = 0; i < 36; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine("\n\n");
            //
            int weaponCount = 0;
            if (iv.GetCount() != 0)
            {
                for (int i = 0; i < iv.GetCount(); i++)
                {
                    if (iv.RetrieveItem(i).ItemType == ItemType.Weapon)
                    { Console.WriteLine("{0}. {1}", i, iv.RetrieveItem(i).Name); weaponCount++; }
                }
            }
            else
            {
                /*Console.WriteLine("Your inventory is empty!");
                allowSelection = false;*/
            }

            if (weaponCount <= 0)
            {
                Console.WriteLine("Your inventory contains no weapons!");
                Console.WriteLine("Press enter to exit..");
                Console.ReadLine();
                return new WeaponNull();
            }
            else
            {

                Console.WriteLine("\n\nEnter item number or q to quit: ");
                string input = Console.ReadLine().ToUpper();
                if (allowSelection)
                {
                    if (input == "Q")
                        return ItemMapping.GetItemByID(-2); //-2 is the weapon that maps to nothing
                    else
                    {
                        Item ret = iv.RetrieveItem(int.Parse(input));
                        return ret;
                    }
                }
                else
                    return ItemMapping.GetItemByID(-2);
            }
        }
        //
    }
}
