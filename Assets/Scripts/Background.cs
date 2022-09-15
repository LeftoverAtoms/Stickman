using UnityEngine;

public class Background : MonoBehaviour
{
    Game Game;

    Vector3 InitalBackgroundPos;
    float BackgroundRepeatWidth;

    void Start()
    {
        Game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();

        InitalBackgroundPos = transform.position;
        BackgroundRepeatWidth = GetComponent<BoxCollider2D>().size.x / 2;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * Game.Player.MoveSpeed * Time.deltaTime);

        if (transform.position.x < InitalBackgroundPos.x - BackgroundRepeatWidth)
        {
            transform.position = InitalBackgroundPos;
        }
    }
}