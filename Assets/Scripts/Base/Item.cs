using System;
using UnityEditor;
using UnityEngine;

namespace Stickman
{
    public class Item : Object
    {
        public ScriptableItem ItemAttribute;

        public e_ItemState State;

        protected override void Start()
        {
            Collider = gameObject.AddComponent<CircleCollider2D>();

            base.Start();

            ItemAttribute = Attribute as ScriptableItem;

            (Collider as CircleCollider2D).radius = 0.5f;
            Collider.isTrigger = true;

            Renderer.sprite = Attribute.Sprite;
            Renderer.sortingOrder = 1;
        }

        public virtual void Use()
        {
            if(State == e_ItemState.Usable)
                State = e_ItemState.Used;
        }

        public enum e_ItemState { Usable, Used, Collectable }
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