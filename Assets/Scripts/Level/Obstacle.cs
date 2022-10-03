using UnityEditor.Tilemaps;
using UnityEngine;

namespace Stickman
{
    public class Obstacle : Object
    {
        private float InitalXPos = 24f;

        private void Awake()
        {
            transform.position = new Vector2(InitalXPos, transform.position.y); // Prefabs will fix this
            LookDirection = Vector3.left;
        }

        protected override void FixedUpdate()
        {
            transform.Translate(LookDirection * Game.Current.Speed * Time.deltaTime);
        }
    }
}