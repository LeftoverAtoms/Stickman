using System;
using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledItem", menuName = "ScriptableObject/Item")]
    public class ScriptableItem : ScriptableObject
    {
        public override Type type => typeof(Item);
    }
}