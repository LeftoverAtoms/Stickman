using UnityEngine;

namespace Stickman
{
    public abstract class Character : Object
    {
        public Inventory Inventory;
        public HeldObject ActiveItem;
        public PawnState State;

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
                if (State == PawnState.Jumping)
                {
                    Body.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
                    IsGrounded = false;
                }
                if (State == PawnState.Sliding)
                {
                    Collider.offset = new Vector2(0f, -0.375f);
                    Collider.size = new Vector2(BBoxSize.x, BBoxSize.y / 4f);

                    TimeSinceSlide += Time.fixedDeltaTime;
                    if (TimeSinceSlide >= MaxSlideTime)
                    {
                        SwapState(PawnState.Running);
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
                    SwapState(PawnState.Running);
                    IsGrounded = true;
                }
            }
        }

        public override bool CanDamage(Object obj)
        {
            //if (PawnState == PawnState.Sliding)
            //    return false;

            return base.CanDamage(obj);
        }

        public void Equip(HeldObject obj, bool make_active = false)
        {
            if (!Inventory.CanAdd() && obj.HasOwner())
                return;

            if (make_active)
            {
                ActiveItem = obj;
            }

            Inventory.Add(obj);
            obj.transform.parent = this.transform;
            obj.LookDirection = this.LookDirection;
            obj.Owner = this;
        }

        public void Unequip(HeldObject obj, bool throw_object = false)
        {
            Inventory.Remove(obj);
            ActiveItem = Inventory.Switch();
            obj.transform.parent = null;
            obj.Owner = null;
        }

        protected void SwapState(PawnState state)
        {
            // Stop Sliding.
            if (state != PawnState.Sliding && State == PawnState.Sliding)
            {
                // Reset Collider.
                Collider.offset = Vector2.zero;
                Collider.size = BBoxSize;

                Animator.SetBool("Sliding", false);
            }
            // Stop Jumping.
            if (state != PawnState.Jumping && State == PawnState.Jumping)
            {
                Animator.SetBool("Jumping", false);
            }

            // Start Running.
            if (state == PawnState.Running && State != PawnState.Running)
            {
                State = PawnState.Running;
            }
            // Start Jumping.
            else if (state == PawnState.Jumping && State != PawnState.Jumping)
            {
                State = PawnState.Jumping;
                Animator.SetBool("Jumping", true);
            }
            // Start Sliding.
            else if (state == PawnState.Sliding && State != PawnState.Sliding)
            {
                State = PawnState.Sliding;
                Animator.SetBool("Sliding", true);
                TimeSinceSlide = 0f;
            }
            // Start Attacking.
            else if (state == PawnState.Attacking && State != PawnState.Attacking)
            {

            }
        }
    }

    public enum PawnState
    {
        Running, Jumping, Sliding, Attacking
    }
}