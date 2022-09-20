using UnityEngine;

public class Player : Character
{
    // 
    public State MoveState { get; private set; }

    //
    float JumpVelocity = 96f;
    bool IsGrounded;

    // TODO: Find better names for these vars.
    float TimeSinceLastSlide, MaxSlideTime = 1f;

    protected override void Start()
    {
        base.Start();

        LookDirection = new Vector2(1f, 0f);
    }

    void Update()
    {
        if (Game.Current.GameHasEnded)
            return;

        //
        // [Inputs]
        //
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetState();
            Weapon?.Throw();
        }
        if (IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W) && MoveState != State.Jumping)
            {
                ResetState();
                MoveState = State.Jumping;

                Animator.SetBool("Jumping", true);
            }
            if (Input.GetKey(KeyCode.S) && MoveState != State.Sliding)
            {
                ResetState();
                MoveState = State.Sliding;

                TimeSinceLastSlide = Time.realtimeSinceStartup;
                Animator.SetBool("Sliding", true);
            }
        }
    }

    void FixedUpdate()
    {
        if (Game.Current.GameHasEnded)
        {
            ResetState();
            return;
        }

        //
        // [Movement]
        //
        if (MoveState == State.Jumping && IsGrounded)
        {
            Body.AddForce(Vector2.up * JumpVelocity, ForceMode2D.Impulse);
            IsGrounded = false;
        }
        else if (MoveState == State.Sliding)
        {
            Collider.offset = new Vector2(0f, -0.25f);
            Collider.size = new Vector2(BBoxSize.x, BBoxSize.y / 2);
        }

        // Stop Sliding
        var elapsed = Time.realtimeSinceStartup - TimeSinceLastSlide;
        if (elapsed > MaxSlideTime && MoveState == State.Sliding)
        {
            ResetState();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            //Debug.Log($"{contact.collider.name} {contact.normal}");

            if (contact.normal == Vector2.up)
            {
                IsGrounded = true;
                ResetState();
            }
            else
            {
                Game.Current.GameHasEnded = true;
            }
        }
    }

    void ResetState()
    {
        MoveState = State.Running;

        // Reset Animator.
        Animator.SetBool("Jumping", false);
        Animator.SetBool("Sliding", false);

        // Reset Collider.
        Collider.offset = Vector2.zero;
        Collider.size = BBoxSize;
    }

    public enum State
    {
        Running,
        Jumping,
        Sliding,
        Attacking
    }
}