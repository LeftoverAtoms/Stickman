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

        ///<summary>TODO: This function seems odd, rename or set attribute in constructor?</summary>
        public void SetAttributes(ScriptableItem attributes) => Attribute = attributes;
    }

    public enum ItemState
    {
        Usable, Used, Collectable
    }
}