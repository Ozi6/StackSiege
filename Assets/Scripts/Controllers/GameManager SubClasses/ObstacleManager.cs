using UnityEngine;
using System.Collections.Generic;

public class ObstacleManager
{
    public List<Obstacle> ActiveObstacles { get; } = new List<Obstacle>();
    private int _destroyedObstaclesCount = 0;

    public event System.Action<int> OnObstacleDestroyedCountChanged;
    public event System.Action OnUpgradeConditionMet;

    public void SpawnForLevel(LevelData levelData)
    {
        ClearObstacles();
        foreach (var obstacleData in levelData.obstacles)
        {
            GameObject obsObj = Object.Instantiate(obstacleData.prefab, obstacleData.initialPosition, Quaternion.identity);
            Obstacle obs = obsObj.GetComponent<Obstacle>();
            obs.SetStats(obstacleData.stackHealth, obstacleData.color);
            ActiveObstacles.Add(obs);
        }
    }

    public void ReportObstacleDestroyed(Obstacle obs)
    {
        if (ActiveObstacles.Contains(obs)) ActiveObstacles.Remove(obs);

        _destroyedObstaclesCount++;
        OnObstacleDestroyedCountChanged?.Invoke(_destroyedObstaclesCount);

        if (_destroyedObstaclesCount > 0 && _destroyedObstaclesCount % 5 == 0)
        {
            OnUpgradeConditionMet?.Invoke();
        }
    }

    public void ResetDestroyedCount()
    {
        _destroyedObstaclesCount = 0;
        OnObstacleDestroyedCountChanged?.Invoke(0);
    }

    private void ClearObstacles()
    {
        foreach (var obstacle in ActiveObstacles.ToArray())
            if (obstacle != null) Object.Destroy(obstacle.gameObject);

        ActiveObstacles.Clear();
        ResetDestroyedCount();
    }
}