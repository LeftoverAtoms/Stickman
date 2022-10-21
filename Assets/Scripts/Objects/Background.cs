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
        }

        public override void FixedUpdate()
        {
            //if(Game.Player != null) transform.Translate(Game.Player.velocity * Time.deltaTime * Vector3.left);

            Debug.Log(Game.Player);

            if (transform.position.x < initalPos.x - repeatWidth)
            {
                transform.position = initalPos;
            }
        }
    }
}