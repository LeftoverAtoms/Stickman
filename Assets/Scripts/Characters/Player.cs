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
                //ProjectileWeapon?.Throw();
                SwapState(MoveState.Attacking);
            }
            if (IsGrounded)
            {
                if (Input.GetKeyDown(KeyCode.W) && State != MoveState.Jumping)
                {
                    SwapState(MoveState.Jumping);
                }
                if (Input.GetKey(KeyCode.S) && State != MoveState.Sliding)
                {
                    SwapState(MoveState.Sliding);
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