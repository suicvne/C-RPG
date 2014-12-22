using SimpleGame.Items.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGame.Items
{
    class WeaponNull : Weapon
    {
        public WeaponNull()
        {
            ID = -2;
            Name = "Nothing";
            Description = "Nothing";
            Damage = 0;
            PluralName = "Nothing";
            ItemType = Items.ItemType.Weapon;
            Type = WeaponType.NA;
        }
    }
}
