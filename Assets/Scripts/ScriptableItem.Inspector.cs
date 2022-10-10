using UnityEditor;

namespace Stickman
{
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
}