using UnityEditor;
using UnityEngine;

/*
namespace Stickman
{
    [CustomEditor(typeof(ScriptableCharacter))]
    public class CharacterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            var obj = target as ScriptableCharacter;
            ObjectEditor.CreateObjectFields(obj);

            obj.JumpHeight = EditorGUILayout.FloatField("Jump Height:", obj.JumpHeight);
            obj.SlideTime = EditorGUILayout.FloatField("Slide Time:", obj.SlideTime);

            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(target); // Save Changes.
        }
    }
}
*/