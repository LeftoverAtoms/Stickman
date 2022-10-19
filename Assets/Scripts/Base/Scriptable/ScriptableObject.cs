using System;
using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledObject", menuName = "ScriptableObject/Object")]
    public class ScriptableObject : UnityEngine.ScriptableObject
    {
        public virtual Type type => typeof(Object);

        // Note: Enumerators could be used for things like the name and sprite of an object.

        //public string name;
        public Vector2Int lookDirection; // Could be delegated to mouse cursor or current target.
        public Sprite sprite;
    }
}