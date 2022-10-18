using UnityEngine;

namespace Stickman
{
    public class Object : MonoBehaviour
    {
        public ScriptableObject Attribute;

        public Animator Animator;
        public Rigidbody2D Body;
        public BoxCollider2D Collider;
        public SpriteRenderer Renderer;

        public bool isInvulnerable;
        public Vector2 lookDirection;
        public float health;

        public Vector2 velocity;

        public virtual void Start()
        {
            /*
            if (Animator == null) Animator = gameObject.GetComponent<Animator>();
            if (Body == null) Body = gameObject.TryGetComponent(out Rigidbody2D RB) ? RB : gameObject.AddComponent<Rigidbody2D>();
            if (Collider == null) Collider = gameObject.TryGetComponent(out BoxCollider2D C) ? C : gameObject.AddComponent<BoxCollider2D>();
            if (Renderer == null) Renderer = gameObject.TryGetComponent(out SpriteRenderer SR) ? SR : gameObject.AddComponent<SpriteRenderer>();
            */

            health = 100f;
        }

        public virtual void FixedUpdate() { }

        public virtual void Update() { }

        public void SetAttributes(ScriptableObject attributes) => Attribute = attributes;

        public virtual bool CanDamage(Object obj) => Vector2.Dot(this.lookDirection, obj.lookDirection) < 0f;

        public virtual void TakeDamage(float dmg = 100f)
        {
            if (isInvulnerable)
            {
                health -= dmg;
                if (health <= 0f)
                {
                    health = 0f;
                    OnKilled();
                }
            }
        }

        public virtual void OnKilled() => Destroy(gameObject);
    }
}