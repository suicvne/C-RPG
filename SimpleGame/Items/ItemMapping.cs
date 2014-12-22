using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGame.Items
{
    class ItemMapping
    {
        public static Item GetItemByID(int ID)
        {
            switch(ID)
            {
                case(-2):
                    return new WeaponNull(); //aka nothing equipped
                case(1):
                    return new ItemFrosty();
                case(2):
                    return new WeaponWoodSword();
                case(3):
                    return new ItemCowMeat();
                case(123456789):
                    return new WeaponMassiveDong();
            }
            return new ItemNull();
        }

    }
}
