using System.Collections.Generic;

namespace Stickman
{
    public class Inventory
    {
        private List<Item> Items;

        private Weapon DefaultWeapon;
        public int Slots;

        public Inventory()
        {
            Slots = 2;
            Items = new List<Item>();
        }

        public bool CanAppend => (Items.Count > Slots);

        public bool Add(Item obj)
        {
            if(CanAppend) {return false;}
            else {Items.Add(obj); return true;}
        }

        public void Remove(Item obj, out Item next) {Items.Remove(obj); next = DefaultWeapon;}
        public bool Remove(Item obj) => (Items.Remove(obj));
    }
}