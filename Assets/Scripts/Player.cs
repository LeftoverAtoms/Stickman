using UnityEngine;

public class Player : Character
{
    protected override void Start()
    {
        base.Start();
        CanRecieveDamage = true;
        LookDirection = Vector2.right;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Game.GiveProjectileWeapon(this);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProjectileWeapon?.TryThrow();
            SwapState(MoveState.Attacking);
        }
        if (IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W) && MoveState != MoveState.Jumping)
            {
                SwapState(MoveState.Jumping);
            }
            if (Input.GetKey(KeyCode.S) && MoveState != MoveState.Sliding)
            {
                SwapState(MoveState.Sliding);
            }
        }
    }
}