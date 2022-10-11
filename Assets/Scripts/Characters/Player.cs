using UnityEngine;

namespace Stickman
{
    public class Player : Character
    {
        protected override void Start()
        {
            base.Start();

            CanRecieveDamage = true;
            LookDirection = Vector2.right;
        }

        protected override void Update()
        {
            // Debug
            if (Input.GetKeyDown(KeyCode.C)) Game.Give("wpn_bottle", this);
            if (Input.GetKeyDown(KeyCode.X)) Game.SpawnEnemyDebug();

            if (Input.GetKeyDown(KeyCode.Space)) ActiveItem?.Use();

            if (IsGrounded)
            {
                if (Input.GetKeyDown(KeyCode.W)) SwapState(_State.Jumping);
                if (Input.GetKey(KeyCode.S)) SwapState(_State.Sliding);
            }
        }

        public override void OnKilled()
        {
            Game.IsGameOver = true;
            gameObject.SetActive(false);
        }
    }
}