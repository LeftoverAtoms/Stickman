using UnityEngine;

namespace Stickman
{
    public class Item : Object
    {
        public Character owner;

        public bool usable;
        public bool pickupable;

        public override void Start()
        {
            base.Start();

            collider.isTrigger = true;
            collider.size = new Vector2(0.5f, 0.5f);

            renderer.sprite = sprite;
            renderer.sortingOrder = 1;
        }

        public virtual void Use()
        {
            if(usable) {usable = false;}
        }
    }
}