using UnityEngine;

namespace Stickman
{
    public class Player : Character
    {
        public override void Start()
        {
            base.Start();

            lookDirection = Vector2.right;
        }

        public override void Update()
        {
            velocity.x = Input.GetAxisRaw("Horizontal") * Game.Speed;

            if(isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.W)) SwapState(State.Jumping);
                if (Input.GetKey(KeyCode.S)) SwapState(State.Sliding);
            }

            if(Input.GetKeyDown(KeyCode.C)) { Game.Give("Bottle", this); }
            if(Input.GetKeyDown(KeyCode.X)) { Game.Spawn<Enemy>("Player", Vector2.right * 16f); }

            if(Input.GetKeyDown(KeyCode.Space)) activeItem?.Use();
        }

        public override void OnKilled()
        {
            Game.IsGameOver = true;
            gameObject.SetActive(false);
        }
    }
}