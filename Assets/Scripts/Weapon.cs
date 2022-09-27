using UnityEngine;

public class Weapon : BaseObject
{
    public Character Owner;
    public Vector2 Velocity;

    public Type WeaponType;

    [HideInInspector] private Vector2 InitialVelocity;
    [HideInInspector] private bool WasThrown;

    protected override void Start()
    {
        base.Start();
        InitialVelocity = new Vector2(15f, 3f);
    }

    private void FixedUpdate()
    {
        if (WasThrown)
        {
            Velocity.y -= 9.8f * Time.deltaTime; // Gravity
            transform.Translate(Velocity * Time.deltaTime);
        }
    }

    public Vector2 GetInitialVelocity()
    {
        var velocity = InitialVelocity;

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
                if (character.MoveState is Character.State.Sliding)
                    return;

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

    public enum Type
    {
        Melee, Projectile
    }
}