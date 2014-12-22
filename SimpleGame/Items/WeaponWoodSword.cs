using SimpleGame.Items.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGame.Items
{
    class WeaponWoodSword : Weapon
    {
        public WeaponWoodSword()
        {
            Name = "Wooden Sword";
            PluralName = "Wooden Swords";
            ID = 2;
            Description = "A basic wooden sword, what more could you possibly want?";
            Type = WeaponType.Melee;
            Damage = 1;
            ItemType = Items.ItemType.Weapon;
        }
    }
}
