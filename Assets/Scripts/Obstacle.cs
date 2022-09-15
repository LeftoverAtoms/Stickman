using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float InitalXPos = 24f;
    float Speed = 10f;

    void Start()
    {
        transform.position = new Vector2(InitalXPos, transform.position.y);
    }

    void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
}
