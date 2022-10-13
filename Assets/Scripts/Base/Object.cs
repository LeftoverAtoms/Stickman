using UnityEngine;

namespace Stickman
{
    public class Object : MonoBehaviour
    {
        public ScriptableObject BaseAttribute;

        public Character Owner;
        public Animator Animator;
        public Rigidbody2D Body;
        public Collider2D Collider;
        public SpriteRenderer Renderer;

        public bool CanRecieveDamage;
        public Vector2 LookDirection;
        public float Health;

        public Vector2 BBoxSize
        {
            get
            {
                if (Collider is BoxCollider2D box) return box.size;
                else return Vector2.zero;
            }
            set
            {
                if (Collider is BoxCollider2D box) BBoxSize = box.size;
            }
        }

        protected virtual void Start()
        {
            if (!gameObject.GetComponent<Animator>()) Animator = gameObject.AddComponent<Animator>();
            if (!gameObject.GetComponent<Rigidbody2D>()) Body = gameObject.AddComponent<Rigidbody2D>();
            if (!gameObject.GetComponent<Collider2D>()) Collider = gameObject.AddComponent<BoxCollider2D>();
            if (!gameObject.GetComponent<SpriteRenderer>()) Renderer = gameObject.AddComponent<SpriteRenderer>();

            Health = 100f;
        }

        protected virtual void FixedUpdate() { }

        protected virtual void Update() { }

        public void SetAttributes(ScriptableObject attributes) => BaseAttribute = attributes;

        public virtual bool CanDamage(Object obj) => Vector2.Dot(this.LookDirection, obj.LookDirection) < 0f;

        public virtual void TakeDamage(float dmg = 100f)
        {
            if (CanRecieveDamage)
            {
                Health -= dmg;
                if (Health <= 0f)
                {
                    Health = 0f;
                    OnKilled();
                }
            }
        }

        ///<summary>Called when this object has no health remaining.</summary>
        public virtual void OnKilled() => Destroy(gameObject);

        public bool HasOwner() => Owner != null;
    }
}