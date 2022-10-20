using UnityEngine;

namespace Stickman
{
    public class Enemy : Character
    {
        public static Character Target;

        public float MeleeRange = 1f;
        public float ThrowRange = 10f;

        public override void Start()
        {
            base.Start();

            //Renderer.flipX = true; // TODO: Fix Error

            isInvincible = true;
            lookDirection = Vector2.left;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            var dist = Vector2.Distance(transform.position, Target.transform.position);
            if (dist <= ThrowRange)
            {
                //ActiveItem?.Attack();
            }
        }
    }
}