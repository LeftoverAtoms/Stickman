using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Rigidbody2D Body { get; private set; }
    public BoxCollider2D Collider { get; private set; }
    public Vector2 BBoxSize { get; private set; }

    public bool HasWeapon => Weapon != null;

    public Vector2 LookDirection;
    public Weapon Weapon;

    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        Body = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();

        BBoxSize = Collider.size;
    }

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