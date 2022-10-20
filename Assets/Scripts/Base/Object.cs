using UnityEngine;

namespace Stickman
{
    public class Object : MonoBehaviour
    {
        public Animator Animator;
        public Rigidbody2D Body;
        public BoxCollider2D Collider;
        public SpriteRenderer Renderer;

        public Sprite sprite;

        public bool isInvincible;
        public float health;

        public Vector2 lookDirection;
        public Vector2 wishVelocity;
        public Vector2 velocity;

        public virtual void Start()
        {
            CreateComponents();
            health = 100f;
        }

        public virtual void FixedUpdate()
        {
            // Not keyword not recognized.
            if(this is Player) {}
            else
            {
                transform.Translate(Game.RelativeSpeed * Time.fixedDeltaTime * lookDirection);
                Debug.Log(Game.RelativeSpeed * Time.fixedDeltaTime * lookDirection);
            }
        }

        public virtual void Update() { }

        public virtual void CreateComponents()
        {
            //if (Animator == null) Animator = gameObject.TryGetComponent(out Animator A) ? A : gameObject.AddComponent<Animator>();
            //if (Body == null) Body = gameObject.TryGetComponent(out Rigidbody2D RB) ? RB : gameObject.AddComponent<Rigidbody2D>();
            if (Collider == null) Collider = gameObject.TryGetComponent(out BoxCollider2D C) ? C : gameObject.AddComponent<BoxCollider2D>();
            if (Renderer == null) Renderer = gameObject.TryGetComponent(out SpriteRenderer SR) ? SR : gameObject.AddComponent<SpriteRenderer>();
        }

        public virtual void SetProperties(ScriptableObject property)
        {
            gameObject.name = property.name;
            sprite = property.sprite;
            lookDirection = property.lookDirection;
        }

        public virtual bool CanDamage(Object obj) => Vector2.Dot(this.lookDirection, obj.lookDirection) < 0f;

        public virtual void TakeDamage(float dmg = 100f)
        {
            if(isInvincible == false)
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