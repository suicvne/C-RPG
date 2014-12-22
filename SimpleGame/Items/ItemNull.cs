using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGame.Items
{
    class ItemNull : Item
    {
        public ItemNull()
        {
            Name = "Null Item";
            PluralName = "Brokens";
            ID = -1;
            Description = "If you've come across this item, you've broken the game. Dispose of this immediately.";
            ItemType = Items.ItemType.Glitch;
        }
    }
}
