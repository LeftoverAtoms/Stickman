using UnityEngine;

public class Player : MonoBehaviour
{
    Game Game;

    // Components
    BoxCollider2D Collider;
    Rigidbody2D Body;

    // Variables
    public State MoveState;
    float JumpForce = 100f;
    public float MoveSpeed = 10f;
    Vector2 BBoxSize;

    void Start()
    {
        Game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();

        Body = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();

        BBoxSize = Collider.size;
    }

    void Update()
    {
        // Inputs
        if (Input.GetKeyDown(KeyCode.Space)) MoveState = State.Attacking;
        else if (Input.GetKeyDown(KeyCode.W)) MoveState = State.Jumping;
        else if (Input.GetKeyDown(KeyCode.S)) MoveState = State.Sliding;
    }

    void FixedUpdate()
    {
        // Reset Collider
        if (MoveState != State.Sliding)
        {
            Collider.offset = Vector2.zero;
            Collider.size = BBoxSize;
        }

        // Game has ended...
        if (Game.GameHasEnded)
        {
            Debug.Log("GameOver");
            ResetState();
            return;
        }

        // Player Movement
        if (MoveState is State.Attacking)
        {
            Debug.Log("Attacking");
            ResetState();
        }
        else if (MoveState is State.Jumping)
        {
            Debug.Log("Jumping");
            Body.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            ResetState();
        }
        else if (MoveState is State.Sliding)
        {
            Debug.Log("Sliding");
            Collider.offset = new Vector2(0f, -0.25f);
            Collider.size = new Vector2(BBoxSize.x, BBoxSize.y / 2);
        }
    }

    void ResetState()
    {
        MoveState = State.Running;
    }

    public enum State
    {
        Running,
        Jumping,
        Sliding,
        Attacking
    }
}