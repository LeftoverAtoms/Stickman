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

            Body.isKinematic = true;

            if (Collider is CircleCollider2D circle)
            {
                circle.isTrigger = true;
                circle.radius = 0.5f;
            }

            Renderer.sprite = Attribute.Sprite;
            Renderer.sortingOrder = 1;
        }

        public virtual void Use()
        {
            if (State != e_ItemState.Usable)
                return;

            State = e_ItemState.Used;
        }

        public enum e_ItemState { Usable, Used, Collectable }
    }

    [CreateAssetMenu(fileName = "UntitledItem", menuName = "ScriptableObject/Item")]
    public class ScriptableItem : ScriptableObject
    {
        public override Type Type => typeof(Item);
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