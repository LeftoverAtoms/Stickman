using System;
using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledObject", menuName = "ScriptableObject/Object")]
    public class ScriptableObject : UnityEngine.ScriptableObject
    {
        public virtual Type Type => typeof(Object);

        public Sprite Sprite;
    }
}