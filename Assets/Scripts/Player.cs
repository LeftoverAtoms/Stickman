using UnityEngine;

public class Player : MonoBehaviour
{
    Animator Animator;
    BoxCollider2D Collider;
    Rigidbody2D Body;

    Weapon Weapon;

    public State MoveState;
    float JumpForce = 100f;

    public bool IsGrounded;

    // TODO: Find better names for these vars.
    float ElapsedTimeSliding { get { return Time.realtimeSinceStartup - TimeSinceLastSlide; } }
    float MaxSlideTime = 1f;
    float TimeSinceLastSlide;

    Vector2 BBoxSize;

    void Start()
    {
        Animator = GetComponent<Animator>();
        Body = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();

        BBoxSize = Collider.size;
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
            MoveState = State.Attacking;
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
        if (MoveState == State.Attacking)
        {
            Debug.Log("ATTACK");
            Weapon.Attack();

            ResetState();
        }
        else if (MoveState == State.Jumping && IsGrounded)
        {
            Body.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            IsGrounded = false;
        }
        else if (MoveState == State.Sliding)
        {
            Collider.offset = new Vector2(0f, -0.25f);
            Collider.size = new Vector2(BBoxSize.x, BBoxSize.y / 2);
        }

        // Stop Sliding
        if (ElapsedTimeSliding > MaxSlideTime && MoveState == State.Sliding)
        {
            ResetState();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            Debug.Log($"{contact.collider.name} {contact.normal}");

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