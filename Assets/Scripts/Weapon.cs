using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player Owner { get; private set; }

    bool AutoAttack; // For enemies or even testing I suppose.
    float MeleeRange; // For raycast
    float ThrowRange; // For Autoattack

    float LocalYPos = 1f; // [RENAME] Where the ray or projectile is thrown from.

    float ProjectileSpeed = 10f;

    void Start()
    {
        Owner = Game.Current.Player;
        var a = GetComponent<SpriteRenderer>();
        a.sprite = Sprites.Milk;
    }

    void FixedUpdate()
    {
        if (AutoAttack)
        {
        }
    }

    public void Melee()
    {
        Vector2 start = new Vector2(Owner.transform.position.x, Owner.transform.position.y);
        var hit = Physics2D.Raycast(start, Vector2.right, MeleeRange);

        var obj = hit.collider.gameObject;
        Debug.Log(obj.name);
    }

    public void Throw()
    {

    }

    public class Sprites
    {
        public static Sprite Milk { get { return Resources.Load<Sprite>("Sprites/Weapons/Milk.png"); } }
    }
}