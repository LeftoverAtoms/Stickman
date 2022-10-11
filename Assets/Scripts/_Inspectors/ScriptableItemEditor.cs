using UnityEditor;
using UnityEngine;

namespace Stickman
{
    [CustomEditor(typeof(ScriptableItem))]
    public class ScriptableItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            var obj = target as ScriptableItem;

            obj.Type = (ScriptableItem.AttributeType)EditorGUILayout.EnumPopup("Type", obj.Type);
            obj.Behavior = (ScriptableItem.AttributeBehavior)EditorGUILayout.EnumPopup("Behavior", obj.Behavior);

            obj.Sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", obj.Sprite, typeof(Sprite), false);

            obj.ThrowVelocity = EditorGUILayout.Vector2Field("Throw Velocity", obj.ThrowVelocity);

            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(target); // Save Changes.
        }
    }
}