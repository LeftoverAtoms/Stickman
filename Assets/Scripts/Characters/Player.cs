using UnityEngine;

namespace Stickman
{
    public class Player : Character
    {
        public override void Start()
        {
            base.Start();

            isInvulnerable = true;
            lookDirection = Vector2.right;
        }

        public override void Update()
        {
            if(Input.GetKeyDown(KeyCode.C)) { Game.Give("Bottle", this); }
            if(Input.GetKeyDown(KeyCode.X)) { Game.SpawnObject("Enemy", Vector2.right * 16f); }

            if(Input.GetKeyDown(KeyCode.Space)) activeItem?.Use();

            wishVelocity.x = Input.GetAxisRaw("Horizontal");

            if(isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.W)) SwapState(State.Jumping);
                if (Input.GetKey(KeyCode.S)) SwapState(State.Sliding);
            }
        }

        public override void OnKilled()
        {
            Game.IsGameOver = true;
            gameObject.SetActive(false);
        }
    }
}