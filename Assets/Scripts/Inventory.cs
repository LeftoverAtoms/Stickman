using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class Inventory
    {
        public readonly Character Owner;
        public int Slots;

        public List<HeldObject> Objects;

        public Inventory(Character owner)
        {
            Objects = new List<HeldObject>();
            Slots = 2;
            Owner = owner;
        }

        public bool CanAdd() => Objects.Count < Slots || FindOpenSlot(out int i);

        public void Add(HeldObject obj)
        {
            if (Objects.Count < Slots)
            {
                Objects.Add(obj);
            }
            else if (FindOpenSlot( out int i ))
            {
                Objects[i] = obj;
            }
        }

        public void Remove(HeldObject obj)
        {
            obj = null;
        }

        private bool FindUsedSlot( out int slot )
        {
            for (int i = 0; i < Slots; i++)
            {
                if (Objects[i] != null)
                {
                    Debug.Log(Objects[i]);
                    slot = i;
                    return true;
                }
            }
            slot = -1;
            return false;
        }

        private bool FindOpenSlot( out int slot )
        {
            for(int i = 0; i < Slots; i++)
            {
                if (Objects[i] == null)
                {
                    slot = i;
                    return true;
                }
            }
            slot = -1;
            return false;
        }

        public HeldObject Switch()
        {
            FindUsedSlot(out int i);
            return Objects[i];
        }
    }
}