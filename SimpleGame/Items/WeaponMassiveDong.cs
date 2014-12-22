using SimpleGameCliCore.Items.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGameCliCore.Items
{
    class WeaponMassiveDong : Weapon
    {
        public WeaponMassiveDong()
        {
            ID = 123456789;
            Name = "Massive Dong";
            PluralName = "There can only be one";
            Description = "Nicole's favourite, and what Mike doesn't have";
            ItemType = Items.ItemType.Weapon;
            Type = WeaponType.Melee;
            Damage = 100;
        }
    }
}
