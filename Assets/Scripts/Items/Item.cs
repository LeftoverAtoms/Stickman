using UnityEngine;

namespace Stickman
{
    public class Item : Object
    {
        public ScriptableItem Attribute;
        public ItemState State;

        public virtual void Use()
        {
            if (State != ItemState.Usable)
                return;

            Owner.State = PawnState.Attacking;
            State = ItemState.Used;
        }
    }

    public enum ItemState
    {
        Usable, Used, Collectable
    }
}