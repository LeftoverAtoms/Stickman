using System;
using UnityEditor;
using UnityEngine;

namespace Stickman
{
    public class Object : MonoBehaviour
    {
        private ScriptableObject Attribute;

        public Character Owner;
        public Animator Animator;
        public Rigidbody2D Body;
        public Collider2D Collider;
        public SpriteRenderer Renderer;

        public bool CanRecieveDamage;
        public Vector2 LookDirection;
        public float Health;

        public Vector2 BBoxSize
        {
            get
            {
                if (Collider is BoxCollider2D box) return box.size;
                else return Vector2.zero;
            }
            set
            {
                if (Collider is BoxCollider2D box) BBoxSize = box.size;
            }
        }

        protected virtual void Start()
        {
            if (!gameObject.GetComponent<Animator>()) Animator = gameObject.AddComponent<Animator>();
            if (!gameObject.GetComponent<Rigidbody2D>()) Body = gameObject.AddComponent<Rigidbody2D>();
            if (!gameObject.GetComponent<Collider2D>()) Collider = gameObject.AddComponent<BoxCollider2D>();
            if (!gameObject.GetComponent<SpriteRenderer>()) Renderer = gameObject.AddComponent<SpriteRenderer>();

            Health = 100f;
        }

        protected virtual void FixedUpdate() { }

        protected virtual void Update() { }

        public void SetAttributes(ScriptableObject attributes) => Attribute = attributes;

        public virtual bool CanDamage(Object obj) => Vector2.Dot(this.LookDirection, obj.LookDirection) < 0f;

        public virtual void TakeDamage(float dmg = 100f)
        {
            if (CanRecieveDamage)
            {
                Health -= dmg;
                if (Health <= 0f)
                {
                    Health = 0f;
                    OnKilled();
                }
            }
        }

        ///<summary>Called when this object has no health remaining.</summary>
        public virtual void OnKilled() => Destroy(gameObject);

        public bool HasOwner() => Owner != null;
    }

    [CreateAssetMenu(fileName = "UntitledObject", menuName = "ScriptableObject/Object")]
    public class ScriptableObject : UnityEngine.ScriptableObject
    {
        public virtual Type Type => typeof(Object);

        // Note: Enumerators could be used for things like the name and sprite of an object.

        public string Name;
        public Vector2Int LookDirection; // Could be delegated to mouse cursor or current target.
        public Sprite Sprite;
    }

    [CustomEditor(typeof(ScriptableObject))]
    public class ObjectEditor : Editor
    {
        // GUILayoutOption: https://answers.unity.com/questions/702499/what-is-a-guilayoutoption.html
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            var obj = target as ScriptableObject;
            CreateObjectFields(obj);

            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(target); // Save Changes.
        }

        public static void CreateObjectFields(ScriptableObject obj)
        {
            obj.Name = EditorGUILayout.TextField("Name:", obj.Name);
            obj.LookDirection = EditorGUILayout.Vector2IntField("Look Direction:", obj.LookDirection);
            obj.Sprite = EditorGUILayout.ObjectField("Sprite:", obj.Sprite, typeof(Sprite), false) as Sprite;
        }
    }
}