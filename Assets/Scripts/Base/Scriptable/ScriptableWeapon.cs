using System;
using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledWeapon", menuName = "ScriptableObject/Weapon")]
    public class ScriptableWeapon : ScriptableItem
    {
        public override Type Type => typeof(Weapon);

        // [Shared]
        public e_Behavior Behavior;
        public float Damage;

        // [Projectile]
        public Vector2 ThrowVelocity;

        // [Melee]


        public enum e_Behavior { Melee, Projectile }
    }
}