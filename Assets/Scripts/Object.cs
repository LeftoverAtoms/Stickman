using UnityEngine;

namespace Stickman
{
    public class Object : MonoBehaviour
    {
        public Character Owner;
        public Animator Animator;
        public Rigidbody2D Body;
        public BoxCollider2D Collider;

        public bool CanRecieveDamage;
        public Vector2 BBoxSize, LookDirection;
        public float Health;

        protected virtual void FixedUpdate() { }
        protected virtual void Update() { }

        protected virtual void Start()
        {
            Animator = GetComponent<Animator>();
            Body = GetComponent<Rigidbody2D>();
            Collider = GetComponent<BoxCollider2D>();

            Health = 100f;

            if (Collider != null)
            {
                BBoxSize = Collider.size;
            }
        }

        public void TakeDamage(float dmg = 100f)
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

        ///<summary>Basic damage detection, override to add further functionality.</summary>
        public virtual bool CanDamage(Object obj) => Vector2.Dot(this.LookDirection, obj.LookDirection) < 0f;

        ///<summary>Called when this object has no health remaining.</summary>
        public virtual void OnKilled() => Destroy(gameObject);
    }
}