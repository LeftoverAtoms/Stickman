using UnityEngine;

namespace Stickman
{
    public class Item : Object
    {
        private ScriptableItem Attribute;

        public ItemState State;

        protected override void Start()
        {
            Collider = gameObject.AddComponent<CircleCollider2D>();

            base.Start();

            Body.isKinematic = true;

            if (Collider is CircleCollider2D circle)
            {
                circle.isTrigger = true;
                circle.radius = 0.5f;
            }

            Renderer.sprite = Attribute.Sprite;
            Renderer.sortingOrder = 1;
        }

        public virtual void Use()
        {
            if (State != ItemState.Usable)
                return;

            State = ItemState.Used;
        }

        public void SetAttributes(ScriptableItem attributes) => Attribute = attributes;
    }

    public enum ItemState { Usable, Used, Collectable }
}