using UnityEngine;

public class Obstacle : BaseObject
{
    private float InitalXPos = 24f;

    private void Awake()
    {
        transform.position = new Vector2(InitalXPos, transform.position.y); // Prefabs will fix this
        LookDirection = Vector3.left;
    }

    private void FixedUpdate()
    {
        transform.Translate(LookDirection * Game.Current.Speed * Time.deltaTime);
    }
}
