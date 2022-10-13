using System;
using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledCharacter", menuName = "ScriptableObject/Character")]
    public class ScriptableCharacter : ScriptableObject
    {
        public override Type Type => typeof(Character);

        // [Shared]
        public ScriptableItem[] Item; // Maybe switch to loadout.
        public float Speed;

        // [Player]
        public float JumpHeight;
        public float SlideTime;

        // [Enemy]
        public float TargetRange;
    }
}