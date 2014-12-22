using System;
using System.Collections.Generic;
using SimpleGame.Items;

namespace SimpleGame
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
                    sw.WriteLine(item.ID.ToString());
                }
                sw.Flush();
                sw.Close();
            }
        }
        public void ReadFromFile(string file)
        {
            _inventory.Clear();
            int MAXCAPACITY = _inventory.Capacity;
            using(var sr = new System.IO.StreamReader(file))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        int id = int.Parse(line);
                        Item add = ItemMapping.GetItemByID(id);
                        if(_inventory.Count <= MAXCAPACITY)
                            _inventory.Add(add);
                    }
                    catch
                    {
                        _inventory.Add(new ItemNull());
                    }
                }
            }
        }
        //
	}
}
