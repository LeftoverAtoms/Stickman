using UnityEngine;

namespace Stickman
{
    public class Weapon : Object
    {
        public ScriptableWeapon Attribute;
        public Character Owner, LastOwner;
        public Vector2 Velocity;

        private bool WasThrown;
        private WeaponState State;

        protected override void FixedUpdate()
        {
            if (WasThrown)
            {
                Velocity.y -= 9.8f * Time.deltaTime; // Gravity
                transform.Translate(Velocity * Time.deltaTime, Space.World);
                transform.Rotate(Vector3.back, 10f, Space.Self);
            }

            if (State == WeaponState.Collectible)
            {

            }
        }

        public void TryThrow()
        {
            if (WasThrown)
                return;

            Owner.UnequipWeapon(this);
            Velocity = GetInitialVelocity();
            WasThrown = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            bool IsBaseObject = collision.gameObject.TryGetComponent<Object>(out Object obj);
            bool IsCharacter = collision.gameObject.TryGetComponent<Character>(out Character character);

            if (WasThrown)
            {
                if (IsBaseObject)
                {
                    if (LastOwner.CanDamage(obj))
                    {
                        obj.TakeDamage();
                        Destroy(gameObject); //self
                    }
                }
                else
                {
                    Destroy(gameObject); //self
                }
            }
            else if (IsCharacter)
            {
                character.EquipWeapon(this);
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
        Thrown,
    }
}