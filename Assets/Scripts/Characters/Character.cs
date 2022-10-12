using UnityEngine;

namespace Stickman
{
    public abstract class Character : Object
    {
        public Inventory Inventory;
        public Item ActiveItem;
        public _State State;

        protected float JumpHeight;
        protected float MaxSlideTime;
        private float TimeSinceSlide;

        public bool IsGrounded;

        protected override void Start()
        {
            base.Start();

            Inventory = new Inventory(this);
            JumpHeight = 72f;
            MaxSlideTime = 1f;
        }

        protected override void FixedUpdate()
        {
            if (IsGrounded)
            {
                if (State == _State.Jumping)
                {
                    Body.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
                }
                if (State == _State.Sliding)
                {
                    TimeSinceSlide += Time.fixedDeltaTime;
                    if (TimeSinceSlide >= MaxSlideTime) SwapState(_State.Running);
                }
            }
        }

        public void Equip(Item obj, bool make_active = false)
        {
            if (!Inventory.CanAdd() && obj.HasOwner())
                return;

            if (make_active)
            {
                ActiveItem = obj;
            }

            Inventory.Add(obj);
            obj.transform.parent = this.transform;
            obj.transform.position = this.transform.position;
            obj.LookDirection = this.LookDirection;
            obj.Owner = this;
        }

        public void Unequip(Item obj, bool throw_object = false)
        {
            Inventory.Remove(obj, out ActiveItem);
            obj.transform.parent = null;
            obj.Owner = null;
        }

        protected void SwapState(_State state)
        {
            if (state == State)
                return;

            if (State == _State.Sliding && state != _State.Sliding)
            {
                Animator.SetBool("Sliding", false);
                TimeSinceSlide = 0f;

                Collider.offset = Vector2.zero;
                BBoxSize = new Vector2(1f, 2f);
            }
            if (State == _State.Jumping && state != _State.Jumping)
            {
                Animator.SetBool("Jumping", false);
            }


            if (state == _State.Running)
            {
                State = _State.Running;
            }
            if (state == _State.Jumping)
            {
                State = _State.Jumping;

                Animator.SetBool("Jumping", true);
                IsGrounded = false;
            }
            if (state == _State.Sliding)
            {
                State = _State.Sliding;

                Animator.SetBool("Sliding", true);
                //TimeSinceSlide = 0f;

                Collider.offset = Vector2.down * 0.65f;
                BBoxSize = new Vector2(1f, 0.75f);
            }
            if (state == _State.Attacking)
            {

            }
        }

        public enum _State { Running, Jumping, Sliding, Attacking }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Object>(out Object obj))
            {
                if (obj.CanDamage(this)) this.TakeDamage();
                else if (obj is Item item) this.Equip(item, true);
            }

            foreach (var contact in collision.contacts)
            {
                if (contact.normal == Vector2.up)
                {
                    SwapState(_State.Running);
                    IsGrounded = true;
                }
            }
        }
    }
}