using System;
using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledWeapon", menuName = "ScriptableObject/Weapon")]
    public class ScriptableWeapon : ScriptableItem
    {
        public override Type Type => typeof(Weapon);

        public e_Behavior Behavior;

        public Vector2 ThrowVelocity;

        public enum e_Behavior { Melee, Projectile }
    }
}