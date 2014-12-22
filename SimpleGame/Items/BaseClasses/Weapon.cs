using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGame.Items.BaseClasses
{
    public class Weapon : Item
    {
        public int Damage { get; set; }
        public WeaponType Type { get; set; }
    }
    public enum WeaponType
    {
        Melee, Ranged, NA
    }
}
