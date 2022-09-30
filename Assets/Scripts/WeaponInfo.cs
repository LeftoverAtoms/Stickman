using UnityEngine;

public class WeaponInfo : ScriptableObject
{
    internal Vector2 LaunchVelocity;
    public WeaponClass Class;
}

public enum WeaponClass
{
    Melee, Projectile
}