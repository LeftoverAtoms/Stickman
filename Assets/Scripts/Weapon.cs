using UnityEngine;

public class Weapon : BaseObject
{
    private Vector2 InitialVelocity = new Vector2(10f, 3f);

    public Character Owner;
    public Vector2 Velocity;

    public bool WasThrown;

    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        if (WasThrown)
        {
            Velocity.y -= 9.8f * Time.deltaTime; // Gravity
            transform.Translate(Velocity * Time.deltaTime);
        }
        Debug.Log(Velocity);
    }

    public Vector2 GetInitialVelocity()
    {
        var velocity = InitialVelocity;

        if (LookDirection.x < 0)
        {
            velocity.x = -velocity.x;
            velocity.x -= Game.Current.Speed * 0.25f;
            Debug.Log(velocity.x);
        }
        return velocity;
    }

    public void Throw()
    {
        Owner.UnequipWeapon(this);
        Velocity = GetInitialVelocity();
        WasThrown = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool IsBaseObject = collision.gameObject.TryGetComponent<BaseObject>(out BaseObject obj);
        bool IsCharacter = collision.gameObject.TryGetComponent<Character>(out Character character);

        // Weapons can only inflict damage while they are thrown.
        if (WasThrown)
        {
            //Debug.Log(Vector2.Dot(obj.LookDirection, LookDirection));
            if (IsBaseObject)
            {
                // Ignore all damage.
                if (character.MoveState is Character.State.Sliding)
                    return;

                // Something was hit.
                if (Vector2.Dot(obj.LookDirection, LookDirection) < 0f)
                {
                    obj.TakeDamage();
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
            Debug.Log("Deleted");
        }
        // Try to pickup a weapon.
        if (IsCharacter && !character.HasWeapon)
        {
            character.EquipWeapon(this);
        }
    }

    /*
    public void Melee()
    {
        IsMeleeing = true;

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
    */
}