using UnityEngine;

public class Character : BaseObject
{
    public Weapon MeleeWeapon;
    public Weapon ProjectileWeapon;

    public float JumpHeight = 72f, MaxSlideTime = 1f;

    [HideInInspector] public bool IsGrounded;
    [HideInInspector] private float TimeSinceSlide;
    [HideInInspector] public MoveState MoveState;

    protected virtual void FixedUpdate()
    {
        if (IsGrounded)
        {
            if (MoveState == MoveState.Jumping)
            {
                Body.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
                IsGrounded = false;
            }
            if (MoveState == MoveState.Sliding)
            {
                Collider.offset = new Vector2(0f, -0.375f);
                Collider.size = new Vector2(BBoxSize.x, BBoxSize.y / 4f);

                TimeSinceSlide += Time.deltaTime;
                if (TimeSinceSlide >= MaxSlideTime)
                {
                    SwapState(MoveState.Running);
                }
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            //Debug.Log($"{contact.normal} {contact.collider.name}");

            if (contact.normal == Vector2.left)
            {
                Game.Current.GameHasEnded = true;
                gameObject.SetActive(false);
            }
            else
            {
                SwapState(MoveState.Running);
                IsGrounded = true;
            }
        }
    }

    public override bool CanDamage(BaseObject obj)
    {
        if (MoveState == MoveState.Sliding)
            return false;

        return base.CanDamage(obj);
    }

    public void EquipWeapon(Weapon wpn)
    {
        if (wpn.Owner != null)
            return;

        if (ProjectileWeapon == null && wpn.WeaponType == WeaponType.Projectile) {ProjectileWeapon = wpn;}
        else if (MeleeWeapon == null && wpn.WeaponType == WeaponType.Melee) {MeleeWeapon = wpn;}
        else return;

        wpn.transform.parent = transform;
        wpn.LookDirection = LookDirection;
        wpn.Owner = this;
    }

    public void UnequipWeapon(Weapon wpn)
    {
        if (wpn.WeaponType == WeaponType.Projectile) {ProjectileWeapon = null;}
        else if (wpn.WeaponType == WeaponType.Melee) {MeleeWeapon = null;}

        wpn.transform.parent = null;
        wpn.PreviousOwner = wpn.Owner;
        wpn.Owner = null;
    }

    protected void SwapState(MoveState state)
    {
        // Reset Animator.
        Animator.SetBool("Jumping", false);
        Animator.SetBool("Sliding", false);

        // Reset Collider.
        Collider.offset = Vector2.zero;
        Collider.size = BBoxSize;

        switch (state)
        {
            case MoveState.Running:
            MoveState = MoveState.Running;
            break;
            case MoveState.Jumping:
            MoveState = MoveState.Jumping;
            Animator.SetBool("Jumping", true);
            break;
            case MoveState.Sliding:
            MoveState = MoveState.Sliding;
            Animator.SetBool("Sliding", true);
            TimeSinceSlide = 0f;
            break;
        }
    }
}

public enum MoveState
{
    Running, Jumping, Sliding, Attacking
}