using System.Collections.Generic;

namespace Stickman
{
    public class Inventory
    {
        public Character Owner;
        public List<Object> Items;
        public Object ActiveItem;

        public int Space = 2;

        public Inventory(Character owner)
        {
            Items = new List<Object>();
            Owner = owner;
        }

        public bool CanAdd()
        {
            if (Items.Count < Space)
            {
                return true;
            }
            return false;
        }

        public void Add(Object obj)
        {
            Items.Add(obj);
        }

        public void Remove(Object obj)
        {
            Owner.Unequip(obj);
            Items.Remove(obj);
        }
    }
}