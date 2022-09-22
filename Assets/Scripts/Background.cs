using UnityEngine;

public class Background : MonoBehaviour
{
    private Vector2 InitalPos;
    private float RepeatWidth = 27.25f;

    private void Start()
    {
        InitalPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.Translate(Game.Current.Speed * Time.deltaTime * Vector3.left);

        if (transform.position.x < InitalPos.x - RepeatWidth)
        {
            transform.position = InitalPos;
        }
    }
}