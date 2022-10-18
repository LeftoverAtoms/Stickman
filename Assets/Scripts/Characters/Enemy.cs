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

            isInvulnerable = true;
            lookDirection = Vector2.left;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            transform.Translate(Game.Speed * Time.fixedDeltaTime * lookDirection);

            var dist = Vector2.Distance(transform.position, Target.transform.position);
            if (dist <= ThrowRange)
            {
                //ActiveItem?.Attack();
            }
        }
    }
}