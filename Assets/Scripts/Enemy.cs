using UnityEngine;

public class Enemy : Character
{
    protected override void Start()
    {
        base.Start();
    }

    void FixedUpdate()
    {
        if (Game.Current.GameHasEnded)
            return;

        transform.Translate(Vector3.left * Game.Current.Speed * Time.deltaTime);
    }
}
