using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Game Configuration")]
    public int playerStartHealth = 3;
    public LevelData[] levels;

    [Header("Object References")]
    public Transform player;
    public Transform playerStartPosition;

    [Header("UI")]
    public UIReferences uiReferences;

    private PlayerManager _playerManager;
    private LevelManager _levelManager;
    private UIManager _uiManager;
    private ObstacleManager _obstacleManager;

    private bool _isPlaying = false;

    protected override bool Persistent => false;

    void Start()
    {
        if (player == null) player = PlayerController.Instance.transform;
        _obstacleManager = new ObstacleManager();
        _playerManager = new PlayerManager(player, playerStartHealth);
        _levelManager = new LevelManager(levels, player, playerStartPosition, _obstacleManager);
        _uiManager = new UIManager(uiReferences, playerStartHealth);
        _playerManager.OnHealthChanged += _uiManager.UpdateHealth;
        _playerManager.OnPlayerDied += HandlePlayerDeath;
        _levelManager.OnLevelLoaded += OnLevelLoad;
        _obstacleManager.OnObstacleDestroyedCountChanged += _uiManager.UpdateDestroyedObstacles;
        _obstacleManager.OnUpgradeConditionMet += ShowUpgradeUI;
        InitializeGame();
    }

    void Update()
    {
        if (!_isPlaying) return;
        _uiManager.UpdateProgressBar(_levelManager.GetLevelProgress());
        if (_levelManager.CheckLevelCompletion())
            HandleLevelCompletion();
    }

    private void InitializeGame()
    {
        Time.timeScale = 0;
        int savedLevel = PlayerPrefs.GetInt("LastReachedLevel", 1);
        _levelManager.LoadLevel(savedLevel);
        _uiManager.ShowStartScreen(true);
    }


    private void OnLevelLoad(int newLevel)
    {
        _uiManager.UpdateLevel(newLevel);
        _uiManager.UpdateProgressBar(0f);
        _playerManager.ResetHealth();
        PlayerPrefs.SetInt("LastReachedLevel", newLevel);
        PlayerPrefs.Save();
    }

    private void HandlePlayerDeath()
    {
        _isPlaying = false;
        Time.timeScale = 0;
        _uiManager.ShowGameOverScreen(true, isWin: false);
    }

    private void HandleLevelCompletion()
    {
        _isPlaying = false;
        Time.timeScale = 0;

        if (_levelManager.IsLastLevel())
            _uiManager.ShowFinaleScreen(true);
        else
            _uiManager.ShowGameOverScreen(true, isWin: true);
    }

    public void StartGame()
    {
        _uiManager.ShowStartScreen(false);
        _isPlaying = true;
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        _uiManager.ShowGameOverScreen(false, false);
        _levelManager.GoToNextLevel();
        _isPlaying = true;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("LastReachedLevel", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReportObstacleDestroyed(Obstacle obs)
    {
        _obstacleManager.ReportObstacleDestroyed(obs);
    }

    public void DamagePlayer(int damage)
    {
        _playerManager.TakeDamage(damage);
    }

    private void ShowUpgradeUI()
    {
        Time.timeScale = 0f;
        PlayerController playerCtrl = PlayerController.Instance;
        List<IUpgrade> possibleUpgrades = new List<IUpgrade>();
        var allTypes = AttackDatabase.Instance.allAttackTypes;
        HashSet<Type> currentTypes = new HashSet<Type>(playerCtrl.attacks.Select(a => a.GetType()));
        foreach (Type t in allTypes.Where(t => !currentTypes.Contains(t)))
            possibleUpgrades.Add(new AddNewAttackUpgrade(t));
        for (int i = 0; i < playerCtrl.attacks.Count; i++)
        {
            string n = playerCtrl.attacks[i].GetType().Name.Replace("Attack", "");
            possibleUpgrades.Add(new BoostFireRateUpgrade(i, n));
            possibleUpgrades.Add(new BoostDamageUpgrade(i, n));
        }
        if (possibleUpgrades.Count < 3)
        {
            ResumeGameAfterUpgrade();
            return;
        }
        System.Random rand = new System.Random();
        List<IUpgrade> selectedUpgrades = possibleUpgrades.OrderBy(x => rand.Next()).Take(3).ToList();
        _uiManager.ShowUpgradeScreen(selectedUpgrades);
    }

    public void ResumeGameAfterUpgrade()
    {
        Time.timeScale = 1f;
        _uiManager.HideUpgradeScreen();
        _obstacleManager.ResetDestroyedCount();
    }
}