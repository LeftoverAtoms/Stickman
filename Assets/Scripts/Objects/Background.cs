using UnityEngine;

namespace Stickman
{
    public class Background : Object
    {
        private readonly float repeatWidth = 40.96f;
        private Vector2 initalPos;

        public override void Start()
        {
            initalPos = transform.position;
            lookDirection = Vector2.left;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (transform.position.x < initalPos.x - repeatWidth)
            {
                transform.position = initalPos;
            }
        }
    }
}