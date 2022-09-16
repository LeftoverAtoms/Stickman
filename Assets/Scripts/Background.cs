using UnityEngine;

public class Background : MonoBehaviour
{
    Vector2 InitalPos;
    float RepeatWidth = 56.4f;

    void Start()
    {
        InitalPos = transform.position;
    }

    void FixedUpdate()
    {
        if (Game.Current.GameHasEnded)
            return;

        transform.Translate(Vector3.left * Game.Current.Player.MoveSpeed * Time.deltaTime);

        if (transform.position.x < InitalPos.x - RepeatWidth)
        {
            transform.position = InitalPos;
        }
    }
}