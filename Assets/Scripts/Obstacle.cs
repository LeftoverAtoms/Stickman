using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float InitalXPos = 24f;
    float Speed = 10f;

    void Awake()
    {
        transform.position = new Vector2(InitalXPos, transform.position.y);
    }

    void Update()
    {
        if (Game.Current.GameHasEnded)
            return;

        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
}
