using UnityEngine;

namespace Stickman
{
    public class Obstacle : Object
    {
        private readonly float InitalXPos = 24f;

        private void Awake()
        {
            transform.position = new Vector2(InitalXPos, transform.position.y); // Prefabs will fix this
            lookDirection = Vector2.left;
        }

        public override void FixedUpdate()
        {
            transform.Translate(Game.Speed * Time.deltaTime * lookDirection);
        }
    }
}