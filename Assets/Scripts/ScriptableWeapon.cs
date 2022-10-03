using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledWeapon", menuName = "Weapon")]
    public class ScriptableWeapon : ScriptableObject
    {
        public Vector2 LaunchVelocity;
        public WeaponType Type;
    }

    public enum WeaponType
    {
        Melee, Projectile
    }
}