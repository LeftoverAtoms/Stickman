using UnityEngine;
using UnityEngine.Networking;

public class Player : Character
{
    private State MoveState { get; set; }

    private readonly float JumpHeight = 72f, MaxSlideTime = 1f;
    private float TimeSinceSlide;
    private bool IsGrounded;

    protected override void Start()
    {
        base.Start();
        CanRecieveDamage = true;
        LookDirection = Vector2.right;
    }

    private void Update()
    {
        if (Game.Current.GameHasEnded)
        {
            gameObject.SetActive(false);
            return;
        }

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
                Animator.SetBool("Sliding", true);
                TimeSinceSlide = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (IsGrounded)
        {
            if (MoveState == State.Jumping)
            {
                Body.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
                IsGrounded = false;
            }
            if (MoveState == State.Sliding)
            {
                Collider.offset = new Vector2(0f, -0.25f);
                Collider.size = new Vector2(BBoxSize.x, BBoxSize.y / 2f);

                TimeSinceSlide += Time.deltaTime;
                if (TimeSinceSlide >= MaxSlideTime)
                {
                    ResetState();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            Debug.Log($"{contact.normal} {contact.collider.name}");

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

    private void ResetState()
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