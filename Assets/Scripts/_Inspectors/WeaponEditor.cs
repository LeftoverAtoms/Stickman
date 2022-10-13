using UnityEditor;
using UnityEngine;

namespace Stickman
{
    [CustomEditor(typeof(ScriptableWeapon))]
    public class WeaponEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            var obj = target as ScriptableWeapon;
            ObjectEditor.CreateObjectFields(obj);

            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(target); // Save Changes.
        }
    }
}