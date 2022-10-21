using UnityEngine;

namespace Stickman
{
    public abstract partial class Character : Object
    {
        public Inventory inventory;
        public Item activeItem;
        public State state;

        private float jumpHeight;
        private float maxSlideTime;
        private float timeSinceSlide;

        public bool isGrounded;

        public override void Start()
        {
            base.Start();

            inventory = new Inventory();
            jumpHeight = 72f;
            maxSlideTime = 1f;

            //Game.Give("Fists", this);
        }

        public override void FixedUpdate()
        {
            if(isGrounded)
            {
                if(state == State.Jumping)
                {
                    Body.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                }
                if(state == State.Sliding)
                {
                    timeSinceSlide += Time.fixedDeltaTime;
                    if(timeSinceSlide >= maxSlideTime) SwapState(State.Running);
                }
            }
        }

        public void Equip(Item obj, bool make_active = false)
        {
            if(obj.owner != null)
                return;

            if(!inventory.Add(obj))
                return;

            if(make_active) activeItem = obj;

            obj.transform.parent = this.transform;
            obj.transform.position = this.transform.position;
            obj.lookDirection = this.lookDirection;
            obj.owner = this;
        }

        public void Unequip(Item obj, bool throw_object = false)
        {
            inventory.Remove(obj, out activeItem);
            obj.transform.parent = null;
            obj.owner = null;
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent<Object>(out Object obj))
            {
                if(obj.CanDamage(this))
                    this.TakeDamage();
                else if(obj is Item item)
                    this.Equip(item, true);
            }

            foreach(var contact in collision.contacts)
            {
                if(contact.normal == Vector2.up)
                {
                    SwapState(State.Running);
                    isGrounded = true;
                }
            }
        }

        public override void CreateComponents()
        {
            if (Animator == null) Animator = gameObject.TryGetComponent(out Animator A) ? A : gameObject.AddComponent<Animator>();
            if (Body == null) Body = gameObject.TryGetComponent(out Rigidbody2D RB) ? RB : gameObject.AddComponent<Rigidbody2D>();
            if (Collider == null) Collider = gameObject.TryGetComponent(out BoxCollider2D C) ? C : gameObject.AddComponent<BoxCollider2D>();
            if (Renderer == null) Renderer = gameObject.TryGetComponent(out SpriteRenderer SR) ? SR : gameObject.AddComponent<SpriteRenderer>();
        }

        public override void SetProperties(ScriptableObject property)
        {
            base.SetProperties(property);
        }
    }
}