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

    [CustomEditor(typeof(ScriptableItem))]
    public class ScriptableItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var obj = target as ScriptableItem;
            obj.Type = (AttributeType)EditorGUILayout.EnumPopup("Type", obj.Type);

            EditorGUI.BeginDisabledGroup(false);
            obj.Behavior = (AttributeBehavior)EditorGUILayout.EnumPopup("Behavior", obj.Behavior);

            obj.ThrowVelocity = EditorGUILayout.Vector2Field("Throw Velocity", obj.ThrowVelocity);
            EditorGUI.BeginDisabledGroup(false);
        }
    }



    public enum AttributeBehavior { Melee, Projectile }
    public enum AttributeType { Item, Weapon }
}