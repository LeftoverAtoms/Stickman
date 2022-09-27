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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProjectileWeapon?.TryThrow();
            SwapState(State.Attacking);
        }
        if (IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W) && MoveState != State.Jumping)
            {
                SwapState(State.Jumping);
            }
            if (Input.GetKey(KeyCode.S) && MoveState != State.Sliding)
            {
                SwapState(State.Sliding);
            }
        }
    }
}