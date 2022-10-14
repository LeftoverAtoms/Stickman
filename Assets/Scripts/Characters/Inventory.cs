using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class Inventory
    {
        public readonly Character Owner;
        public List<Item> Objects;
        public int Slots;

        public Inventory(Character owner)
        {
            Owner = owner;
            Objects = new List<Item>();
            Slots = 2;
        }

        public bool CanAdd() => Objects.Count + 1 < Slots;

        public void Add(Item obj)
        {
            if (CanAdd()) Objects.Add(obj);
        }

        public void Remove(Item obj, out Item next)
        {
            if (obj == Objects[0])
            {
                next = Objects[0];
                return;
            }

            Objects.Remove(obj);

            if (Objects.Count > 0) next = Objects[0];
            else next = null;
        }
    }
}