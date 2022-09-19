using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Range = 7.5f;

    void Start()
    {
        // Start offscreen
    }

    void FixedUpdate()
    {
        if (Game.Current.GameHasEnded)
            return;

        transform.Translate(Vector3.left * Game.Current.Speed * Time.deltaTime);
    }
}
