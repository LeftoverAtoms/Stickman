using UnityEngine;

namespace Stickman
{
    public abstract class Character : Object
    {
        public Inventory Inventory;
        public Item ActiveItem;
        public CharState State;

        public float JumpHeight;
        public float MaxSlideTime;
        private float TimeSinceSlide;

        public bool IsGrounded;

        public Character()
        {
            Inventory = new Inventory(this);
            JumpHeight = 72f;
            MaxSlideTime = 1f;
        }

        protected override void FixedUpdate()
        {
            if (IsGrounded)
            {
                if (State == CharState.Jumping)
                {
                    Body.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
                    IsGrounded = false;
                }
                if (State == CharState.Sliding)
                {
                    Collider.offset = new Vector2(0f, -0.375f);
                    Collider.size = new Vector2(BBoxSize.x, BBoxSize.y / 4f);

                    TimeSinceSlide += Time.deltaTime;
                    if (TimeSinceSlide >= MaxSlideTime)
                    {
                        SwapState(CharState.Running);
                    }
                }
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Object>(out Object obj))
            {
                if (obj.CanDamage(this))
                {
                    this.TakeDamage();
                }
            }

            foreach (var contact in collision.contacts)
            {
                if (contact.normal == Vector2.up)
                {
                    SwapState(CharState.Running);
                    IsGrounded = true;
                }
            }
        }

        public override bool CanDamage(Object obj)
        {
            //if (CharState == CharState.Sliding)
            //    return false;

            return base.CanDamage(obj);
        }

        public bool Equip(Object obj) // TODO: Make a class for this.
        {
            if (obj.Owner != null)
                return false;

            if (!Inventory.CanAdd())
                return false;

            Inventory.Add(obj);
            obj.transform.parent = transform;
            obj.LookDirection = LookDirection;
            obj.Owner = this;

            return true;
        }

        public void Unequip(Object obj)
        {
            obj.transform.parent = null;
            obj.Owner = null;
        }

        protected void SwapState(CharState state)
        {
            // Reset Animator.
            Animator.SetBool("Jumping", false);
            Animator.SetBool("Sliding", false);

            // Reset Collider.
            Collider.offset = Vector2.zero;
            Collider.size = BBoxSize;

            switch (state)
            {
                case CharState.Running:
                State = CharState.Running;
                break;
                case CharState.Jumping:
                State = CharState.Jumping;
                Animator.SetBool("Jumping", true);
                break;
                case CharState.Sliding:
                State = CharState.Sliding;
                Animator.SetBool("Sliding", true);
                TimeSinceSlide = 0f;
                break;
            }
        }
    }

    public enum CharState // I have no idea how this should work.
    {
        Running, Jumping, Sliding, Attacking
    }
}