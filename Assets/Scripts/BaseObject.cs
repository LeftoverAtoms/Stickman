using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Rigidbody2D Body { get; private set; }
    public BoxCollider2D Collider { get; private set; }

    public Vector2 BBoxSize { get; private set; }
    public float Health { get; set; } = 100f;
    public Vector2 LookDirection { get; set; }

    protected bool CanRecieveDamage { get; set; }

    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        Body = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();

        BBoxSize = Collider.size;
    }

    public void TakeDamage(float dmg = 100f)
    {
        if (CanRecieveDamage)
        {
            Health -= dmg;
            if (Health <= 0f)
            {
                Health = 0f;
                Destroy(gameObject);
            }
        }
    }
}
