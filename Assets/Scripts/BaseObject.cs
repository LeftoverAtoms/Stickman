using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Rigidbody2D Body { get; private set; }
    public BoxCollider2D Collider { get; private set; }

    public float Health = 100f;

    [HideInInspector] public bool CanRecieveDamage;
    [HideInInspector] public Vector2 BBoxSize, LookDirection;

    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        Body = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();

        if (Collider != null)
        {
            BBoxSize = Collider.size;
        }
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
