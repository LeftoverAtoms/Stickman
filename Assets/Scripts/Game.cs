using UnityEngine;

public class Game : MonoBehaviour
{
    // The reference to this class and all it's variables
    // within that instance can be accessed with this variable.
    public static Game Current { get; private set; }

    // Store class references.
    public Background Background { get; private set; }
    public Player Player { get; private set; }

    // Prefabs
    public GameObject[] ObstaclePrefabs;

    public bool GameHasEnded = false;
    public float Speed = 10f;

    private void Awake()
    {
        Background = GameObject.FindGameObjectWithTag("Background").GetComponent<Background>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Current = this;

        CreateObstacle();
    }

    private void FixedUpdate()
    {
        if (GameHasEnded)
            return;
    }

    private void CreateObstacle()
    {
        var obj = Instantiate(ObstaclePrefabs[0]);
        obj.AddComponent<Obstacle>();
    }
}