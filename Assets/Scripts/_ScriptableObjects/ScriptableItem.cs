using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledItem", menuName = "Item")]
    public class ScriptableItem : ScriptableObject
    {
        //////////
        // Global
        public AttributeType Type;
        public AttributeBehavior Behavior;
        public Sprite Sprite;

        ///////////////////
        // Item Attributes

        /////////////////////
        // Weapon Attributes
        public Vector2 ThrowVelocity;
    }

    public enum AttributeBehavior { Melee, Projectile }
    public enum AttributeType { Item, Weapon }
}