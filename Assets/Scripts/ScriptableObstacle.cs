using UnityEngine;

namespace Stickman
{
    [CreateAssetMenu(fileName = "UntitledObstacle", menuName = "Obstacle")]
    public class ScriptableObstacle : ScriptableObject
    {
        public ObstacleType[] Type;
        public Vector2[] Position;
    }

    public enum ObstacleType
    {
        Character,
        Box,
    }
}