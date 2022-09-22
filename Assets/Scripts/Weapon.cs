using UnityEngine;

public class Weapon : BaseObject
{
    private Vector2 InitialVelocity { get { var velocity = new Vector2(10f, 2.5f); velocity.x *= LookDirection.x > 0f ? 1f : -1f; return velocity; } }

    public Character Owner;
    public Vector2 Velocity;
    private float MeleeRange = 16f;

    private bool WasThrown;

    private void FixedUpdate()
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
        WasThrown = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool IsCharacter = collision.gameObject.TryGetComponent<Character>(out Character obj);

        if (WasThrown)
        {
            if (IsCharacter)
            {
                // Damage characters facing in the opposite direction than the motion of this object.
                Debug.Log(Vector2.Dot(obj.LookDirection, LookDirection));
                if (Vector2.Dot(obj.LookDirection, LookDirection) < 0f)
                {
                    obj.TakeDamage();
                }
            }
            else
            {
                Velocity = Vector2.zero;
                Destroy(gameObject);
            }
        }
        else if (IsCharacter && !obj.HasWeapon)
        {
            obj.EquipWeapon(this);
        }
    }
}