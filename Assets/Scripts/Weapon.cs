using UnityEngine;

public class Weapon : BaseObject
{
    public Character Owner, PreviousOwner;
    public Vector2 Velocity;
    public WeaponInfo Info;

    private bool WasThrown;

    protected override void Start()
    {
       base.Start();
    }

    private void FixedUpdate()
    {
        if (WasThrown)
        {
            Velocity.y -= 9.8f * Time.deltaTime; // Gravity
            transform.Translate(Velocity * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.back, 10f, Space.Self);
        }
    }

    public Vector2 GetInitialVelocity()
    {
        var velocity = Info.LaunchVelocity;

        if (LookDirection.x < 0) // Vector2.left
        {
            velocity.x = -velocity.x;
            velocity.x -= Game.Current.Speed * 0.25f;
        }
        return velocity;
    }

    public void TryThrow()
    {
        if (WasThrown)
            return;

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
                if (PreviousOwner.CanDamage(obj))
                {
                    obj.TakeDamage();
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (IsCharacter)
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