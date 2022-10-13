using UnityEditor;
using UnityEngine;

namespace Stickman
{
    [CustomEditor(typeof(ScriptableObject))]
    public class ObjectEditor : Editor
    {
        // GUILayoutOption: https://answers.unity.com/questions/702499/what-is-a-guilayoutoption.html
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            var obj = target as ScriptableObject;
            CreateObjectFields(obj);

            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(target); // Save Changes.
        }

        public static void CreateObjectFields(ScriptableObject obj)
        {
            obj.Name = EditorGUILayout.TextField("Name:", obj.Name);
            obj.LookDirection = EditorGUILayout.Vector2IntField("Look Direction:", obj.LookDirection);
            obj.Sprite = EditorGUILayout.ObjectField("Sprite:", obj.Sprite, typeof(Sprite), false) as Sprite;
        }
    }
}