using System;
using UnityEngine;

namespace Stickman
{
    public abstract partial class Character : Object
    {
        public enum e_State { Idle, Running, Jumping, Sliding, Attacking }

        protected virtual void Attack()
        {

        }

        protected virtual void Run()
        {

        }

        protected virtual void Jump()
        {

        }

        protected virtual void Slide()
        {
            if(State != e_State.Sliding)
            {
                State = e_State.Sliding;
                Animator.SetBool("Sliding", true);

                Collider.offset = Vector2.down * 0.35f;
                Collider.size = new Vector2(1f, 1.25f);
            }
            else if(timeSinceSlide >= maxSlideTime)
            {
                ResetState();
            }
        }

        protected virtual void ResetState()
        {
            State = e_State.Running;
            Animator.SetBool("Jumping", false);
            Animator.SetBool("Sliding", false);
            timeSinceSlide = 0f;

            Collider.offset = Vector2.zero;
            Collider.size = new Vector2(1f, 2f);
        }
    }
}