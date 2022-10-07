using UnityEngine;

namespace Stickman
{
    public class Player : Character
    {
        protected override void Start()
        {
            base.Start();

            LookDirection = Vector2.right;
            CanRecieveDamage = true;
        }

        protected override void Update()
        {
            // Debug
            if (Input.GetKeyDown(KeyCode.C))
            {
                Game.GiveWeapon("Bottle", this);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Game.SpawnEnemyDebug();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ActiveItem.Use();
            }
            if (IsGrounded)
            {
                if (Input.GetKeyDown(KeyCode.W) && State != CharState.Jumping)
                {
                    SwapState(CharState.Jumping);
                }
                if (Input.GetKey(KeyCode.S) && State != CharState.Sliding)
                {
                    SwapState(CharState.Sliding);
                }
            }
        }

        public override void OnKilled()
        {
            Game.Current.GameHasEnded = true;
            gameObject.SetActive(false);
        }
    }
}