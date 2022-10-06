using UnityEngine;

namespace Stickman
{
    public class Weapon : Object
    {
        public ScriptableWeapon Attribute;
        public Character LastOwner;
        private WeaponState State;
        public Vector2 Velocity;

        protected override void FixedUpdate()
        {
            if (State == WeaponState.Attack)
            {
                if (Attribute.Type == WeaponType.Melee)
                {

                }
                else if (Attribute.Type == WeaponType.Projectile)
                {
                    Velocity.y -= 9.8f * Time.deltaTime; // Gravity
                    transform.Translate(Velocity * Time.deltaTime, Space.World);
                    transform.Rotate(Vector3.back, 10f, Space.Self);
                }
            }
        }

        public void Throw()
        {
            if (State == WeaponState.Attack)
                return;

            (Owner as Character)?.Unequip(this);
            Velocity = GetInitialVelocity();
            State = WeaponState.Attack;
        }

        public void Attack()
        {
            if (State == WeaponState.Attack)
                return;

            if (Attribute.Type == WeaponType.Projectile)
            {
                (Owner as Character)?.Unequip(this);
                Velocity = GetInitialVelocity();
            }
            State = WeaponState.Attack;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (State == WeaponState.Attack)
            {
                if (collision.gameObject.TryGetComponent<Object>(out Object obj))
                {
                    if (LastOwner.CanDamage(obj))
                    {
                        obj.TakeDamage();
                        Destroy(this.gameObject);
                    }
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
            else if (collision.gameObject.TryGetComponent<Character>(out Character chr))
            {
                chr.Equip(this);
            }
        }

        private Vector2 GetInitialVelocity()
        {
            var velocity = Attribute.LaunchVelocity;

            if (LookDirection.x < 0) // Vector2.left
            {
                velocity.x = -velocity.x;
                velocity.x -= Game.Current.Speed * 0.25f;
            }
            return velocity;
        }

        public void SwapState(WeaponState state)
        {
            if (state == WeaponState.Attack)
            {
                State = WeaponState.Attack;
                Attack();
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
    }

    public enum WeaponState
    {
        Collectible,
        Active,
        Equiped,
        Attack,
    }
}