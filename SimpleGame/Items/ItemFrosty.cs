using SimpleGame.Items.BaseClasses;
using System;

namespace SimpleGame.Items
{
	public class ItemFrosty : HealingItem
	{
        public string Flavour { get; set; }
		
		public ItemFrosty()
		{
			ID = 1;
			Name = "Frosty";
			PluralName = "Frosties";
			HitpointsHealed = 3;
            Description = "An extremely thick shake, essentially ice cream but better.";
            Flavour = "Chocolate";
            ItemType = Items.ItemType.Healing;
		}
	}
}
