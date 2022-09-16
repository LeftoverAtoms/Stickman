using UnityEngine;

public class Player : MonoBehaviour
{
    Animator Animator;
    BoxCollider2D Collider;
    Rigidbody2D Body;

    public float MoveSpeed = 10f;
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
        Vector2 rayStart = new Vector2(transform.position.x, transform.position.y - 1.01f);
        if (Physics2D.Raycast(rayStart, Vector2.down, 0.1f))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveState = State.Attacking;
        }
        else if (Input.GetKeyDown(KeyCode.W) && IsGrounded)
        {
            MoveState = State.Jumping;
        }
        else if (Input.GetKeyDown(KeyCode.S) && IsGrounded && MoveState != State.Sliding)
        {
            MoveState = State.Sliding;

            TimeSinceLastSlide = Time.realtimeSinceStartup;
            Animator.SetBool("Sliding", true);
        }
    }

    void FixedUpdate()
    {
        // Game has ended...
        if (Game.Current.GameHasEnded)
        {
            ResetState();
            return;
        }

        // Player Movement
        if (MoveState == State.Attacking)
        {
            Debug.Log("Attacking");

            ResetState();
        }
        else if (MoveState == State.Jumping)
        {
            Body.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            ResetState();
        }
        else if (MoveState == State.Sliding)
        {
            Collider.offset = new Vector2(0f, -0.25f);
            Collider.size = new Vector2(BBoxSize.x, BBoxSize.y / 2);
        }

        // Player is no long sliding.
        if (MoveState != State.Sliding || ElapsedTimeSliding > MaxSlideTime)
        {
            ResetState();
        }
    }

    void ResetState()
    {
        MoveState = State.Running;

        // Reset Animator.
        Animator.SetBool("Sliding", false);

        // Reset Collider;
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