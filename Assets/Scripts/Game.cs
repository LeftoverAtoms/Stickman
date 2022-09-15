using UnityEngine;

public class Game : MonoBehaviour
{
    // Store script references of all major gameobjects.
    public Background Background;
    public Player Player;

    public GameObject[] ObstaclePrefabs;

    public bool GameHasEnded = false;

    void Start()
    {
        Background = GameObject.FindGameObjectWithTag("Background").GetComponent<Background>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        CreateObstacle();
    }

    void FixedUpdate()
    {
        if (GameHasEnded) return;
    }

    void CreateObstacle()
    {
        var obj = Instantiate(ObstaclePrefabs[0]);
        obj.gameObject.AddComponent<Obstacle>();
    }
}