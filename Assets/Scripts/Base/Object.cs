using UnityEngine;

namespace Stickman
{
    public class Object : MonoBehaviour
    {
        public Animator animator;
        public Rigidbody2D body;
        public new BoxCollider2D collider;  // Unity depricated component properties with these names in 2018
        public new SpriteRenderer renderer; // so the new keyword essentially overrides them.

        public Sprite sprite;

        public bool isPlayer;
        public bool isInvincible;
        public float health;

        public Vector2 lookDirection;
        public Vector2 wishVelocity;
        public Vector2 velocity;

        public virtual void Start()
        {
            health = 100f;

            CreateComponents();
        }

        public virtual void FixedUpdate()
        {
            if(!isPlayer) transform.Translate(Game.RelativeSpeed * Time.fixedDeltaTime * lookDirection);
        }

        public virtual void Update()
        {
        }

        public virtual void CreateComponents()
        {
            //if (Animator == null) Animator = gameObject.TryGetComponent(out Animator A) ? A : gameObject.AddComponent<Animator>();
            //if (Body == null) Body = gameObject.TryGetComponent(out Rigidbody2D RB) ? RB : gameObject.AddComponent<Rigidbody2D>();
            if (collider == null) collider = gameObject.TryGetComponent(out BoxCollider2D C) ? C : gameObject.AddComponent<BoxCollider2D>();
            if (renderer == null) renderer = gameObject.TryGetComponent(out SpriteRenderer SR) ? SR : gameObject.AddComponent<SpriteRenderer>();
        }

        public virtual void SetProperties(ScriptableObject property)
        {
            gameObject.name = property.name;
            sprite = property.sprite;
            lookDirection = property.lookDirection;
        }

        public virtual bool CanDamage(Object obj)
        {
            if(this.CompareTag("Player") && obj.CompareTag("Enemy")) return true;
            if(this.CompareTag("Enemy") && obj.CompareTag("Player")) return true;
            return false;
        }

        public virtual void TakeDamage(float dmg = 100f)
        {
            if(isInvincible == false) // If an object is invincible, should it just have negative health?
            {
                health -= dmg;
                if (health <= 0f)
                {
                    health = 0f;
                    OnKilled();
                }
            }
        }

        public virtual void OnKilled()
        {
            Destroy(this.gameObject);
        }
    }
}

//public virtual bool CanDamage(Object obj) => Vector2.Dot(this.lookDirection, obj.lookDirection) < 0f;