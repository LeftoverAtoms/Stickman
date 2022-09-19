using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player Owner { get; private set; }

    bool AutoAttack; // For enemies or even testing I suppose.
    float Range;

    float LocalYPos = 1f; // [RENAME] Where the ray or projectile is thrown from.

    float ProjectileSpeed = 10f;

    // Switch Sprite parameter with an enum or something? IDK
    public Weapon( Player owner, Sprite img )
    {
        var a = GetComponent<SpriteRenderer>();
        a.sprite = img;

        Owner = owner;
    }

    public void Melee()
    {
        Vector2 start = new Vector2(Owner.transform.position.x, Owner.transform.position.y);
        Physics2D.Raycast(start, Vector2.right, Range);
    }

    public void Throw()
    {

    }
}