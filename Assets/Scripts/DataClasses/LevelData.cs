using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Stack Attack/Level Data 2D")]
public class LevelData : ScriptableObject
{
    public List<ObstacleData> obstacles;
    public float levelDistance = 50f;
}