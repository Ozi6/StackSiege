using UnityEngine;
using System.Collections.Generic;

public class UIManager
{
    private readonly UIReferences _refs;
    private GameObject[] _heartIcons;

    public UIManager(UIReferences references, int maxPlayerHealth)
    {
        this._refs = references;
        InitializeHearts(maxPlayerHealth);

        if (_refs.progressBar != null)
        {
            _refs.progressBar.minValue = 0f;
            _refs.progressBar.maxValue = 1f;
        }
        _refs.upgradeUI.gameObject.SetActive(false);
        _refs.gameOverScreen.SetActive(false);
        _refs.finaleScreen.SetActive(false);
    }

    private void InitializeHearts(int maxHealth)
    {
        foreach (Transform child in _refs.heartContainer)
            Object.Destroy(child.gameObject);

        _heartIcons = new GameObject[maxHealth];
        for (int i = 0; i < maxHealth; i++)
            _heartIcons[i] = Object.Instantiate(_refs.heartPrefab, _refs.heartContainer);
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < _heartIcons.Length; i++)
            if (_heartIcons[i] != null)
                _heartIcons[i].SetActive(i < currentHealth);
    }

    public void UpdateLevel(int level)
    {
        if (_refs.levelText) _refs.levelText.text = "Level: " + level;
    }

    public void UpdateProgressBar(float progress)
    {
        if (_refs.progressBar) _refs.progressBar.value = 1f - Mathf.Clamp01(progress);
    }

    public void UpdateDestroyedObstacles(int count)
    {
        if (_refs.destroyedObstaclesText != null)
            _refs.destroyedObstaclesText.text = $"Destroyed: {count % 5}/5";
    }

    public void ShowStartScreen(bool show)
    {
        _refs.startScreen.SetActive(show);
        _refs.tint.SetActive(show);
    }

    public void ShowGameOverScreen(bool show, bool isWin)
    {
        _refs.gameOverScreen.SetActive(show);
        _refs.tint.SetActive(show);
        if (show)
            _refs.gameOverScreen.GetComponent<GameOverScreenController>().Setup(isWin);
    }

    public void ShowFinaleScreen(bool show)
    {
        _refs.finaleScreen.SetActive(show);
        _refs.tint.SetActive(show);
    }

    public void ShowUpgradeScreen(List<IUpgrade> upgrades)
    {
        _refs.tint.SetActive(true);
        _refs.upgradeUI.gameObject.SetActive(true);
        _refs.upgradeUI.Show(upgrades);
    }

    public void HideUpgradeScreen()
    {
        _refs.tint.SetActive(false);
        _refs.upgradeUI.gameObject.SetActive(false);
    }
}