using UnityEngine;

namespace Stickman
{
    public abstract class Character : Object
    {
        public Inventory Inventory;
        public MoveState MoveState;

        public float JumpHeight;
        public float MaxSlideTime;
        private float TimeSinceSlide;

        public bool IsGrounded;

        public Weapon MeleeWeapon;
        public Weapon ProjectileWeapon;

        public Character()
        {
            Inventory = new Inventory();
            JumpHeight = 72f;
            MaxSlideTime = 1f;
        }

        protected override void FixedUpdate()
        {
            if (IsGrounded)
            {
                if (MoveState == MoveState.Jumping)
                {
                    Body.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
                    IsGrounded = false;
                }
                if (MoveState == MoveState.Sliding)
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

        public void EquipWeapon(Weapon wpn)
        {
            if (wpn.Owner != null)
                return;

            if (wpn.Attribute.Type == WeaponType.Projectile)
            {
                if (ProjectileWeapon == null)
                    ProjectileWeapon = wpn;
            }
            else if (wpn.Attribute.Type == WeaponType.Melee)
            {
                if (MeleeWeapon == null)
                    MeleeWeapon = wpn;
            }
            else
            {
                return;
            }

            wpn.transform.parent = transform;
            wpn.LookDirection = LookDirection;
            wpn.Owner = this;
        }

        public void UnequipWeapon(Weapon wpn)
        {
            if (wpn.Attribute.Type == WeaponType.Projectile)
            {
                ProjectileWeapon = null;
            }
            else if (wpn.Attribute.Type == WeaponType.Melee)
            {
                MeleeWeapon = null;
            }

            wpn.transform.parent = null;
            wpn.LastOwner = wpn.Owner;
            wpn.Owner = null;
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
                MoveState = MoveState.Running;
                break;
                case MoveState.Jumping:
                MoveState = MoveState.Jumping;
                Animator.SetBool("Jumping", true);
                break;
                case MoveState.Sliding:
                MoveState = MoveState.Sliding;
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