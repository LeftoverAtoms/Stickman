using UnityEngine;

public class Throwable : MonoBehaviour
{
    float ProjectileSpeed = 15f;

    void Start()
    {
        transform.position = new Vector2(7.5f, 1f);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.left * ProjectileSpeed * Time.deltaTime );
        transform.Rotate(Vector3.left, 4f);
    }
}
