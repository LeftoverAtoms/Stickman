using UnityEngine;

namespace Stickman
{
    public class HeldObject : Object
    {
        public ScriptableItem Attribute;
        public ItemState State;

        public virtual void Use()
        {
            if (State != ItemState.Usable)
                return;

            State = ItemState.Used;
        }
    }

    public enum ItemState
    {
        Usable, Used, Collectable
    }
}