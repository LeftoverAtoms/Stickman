using System.Collections.Generic;

namespace Stickman
{
    public class Inventory
    {
        public readonly Character Owner;
        public readonly int Slots;

        public Item ActiveItem;
        public List<Item> Items;

        public Inventory(Character owner)
        {
            Items = new List<Item>();
            Slots = 2;
            Owner = owner;
        }

        public bool CanAdd()
        {
            if (Items.Count < Slots)
            {
                return true;
            }
            return false;
        }

        public void Add(Item obj)
        {
            Items.Add(obj);
        }

        public void Remove(Item obj)
        {
            Owner.Unequip(obj);
            Items.Remove(obj);
        }
    }
}