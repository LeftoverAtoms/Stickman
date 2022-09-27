using UnityEngine;

public class Character : BaseObject
{
    public Weapon MeleeWeapon;
    public Weapon ProjectileWeapon;

    public float JumpHeight = 72f, MaxSlideTime = 1f;

    [HideInInspector] public bool IsGrounded;
    [HideInInspector] private float TimeSinceSlide;
    [HideInInspector] public State MoveState;

    protected virtual void FixedUpdate()
    {
        if (IsGrounded)
        {
            if (MoveState == State.Jumping)
            {
                Body.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
                IsGrounded = false;
            }
            if (MoveState == State.Sliding)
            {
                Collider.offset = new Vector2(0f, -0.375f);
                Collider.size = new Vector2(BBoxSize.x, BBoxSize.y / 4f);

                TimeSinceSlide += Time.deltaTime;
                if (TimeSinceSlide >= MaxSlideTime)
                {
                    SwapState(State.Running);
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
                SwapState(State.Running);
                IsGrounded = true;
            }
        }
    }

    public bool IsTeamedWith(Character obj)
    {
        if (Vector2.Dot(obj.LookDirection, LookDirection) < 0f)
        {
            return true;
        }
        return false;
    }

    public void EquipWeapon(Weapon obj)
    {
        if (MeleeWeapon == null && obj.WeaponType == Weapon.Type.Melee)
        {
            MeleeWeapon = obj;
        }
        else if (ProjectileWeapon == null && obj.WeaponType == Weapon.Type.Projectile)
        {
            ProjectileWeapon = obj;
        }
        else
        {
            return;
        }

        obj.transform.parent = transform;
        obj.LookDirection = LookDirection;
        obj.Owner = this;
    }

    public void UnequipWeapon(Weapon obj)
    {
        if (obj.WeaponType == Weapon.Type.Melee)
        {
            MeleeWeapon = null;
        }
        else if (obj.WeaponType == Weapon.Type.Projectile)
        {
            ProjectileWeapon = null;
        }

        obj.transform.parent = null;
        obj.Owner = null;
    }

    protected void SwapState(State state)
    {
        // Reset Animator.
        Animator.SetBool("Jumping", false);
        Animator.SetBool("Sliding", false);

        // Reset Collider.
        Collider.offset = Vector2.zero;
        Collider.size = BBoxSize;

        switch (state)
        {
            case State.Running:
            MoveState = State.Running;
            break;
            case State.Jumping:
            MoveState = State.Jumping;
            Animator.SetBool("Jumping", true);
            break;
            case State.Sliding:
            MoveState = State.Sliding;
            Animator.SetBool("Sliding", true);
            TimeSinceSlide = 0f;
            break;
        }
    }

    public enum State
    {
        Running, Jumping, Sliding, Attacking
    }
}