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
                Game.Give("Bottle", this);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Game.SpawnEnemyDebug();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ActiveItem?.Use();
            }
            if (IsGrounded)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    SwapState(PawnState.Jumping);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    SwapState(PawnState.Sliding);
                }
            }
        }

        public override void OnKilled()
        {
            Game.IsGameOver = true;
            gameObject.SetActive(false);
        }
    }
}