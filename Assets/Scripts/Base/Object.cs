using UnityEngine;

namespace Stickman
{
    public class Object : MonoBehaviour
    {
        public Animator Animator { get; private set; }
        public Rigidbody2D Body { get; private set; }
        public BoxCollider2D Collider { get; private set; }

        public float Health = 100f;

        public bool CanRecieveDamage;
        public Vector2 BBoxSize, LookDirection;

        protected virtual void Start()
        {
            Animator = GetComponent<Animator>();
            Body = GetComponent<Rigidbody2D>();
            Collider = GetComponent<BoxCollider2D>();

            if (Collider != null)
            {
                BBoxSize = Collider.size;
            }
        }

        protected virtual void FixedUpdate() { }
        protected virtual void Update() { }

        public virtual bool CanDamage(Object obj)
        {
            if (Vector2.Dot(LookDirection, obj.LookDirection) < 0f)
            {
                return true;
            }
            return false;
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

        public virtual void OnKilled()
        {
            Destroy(gameObject);
        }
    }
}