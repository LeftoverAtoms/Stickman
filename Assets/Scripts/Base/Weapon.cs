using UnityEngine;

namespace Stickman
{
    public class Weapon : Item
    {
        private ScriptableWeapon Attribute;

        public Vector2 Velocity;

        protected override void Start()
        {
            base.Start();
        }

        protected override void FixedUpdate()
        {
            if (State == ItemState.Used)
            {
                if (Attribute.Behavior == ScriptableWeapon.e_Behavior.Projectile)
                {
                    Velocity.y -= 9.8f * Time.fixedDeltaTime; // Gravity
                    transform.Translate(Velocity * Time.fixedDeltaTime, Space.World);
                    transform.Rotate(Vector3.back, 10f, Space.Self);
                }
            }
        }

        public override void Use()
        {
            base.Use();

            if (Attribute.Behavior == ScriptableWeapon.e_Behavior.Projectile)
            {
                Owner?.Unequip(this);
                Velocity = GetInitialVelocity();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (State == ItemState.Used)
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
            var velocity = Attribute.ThrowVelocity;

            if (LookDirection.x == Vector2.left.x)
            {
                velocity.x = -velocity.x;
                velocity.x -= Game.Speed * 0.25f;
            }
            return velocity;
        }

        public void SetAttributes(ScriptableWeapon attributes) => Attribute = attributes;
    }
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