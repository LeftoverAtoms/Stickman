using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Character Owner { get; set; }

    float MeleeRange;

    bool IsProjectile => Velocity.magnitude > 0f;
    Vector2 Velocity;

    void FixedUpdate()
    {
        if (IsProjectile)
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
        transform.parent = null;
        Velocity = new Vector2(10f, 2.5f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Character obj;
        if (collision.gameObject.TryGetComponent<Character>(out obj))
        {
            // Check for inventory space.
            if (obj.Weapon is null && !this.IsProjectile)
            {
                obj.EquipWeapon(this);
                return;
            }

            // Damage characters facing in the opposite direction than the motion of this object.
            float result = Vector2.Dot(obj.LookDirection, this.Velocity.normalized);
            if (result < 0f && this.IsProjectile)
            {
                Velocity = Vector2.zero;
                Owner.UnequipWeapon(this);
                Destroy(gameObject);
            }
        }
        // Anything within the world.
        else if (this.IsProjectile)
        {
            Velocity = Vector2.zero;
            Owner.UnequipWeapon(this);
            Destroy(gameObject);
        }
    }
}