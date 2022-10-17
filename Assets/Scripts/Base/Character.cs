using System;
using UnityEditor;
using UnityEngine;

namespace Stickman
{
    public abstract class Character : Object
    {
        public ScriptableCharacter CharacterAttribute;

        public Inventory Inventory;
        public Item ActiveItem;
        public e_State State;

        protected float JumpHeight;
        protected float MaxSlideTime;
        private float TimeSinceSlide;

        public bool IsGrounded;

        protected override void Start()
        {
            base.Start();

            CharacterAttribute = Attribute as ScriptableCharacter;

            Inventory = new Inventory();
            JumpHeight = 72f;
            MaxSlideTime = 1f;

            Game.Give("Fists", this);
        }

        protected override void FixedUpdate()
        {
            if(IsGrounded)
            {
                if(State == e_State.Jumping)
                {
                    Body.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
                }
                if(State == e_State.Sliding)
                {
                    TimeSinceSlide += Time.fixedDeltaTime;
                    if(TimeSinceSlide >= MaxSlideTime)
                        SwapState(e_State.Running);
                }
            }
        }

        public void Equip(Item obj, bool make_active = false)
        {
            if(obj.HasOwner())
                return;

            if(!Inventory.Add(obj))
                return;

            if(make_active)
                ActiveItem = obj;

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
                    SwapState(e_State.Running);
                    IsGrounded = true;
                }
            }
        }

        // Note: Look into bitwise operations, this would drastically
        // simplify the character states even more so.
        protected void SwapState(e_State state)
        {
            if(state == State)
                return;

            if(State == e_State.Sliding && state != e_State.Sliding)
            {
                Animator.SetBool("Sliding", false);
                TimeSinceSlide = 0f;

                Collider.offset = Vector2.zero;
                BBoxSize = new Vector2(1f, 2f);
            }
            if(State == e_State.Jumping && state != e_State.Jumping)
            {
                Animator.SetBool("Jumping", false);
            }


            if(state == e_State.Running)
            {
                State = e_State.Running;
            }
            if(state == e_State.Jumping)
            {
                State = e_State.Jumping;

                Animator.SetBool("Jumping", true);
                IsGrounded = false;
            }
            if(state == e_State.Sliding)
            {
                State = e_State.Sliding;

                Animator.SetBool("Sliding", true);
                //TimeSinceSlide = 0f;

                Collider.offset = Vector2.down * 0.65f;
                BBoxSize = new Vector2(1f, 0.75f);
            }
            if(state == e_State.Attacking)
            {

            }
        }

        public enum e_State { Running, Jumping, Sliding, Attacking }
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