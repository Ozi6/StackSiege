using UnityEngine;

public class LevelManager
{
    public int CurrentLevel { get; private set; }
    private readonly LevelData[] _levels;
    private readonly Transform _player;
    private readonly Transform _playerStartPosition;
    private readonly ObstacleManager _obstacleManager;

    public event System.Action<int> OnLevelLoaded;

    public LevelManager(LevelData[] levels, Transform player, Transform startPos, ObstacleManager obsManager)
    {
        this._levels = levels;
        this._player = player;
        this._playerStartPosition = startPos;
        this._obstacleManager = obsManager;
    }

    public void LoadLevel(int levelIndex)
    {
        CurrentLevel = levelIndex;
        if (CurrentLevel > _levels.Length) return;

        _player.position = _playerStartPosition.position;
        _obstacleManager.SpawnForLevel(_levels[CurrentLevel - 1]);
        OnLevelLoaded?.Invoke(CurrentLevel);
    }

    public void GoToNextLevel()
    {
        if (!IsLastLevel())
        {
            LoadLevel(CurrentLevel + 1);
        }
    }

    public bool CheckLevelCompletion()
    {
        if (IsGameFinished()) return false;
        LevelData levelData = _levels[CurrentLevel - 1];
        return _player.position.y >= levelData.levelDistance;
    }

    public float GetLevelProgress()
    {
        if (IsGameFinished()) return 1f;
        LevelData levelData = _levels[CurrentLevel - 1];
        return _player.position.y / levelData.levelDistance;
    }

    public bool IsLastLevel() => CurrentLevel >= _levels.Length;
    public bool IsGameFinished() => CurrentLevel > _levels.Length;
}