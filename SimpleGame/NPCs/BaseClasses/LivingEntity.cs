
using System;

namespace SimpleGameCliCore.Items
{
	/// <summary>
	/// Description of LivingEntity.
	/// </summary>
	public class LivingEntity
	{
		public int ID {get; set;}
		public string Name {get; set;}
		public int CurrentHitpoints {get; set;}
		public int MaximumHitpoints {get; set;}
        public Item DroppedItem { get; set; }
        public bool CanAttack { get; set; }

        public Item DropItem()
        {
            return DroppedItem;
        }
	}
}
