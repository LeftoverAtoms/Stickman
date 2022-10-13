using UnityEditor;
using UnityEngine;

namespace Stickman
{
    [CustomEditor(typeof(ScriptableObject))]
    public class ScriptableObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            var obj = target as ScriptableObject;

            //obj.Type = (ScriptableObject.e_Type)EditorGUILayout.EnumPopup("Type", obj.Type);
            //obj.Behavior = (ScriptableWeapon.e_Behavior)EditorGUILayout.EnumPopup("Weapon Behavior", obj.Behavior);

            obj.Sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", obj.Sprite, typeof(Sprite), false);

            //obj.ThrowVelocity = EditorGUILayout.Vector2Field("Throw Velocity", obj.ThrowVelocity);

            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(target); // Save Changes.
        }
    }
}