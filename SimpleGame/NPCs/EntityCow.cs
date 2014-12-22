using SimpleGameCliCore.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGameCliCore.NPCs
{
    class EntityCow : LivingEntity
    {

        public EntityCow()
        {
            ID = 1;
            Name = "Cow";
            MaximumHitpoints = 5;
            CurrentHitpoints = 5;
            DroppedItem = new ItemCowMeat();
            CanAttack = false;
        }

    }
}
