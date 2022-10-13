using System;
using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledObject", menuName = "ScriptableObject/Object")]
    public class ScriptableObject : UnityEngine.ScriptableObject
    {
        public virtual Type Type => typeof(Object);

        // Note: Enumerators could be used for things like the name and sprite of an object.

        public string Name;
        public Vector2Int LookDirection; // Could be delegated to mouse cursor or current target.
        public Sprite Sprite;
    }
}