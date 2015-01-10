using System;
using System.Collections.Generic;
using SimpleGameCliCore.Items;

namespace SimpleGameCliCore
{
	public class InventoryHandler
	{
		private List<Item> _inventory = new List<Item>(20);
		public InventoryHandler()
		{
			_inventory = new List<Item>(20);
		}
		public InventoryHandler(int MaxItems)
		{
			_inventory = new List<Item>(MaxItems);
		}
		//
		public void AddItem(Item add)
		{
			_inventory.Add(add);
		}
		public void AddItem(Item[] items)
		{
			foreach(var i in items)
			{
				_inventory.Add(i);
			}
		}
		//
        public bool ItemExists(Item item)
        {
            foreach(var i in _inventory)
            {
                if(i == item)
                    return true;
                else
                    return false;
            }
            return false;
        }
        //
		public Item RetrieveItem(Item item)
		{
			foreach(var i in _inventory)
			{
				if(i == item)
					return i;
			}
			return null;
		}
		public Item RetrieveItem(int index)
		{
			return _inventory[index];
		}
		//
		public int GetMaxItems()
		{
			return _inventory.Capacity;
		}
        public int GetCount()
        {
            return _inventory.Count;
        }
		public void RemoveItem(Item item)
		{
			foreach(var i in _inventory)
			{
				if(i == item)
					_inventory.Remove(i);
			}
		}
        public void RemoveItem(int item)
        {
            _inventory.RemoveAt(item);
        }
		//
        public void WriteToFile(string destination)
        {
            using(var sw = new System.IO.StreamWriter(destination))
            {
                foreach(var item in _inventory)
                {
                    if(item.CustomItemName != null)
                        sw.WriteLine("{0},\"{1}\"", item.ID.ToString(), item.CustomItemName);
                    else
                        sw.WriteLine(item.ID.ToString());
                }
                sw.Flush();
                sw.Close();
            }
        }
        /// <summary>
        /// This reads strictly inventory; no chest
        /// </summary>
        /// <param name="file"></param>
        public void ReadFromFile_NEWPROTOTYPE(string file)
        {
            _inventory.Clear();
            int MAXCAPACITY = _inventory.Capacity;
            using(var sr = new System.IO.StreamReader(file))
            {
                string line; int lineCountIndex = 0;
                while((line = sr.ReadLine()) != null)
                {
                    if (lineCountIndex == 0 && !line.StartsWith("#64#"))
                    {
                        Room err = new Room(RoomType.ErrorMessage, "", "", ""); //old file thingy
                        break;
                    }
                    if(line.StartsWith("//") || line.Contains("#CHEST") || line.StartsWith("#64#") || line.Contains("#INV2#")) //stop at chests, for obvious reasons and ignore comments
                    {
                        if (line.StartsWith("#CHEST"))
                        { break; }
                    }
                    else
                    {
                        if(line.Contains(","))
                        {
                            //Lines should look like 0:2,"Name"
                            //So the array would look like this
                            //split[0]: Slot (0)
                            //split[1]: ID (2)
                            //split[2]: Name ("Name")
                            var split = line.Split(new char[] { ':', ',' }, 3);
                            try
                            {
                                int slot = int.Parse(split[0]);
                                int ID = int.Parse(split[1]);
                                string customItemName = split[2].Trim('\"');
                                Item toAdd = ItemMapping.GetItemByID(ID);
                                toAdd.CustomItemName = customItemName; toAdd.InventoryIndex = slot;
                                _inventory.Add(toAdd);
                            }
                            catch
                            { Room err = new Room(RoomType.ErrorMessage, "", "", ""); /*files screwed*/ }
                        }
                        else
                        {
                            //Lines should look like 0:2
                            //So the array would look like this
                            //split[0]: Slot (0)
                            //split[1]: ID (2)
                            var split = line.Split(new char[] {':'}, 2);
                            try
                            {
                                int slot = int.Parse(split[0]);
                                int ID = int.Parse(split[1]);
                                Item toAdd = ItemMapping.GetItemByID(ID);
                                toAdd.InventoryIndex = slot;
                                _inventory.Add(toAdd);
                            }
                            catch
                            { Room err = new Room(RoomType.ErrorMessage, "", "", ""); /*files screwed*/ }
                        }
                    }
                    lineCountIndex++;
                }
            }
        }

        public bool IsValidFile(string file)
        {
            using (var sr = new System.IO.StreamReader(file))
            {
                string line; int lineCountIndex = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (lineCountIndex == 0 && line == ("#64#"))
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public void WriteToFile_NEWPROTOTYPE(string file)
        {
            using(var sw = new System.IO.StreamWriter(file))
            {
                sw.WriteLine("#64#");
                //writing inventory
                sw.WriteLine("#INV2#");
                for(int i = 0; i < _inventory.Count; i++)
                {
                    Item retrieve = _inventory[i];
                    if(retrieve.CustomItemName != null)
                    {
                        sw.WriteLine("{0}:{1},\"{2}\"", i, retrieve.ID, retrieve.CustomItemName);
                    }
                    else
                    {
                        sw.WriteLine("{0}:{1}", i, retrieve.ID);
                    }
                }
                //here, we would do chests when they're implemented.
                int chestNumber = 0;
                if(CliProgram.MainChestHandler.ChestsList().Count > 0)
                {
                    bool wroteChestBeginning = false;
                    for(int i = 0; i < CliProgram.MainChestHandler.ChestsList().Count; i++)
                    {
                        if(!wroteChestBeginning)
                        {
                            sw.WriteLine("#CHEST{0}#", chestNumber);
                            wroteChestBeginning = true;
                        }
                        for(int x = 0; i < CliProgram.MainChestHandler.ChestsList()[i].ChestContents().Count; x++)
                        {
                            Item retrieve = CliProgram.MainChestHandler.ChestsList()[i].ChestContents()[x];
                            if(retrieve.CustomItemName != null)
                            {
                                sw.WriteLine("{0}:{1},\"{2}\"", x, retrieve.ID, retrieve.CustomItemName);
                            }
                            else
                            {
                                sw.WriteLine("{0}:{1}", x, retrieve.ID);
                            }
                        }
                        wroteChestBeginning = false;
                    }
                }
                sw.Flush();
                sw.Close();
            }
            //
        }

        public void ReadFromFile(string file)
        {
            _inventory.Clear();
            int MAXCAPACITY = _inventory.Capacity;
            using(var sr = new System.IO.StreamReader(file))
            {
                string line;
                int lineCountIndex = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(","))
                    {
                        var split = line.Split(new char[] {','}, 2);
                        string customItemName = split[1].Trim(new char[]{'\"'});
                        try
                        {
                            int id2 = int.Parse(split[0]);
                            Item add2 = ItemMapping.GetItemByID(id2);
                            add2.CustomItemName = customItemName;
                            add2.InventoryIndex = lineCountIndex;
                            if (_inventory.Count <= MAXCAPACITY)
                                _inventory.Add(add2);
                        }
                        catch
                        {
                            Item nullItem = new ItemNull();
                            nullItem.InventoryIndex = lineCountIndex;
                            _inventory.Add(new ItemNull());
                        }
                    }
                    else
                    {
                        try
                        {
                            int id = int.Parse(line);
                            Item add = ItemMapping.GetItemByID(id);
                            add.InventoryIndex = lineCountIndex;
                            if (_inventory.Count <= MAXCAPACITY)
                                _inventory.Add(add);
                        }
                        catch
                        {
                            Item nullItem = new ItemNull();
                            nullItem.InventoryIndex = lineCountIndex;
                            _inventory.Add(new ItemNull());
                        }
                    }
                    lineCountIndex++;
                }
            }
        }
        //
	}
}
