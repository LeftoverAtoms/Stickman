using UnityEngine;

namespace Stickman
{
    public class Item : Object
    {
        public ScriptableItem Attribute;
        public ItemState State;

        protected override void Start()
        {
            var circle = gameObject.AddComponent<CircleCollider2D>();
            circle.isTrigger = true;
            circle.radius = 0.5f;
            Collider = circle;
            Renderer = gameObject.AddComponent<SpriteRenderer>();
            Renderer.sortingOrder = 1;

            Health = 100f;

            Renderer.sprite = Attribute.Sprite;
        }

        public virtual void Use()
        {
            if (State != ItemState.Usable)
                return;

            State = ItemState.Used;
        }

        ///<summary>TODO: This function seems odd, rename or set attribute in constructor?</summary>
        public void SetAttributes(ScriptableItem attributes) => Attribute = attributes;
    }

    public enum ItemState { Usable, Used, Collectable }
}