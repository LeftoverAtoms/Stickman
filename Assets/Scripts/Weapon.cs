using UnityEngine;

public class Weapon : BaseObject
{
    private Vector2 InitialVelocity { get { var velocity = new Vector2(10f, 2.5f); velocity.x *= LookDirection.x > 0f ? 1f : -1f; return velocity; } }
    public bool IsThrowable { get; private set; }

    public Character Owner;
    public Vector2 Velocity;
    private float MeleeRange = 2f;

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
        var hits = Physics2D.RaycastAll(Owner.transform.position, LookDirection, MeleeRange);
        
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Character obj))
            {
                if (Owner.IsTeamedWith(obj))
                {
                    Debug.Log(obj.name);
                    obj.TakeDamage();
                    break;
                }
            }
        }
    }

    public void Throw()
    {
        Owner.UnequipWeapon(this);
        Velocity = InitialVelocity;
        WasThrown = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool IsCharacter = collision.gameObject.TryGetComponent(out Character obj);

        if (WasThrown)
        {
            if (IsCharacter)
            {
                // Damage characters facing in the opposite direction than the motion of this object.
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
            obj.Weapon.IsThrowable = true;
        }
    }
}