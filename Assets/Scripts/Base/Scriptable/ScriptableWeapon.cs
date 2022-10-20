using System;
using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledWeapon", menuName = "ScriptableObject/Weapon")]
    public class ScriptableWeapon : ScriptableItem
    {
        //public override Type type => typeof(Weapon);

        // [Shared]
        public Behavior behavior;
        public float damage;

        // [Projectile]
        public Vector2 throwVelocity;

        // [Melee]


        public enum Behavior { Melee, Projectile }
    }
}