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
            //if (Input.GetKeyDown(KeyCode.C)) Game.Give("Bottle", this);
            //if (Input.GetKeyDown(KeyCode.X)) Game.SpawnObject("Enemy", Vector2.right * 16f);

            if (Input.GetKeyDown(KeyCode.Space)) ActiveItem?.Use();

            if (IsGrounded)
            {
                if (Input.GetKeyDown(KeyCode.W)) SwapState(e_State.Jumping);
                if (Input.GetKey(KeyCode.S)) SwapState(e_State.Sliding);
            }
        }

        public override void OnKilled()
        {
            Game.IsGameOver = true;
            gameObject.SetActive(false);
        }
    }
}