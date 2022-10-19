using UnityEngine;

namespace Stickman
{
    public class Weapon : Item
    {
        public override void FixedUpdate()
        {
            if (!this.usable)
            {
                if (WeaponAttribute.Behavior == ScriptableWeapon.e_Behavior.Projectile)
                {
                    velocity.y -= 9.8f * Time.fixedDeltaTime; // Gravity
                    transform.Translate(velocity * Time.fixedDeltaTime, Space.World);
                    transform.Rotate(Vector3.back, 10f, Space.Self);
                }
            }
        }

        public override void Use()
        {
            base.Use();

            if (WeaponAttribute.Behavior == ScriptableWeapon.e_Behavior.Projectile)
            {
                owner?.Unequip(this);
                velocity = GetInitialVelocity();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (this.usable)
            {
                if (collision.gameObject.TryGetComponent<Object>(out Object obj))
                {
                    if (this.CanDamage(obj))
                    {
                        obj.TakeDamage();
                        Destroy(this.gameObject);
                    }
                }
                else Destroy(this.gameObject);
            }
            else if (collision.gameObject.TryGetComponent<Character>(out Character chr))
            {
                chr.Equip(this);
            }
        }

        private Vector2 GetInitialVelocity()
        {
            var velocity = WeaponAttribute.ThrowVelocity;

            if (lookDirection.x == Vector2.left.x)
            {
                velocity.x = -velocity.x;
                velocity.x -= Game.Speed * 0.25f;
            }
            return velocity;
        }

        /*
        public void Melee()
        {
            IsMeleeing = true;

            var hits = Physics2D.RaycastAll(Owner.transform.position, LookDirection, MeleeRange);

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out Character obj))
                {
                    if (Owner.IsTeamedWith(obj))
                    {
                        Debug.Log(obj.name);
                        obj.TakeDamage();
                        break;
                    }
                }
            }
        }
        */
    }

    /*
    [CustomEditor(typeof(ScriptableWeapon))]
    public class WeaponEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            var obj = target as ScriptableWeapon;
            ObjectEditor.CreateObjectFields(obj);

            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(target); // Save Changes.
        }
    }
    */
}