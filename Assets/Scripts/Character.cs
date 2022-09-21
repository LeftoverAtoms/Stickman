using UnityEngine;

public class Character : BaseObject
{
    public bool HasWeapon => Weapon != null;
    public Weapon Weapon;

    public void EquipWeapon(Weapon obj)
    {
        obj.transform.parent = transform;
        obj.transform.position = Vector2.up;
        obj.LookDirection = LookDirection;
        obj.Owner = this;
        Weapon = obj;
    }

    public void UnequipWeapon(Weapon obj)
    {
        obj.transform.parent = null;
        obj.Owner = null;
        Weapon = null;
    }
}