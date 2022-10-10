using UnityEditor;
using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledWeapon", menuName = "Item")]
    public class ScriptableItem : ScriptableObject // Perhaps rename to InventoryObject so the type of object makes more sense.
    {
        //////////////////
        // State Trackers
        public AttributeType Type;
        public AttributeBehavior Behavior;

        ///////////////////
        // Item Attributes

        /////////////////////
        // Weapon Attributes
        public Vector2 ThrowVelocity;
    }

    public enum AttributeBehavior { Melee, Projectile }
    public enum AttributeType { Item, Weapon }
}