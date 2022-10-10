using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class Inventory
    {
        public readonly Character Owner;
        public List<HeldObject> Objects;
        public int Slots;

        public Inventory(Character owner)
        {
            Owner = owner;
            Objects = new List<HeldObject>();
            Slots = 2;
        }

        public bool CanAdd() => Objects.Count < Slots;

        public void Add(HeldObject obj)
        {
            if (CanAdd()) Objects.Add(obj);
        }

        public void Remove(HeldObject obj, out HeldObject next)
        {
            Objects.Remove(obj);

            if (Objects.Count > 0) next = Objects[0];
            else next = null;
        }
    }
}