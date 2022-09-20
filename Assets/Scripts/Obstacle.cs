using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float InitalXPos = 24f;

    void Awake()
    {
        transform.position = new Vector2(InitalXPos, transform.position.y); // Prefabs will fix this
    }

    void FixedUpdate()
    {
        if (Game.Current.GameHasEnded)
            return;

        transform.Translate(Vector3.left * Game.Current.Speed * Time.deltaTime);
    }
}
