using UnityEngine;

public class Obstacle : BaseObject
{
    float InitalXPos = 24f;

    void Awake()
    {
        transform.position = new Vector2(InitalXPos, transform.position.y); // Prefabs will fix this
        LookDirection = Vector3.left;
    }

    void FixedUpdate()
    {
        if (Game.Current.GameHasEnded)
            return;

        transform.Translate(LookDirection * Game.Current.Speed * Time.deltaTime);
    }
}
