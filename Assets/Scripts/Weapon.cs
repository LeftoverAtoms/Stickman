using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Properties
    public Character Owner { get; set; }
    public Vector2 InitialVelocity
    {
        get { Vector2 velocity = initial_velocity; velocity.x *= LookDirection.x > 0f ? 1f : -1f; return velocity; }
        set { initial_velocity = value; }
    }

    public Vector2 Velocity;
    public Vector2 LookDirection;
    float MeleeRange = 16f;

    Vector2 initial_velocity = new Vector2(10f, 2.5f);

    void FixedUpdate()
    {
        if (Velocity.magnitude > 0f)
        {
            Velocity.y -= 10f * Time.deltaTime; // Gravity
            transform.Translate(Velocity * Time.deltaTime);
        }
    }

    public void Melee()
    {
        var hit = Physics2D.Raycast(Owner.transform.position, Vector2.right, MeleeRange);

        var obj = hit.collider.gameObject;
        Debug.Log(obj.name);
    }

    public void Throw()
    {
        Owner.UnequipWeapon(this);
        Velocity = InitialVelocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Character collision.
        Character obj;
        if (collision.gameObject.TryGetComponent<Character>(out obj))
        {
            if (Velocity.magnitude > 0f)
            {
                // Damage characters facing in the opposite direction
                // than the motion of this object.
                float result = Vector2.Dot(obj.LookDirection, LookDirection);
                if (result < 0f)
                {
                    Velocity = Vector2.zero;
                    Debug.Log("Character Remove");
                    Destroy(gameObject);
                }
            }
            else
            {
                // Pickup this weapon if this character has inventory space.
                if (!obj.HasWeapon)
                {
                    obj.EquipWeapon(this);
                    return;
                }
            }
        }
        // Anything within the world.
        else if (Velocity.magnitude > 0f)
        {
            Velocity = Vector2.zero;
            Debug.Log("World Remove");
            Destroy(gameObject);
        }
    }
}