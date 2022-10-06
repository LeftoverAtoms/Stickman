using UnityEngine;

namespace Stickman
{
    public abstract class Character : Object
    {
        public Inventory Inventory;
        public MoveState State;

        public float JumpHeight;
        public float MaxSlideTime;
        private float TimeSinceSlide;

        public bool IsGrounded;

        public Weapon MeleeWeapon;
        public Weapon ProjectileWeapon;

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
                if (State == MoveState.Jumping)
                {
                    Body.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
                    IsGrounded = false;
                }
                if (State == MoveState.Sliding)
                {
                    Collider.offset = new Vector2(0f, -0.375f);
                    Collider.size = new Vector2(BBoxSize.x, BBoxSize.y / 4f);

                    TimeSinceSlide += Time.deltaTime;
                    if (TimeSinceSlide >= MaxSlideTime)
                    {
                        SwapState(MoveState.Running);
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
                    SwapState(MoveState.Running);
                    IsGrounded = true;
                }
            }
        }

        public override bool CanDamage(Object obj)
        {
            //if (MoveState == MoveState.Sliding)
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

            /*
            if (obj.Attribute.Type == WeaponType.Projectile)
            {
                if (ProjectileWeapon == null)
                    ProjectileWeapon = obj;
            }
            else if (obj.Attribute.Type == WeaponType.Melee)
            {
                if (MeleeWeapon == null)
                    MeleeWeapon = obj;
            }
            else
            {
                return;
            }
            */
        }

        public void Unequip(Object obj)
        {
            if (obj is Weapon wpn)
            {
                wpn.LastOwner = this;
            }

            obj.transform.parent = null;
            obj.Owner = null;
        }

        protected void SwapState(MoveState state)
        {
            // Reset Animator.
            Animator.SetBool("Jumping", false);
            Animator.SetBool("Sliding", false);

            // Reset Collider.
            Collider.offset = Vector2.zero;
            Collider.size = BBoxSize;

            switch (state)
            {
                case MoveState.Running:
                State = MoveState.Running;
                break;
                case MoveState.Jumping:
                State = MoveState.Jumping;
                Animator.SetBool("Jumping", true);
                break;
                case MoveState.Sliding:
                State = MoveState.Sliding;
                Animator.SetBool("Sliding", true);
                TimeSinceSlide = 0f;
                break;
            }
        }
    }

    public enum MoveState
    {
        Running, Jumping, Sliding, Attacking
    }
}