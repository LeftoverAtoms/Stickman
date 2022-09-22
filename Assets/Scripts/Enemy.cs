using UnityEngine;

public class Enemy : Character
{
    public static Character Target;

    public float MeleeRange = 1f;
    public float ThrowRange = 10f;

    protected override void Start()
    {
        base.Start();
        CanRecieveDamage = true;
        LookDirection = Vector2.left;
        Target = Game.Current.Player;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        transform.Translate(Game.Current.Speed * Time.deltaTime * LookDirection);

        if (HasWeapon)
        {
            var dist = Vector2.Distance(transform.position, Target.transform.position);
            if (Weapon.IsThrowable && dist <= ThrowRange)
            {
                Weapon?.Throw();
            }
            else if (dist <= MeleeRange)
            {
                Weapon?.Melee();
            }
        }
    }
}
