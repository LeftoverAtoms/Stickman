using UnityEngine;

namespace Stickman
{
    public abstract partial class Character : Object
    {
        public Inventory inventory;
        public Item activeItem;
        public State state;

        protected float jumpHeight;
        protected float maxSlideTime;
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
                    if(timeSinceSlide >= maxSlideTime)
                        SwapState(State.Running);
                }
            }
        }

        public void Equip(Item obj, bool make_active = false)
        {
            if(obj.owner != null)
                return;

            if(!inventory.Add(obj))
                return;

            if(make_active)
                activeItem = obj;

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

        // Note: Look into bitwise operations, this would drastically
        // simplify the character states even more so.
        // Queue states with a NextState variable.
        protected void SwapState(State state)
        {
            if(this.state == state) return;

            Animator.SetBool("Jumping", false);
            Animator.SetBool("Sliding", false);
            timeSinceSlide = 0f;

            Collider.offset = Vector2.zero;
            Collider.size = new Vector2(1f, 2f);

            if(state == State.Running)
            {
                this.state = State.Running;
            }
            if(state == State.Jumping)
            {
                this.state = State.Jumping;

                Animator.SetBool("Jumping", true);
                isGrounded = false;
            }
            if(state == State.Sliding)
            {
                this.state = State.Sliding;

                Animator.SetBool("Sliding", true);
                //TimeSinceSlide = 0f;

                Collider.offset = Vector2.down * 0.35f;
                Collider.size = new Vector2(1f, 1.25f);
            }
            if(state == State.Attacking)
            {

            }
        }
    }

    /*
    [CustomEditor(typeof(ScriptableCharacter))]
    public class CharacterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            var obj = target as ScriptableCharacter;
            //ObjectEditor.CreateObjectFields(obj);

            obj.JumpHeight = EditorGUILayout.FloatField("Jump Height:", obj.JumpHeight);
            obj.SlideTime = EditorGUILayout.FloatField("Slide Time:", obj.SlideTime);

            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(target); // Save Changes.
        }
    }
    */
}