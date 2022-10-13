using UnityEngine;

namespace Stickman
{
    public class Enemy : Character
    {
        public static Character Target;

        public float MeleeRange = 1f;
        public float ThrowRange = 10f;

        protected override void Start()
        {
            base.Start();

            //Renderer.flipX = true; // TODO: Fix Error

            CanRecieveDamage = true;
            LookDirection = Vector2.left;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            transform.Translate(Game.Speed * Time.deltaTime * LookDirection);

            var dist = Vector2.Distance(transform.position, Target.transform.position);
            if (dist <= ThrowRange)
            {
                //ActiveItem?.Attack();
            }
        }
    }
}