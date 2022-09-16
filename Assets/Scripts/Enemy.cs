using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Range = 7.5f;
    public bool HasThrowable = true;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Game.Current.GameHasEnded)
            return;

        transform.Translate(Vector3.left * Game.Current.Speed * Time.deltaTime);
    }
}
