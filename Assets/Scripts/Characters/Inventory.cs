using System.Collections.Generic;

namespace Stickman
{
    public class Inventory
    {
        private List<Item> Items;
        private List<Weapon> Weapons;

        private Weapon DefaultWeapon;
        public int Slots;

        public Inventory()
        {
            Slots = 2;
            Items = new List<Item>() { Capacity = Slots };
            Weapons = new List<Weapon> { Capacity = Slots };
        }

        public bool HasOpenSlots() => (Items.Count + Weapons.Count > Slots);

        public bool Add(Item obj)
        {
            if(this.HasOpenSlots()) return false;

            if(obj is Weapon wpn) Weapons.Add(wpn);
            else if(obj is Item) Items.Add(obj);
            else return false;

            return true;
        }

        public bool Remove(Item obj) => (Items.Remove(obj));
        public void Remove(Item obj, out Item next)
        {
            Items.Remove(obj);
            next = DefaultWeapon;
        }
    }
}