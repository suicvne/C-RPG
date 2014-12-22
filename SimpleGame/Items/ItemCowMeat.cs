using SimpleGameCliCore.Items.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGameCliCore.Items
{
    class ItemCowMeat : HealingItem
    {
        public ItemCowMeat()
        {
            ID = 3;
            Name = "Cow Meat";
            PluralName = "Cow Meats";
            Description = "Meat dropped from a cow. Not very filling";
            ItemType = Items.ItemType.Healing;
            HitpointsHealed = 1;
        }
    }
}
