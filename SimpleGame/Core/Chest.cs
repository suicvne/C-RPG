using SimpleGameCliCore.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGameCliCore.Core
{
    public class Chest
    {
        List<Item> _chestContents = new List<Item>(20);
        public int ID { get; set; }
        public bool IsNullChest { get; set; }

        public List<Item> ChestContents()
        { return _chestContents; }

        public void LoadChestContents(string file)
        {
            if (ID == null)
            { throw new NullReferenceException("Chest ID Null"); }
            using(var sr = new System.IO.StreamReader(file))
            {
                string chestBeginningLine = string.Format("#CHEST{0}#", ID); //the line that indicates the starting point of the chest
                string line; int lineCountIndex = 0; bool skipRead = false; bool didReadChest = false;
                while((line = sr.ReadLine()) != null)
                {
                    if (lineCountIndex == 0 && !line.StartsWith("#64#")) { break; }
                    if(line.StartsWith("#CHEST"))
                    {
                        if (line == "#INV2#")
                        { }
                        else if (line == chestBeginningLine)
                        {
                            didReadChest = true;
                            while ((line = sr.ReadLine()) != null)
                            {
                                if(line.StartsWith("#"))
                                {
                                    if (line == chestBeginningLine)
                                    { }
                                    else
                                        break;
                                }
                                if (line.Contains(","))
                                {
                                    var split = line.Split(new char[] { ':', ',' }, 2);
                                    int index = int.Parse(split[0]);
                                    int itemID = int.Parse(split[1]);
                                    string customItemName = split[2].Trim('\"');
                                    Item toAdd = ItemMapping.GetItemByID(itemID);
                                    toAdd.InventoryIndex = index;
                                    toAdd.CustomItemName = customItemName;
                                }
                                else
                                {
                                    var split = line.Split(new char[] { ':' }, 2);
                                    int index = int.Parse(split[0]);
                                    int itemID = int.Parse(split[1]);
                                    Item toAdd = ItemMapping.GetItemByID(itemID);
                                    toAdd.InventoryIndex = index;
                                }
                            }
                        }
                    }
                    lineCountIndex++;
                }
                if (!didReadChest)
                    IsNullChest = true;
                else
                    IsNullChest = false;
            }
        }
        //

    }
}
