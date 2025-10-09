using UnityEngine;

[System.Serializable]
public class ObstacleData
{
    public GameObject prefab;
    public Vector2 initialPosition;
    public int stackHealth;
    public Color color = Color.white;

    public virtual void ApplyTo(Obstacle obstacle)
    {
        obstacle.SetStats(stackHealth, color);
    }
}