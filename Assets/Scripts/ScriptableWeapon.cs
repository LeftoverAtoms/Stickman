using UnityEditor;
using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledWeapon", menuName = "Item")]
    public class ScriptableItem : ScriptableObject // Perhaps rename to InventoryObject so the type of object makes more sense.
    {
        public UseType UseType;
        internal ItemType ItemType;

        ///////////////////
        // Item Attributes

        /////////////////////
        // Weapon Attributes

        public Vector2 LaunchVelocity;
    }

    public enum UseType { Melee, Projectile }
    public enum ItemType { Item, Weapon }

    [CustomEditor(typeof(ScriptableItem))]
    public class ScriptableItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var item = target as ScriptableItem;

            item.ItemType = (ItemType)EditorGUILayout.EnumPopup("Type", item.ItemType);

            if (item.ItemType == ItemType.Weapon)
            {
                GUILayout.Label("This is a Weapon");
            }
            else
            {
                GUILayout.Label("This is an Item");
            }
        }
    }
}