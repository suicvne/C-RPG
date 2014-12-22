
using System;

namespace SimpleGame.Items
{
	/// <summary>
	/// Description of Item.
	/// </summary>
	public class Item
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string PluralName { get; set;}
        public string Description { get; set; }
        public ItemType ItemType { get; set; }
	}
    public enum ItemType
    {
        BasicItem, Weapon, Glitch, Healing
    }
}
