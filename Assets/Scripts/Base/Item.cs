using UnityEngine;

namespace Stickman
{
    public class Item : Object
    {
        public Character owner;

        public bool usable;
        public bool pickupable;

        public override void Start()
        {
            base.Start();

            Collider.isTrigger = true;
            Collider.size = new Vector2(0.5f, 0.5f);

            Renderer.sprite = sprite;
            Renderer.sortingOrder = 1;
        }

        public virtual void Use()
        {
            if(usable) {usable = false;}
        }
    }

    /*
    [CustomEditor(typeof(ScriptableItem))]
    public class ItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            var obj = target as ScriptableItem;
            ObjectEditor.CreateObjectFields(obj);

            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(target); // Save Changes.
        }
    }
    */
}