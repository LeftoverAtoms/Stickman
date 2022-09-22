using UnityEngine;

public class Enemy : Character
{
    protected override void Start()
    {
        base.Start();
        CanRecieveDamage = true;
        LookDirection = Vector2.left;
    }

    private void FixedUpdate()
    {
        transform.Translate(Game.Current.Speed * Time.deltaTime * LookDirection);
    }
}
