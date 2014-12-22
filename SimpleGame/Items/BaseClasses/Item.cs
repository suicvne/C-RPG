using System;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleGameCliCore.Items
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
        public Texture2D ItemSprite { get; set; }
	}
    public enum ItemType
    {
        BasicItem, Weapon, Glitch, Healing
    }
}
