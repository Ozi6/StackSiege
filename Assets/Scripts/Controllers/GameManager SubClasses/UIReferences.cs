using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class UIReferences
{
    [Header("Screens")]
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public GameObject finaleScreen;
    public GameObject tint;
    public UpgradeUI upgradeUI;

    [Header("In-Game UI")]
    public Transform heartContainer;
    public GameObject heartPrefab;
    public TextMeshProUGUI levelText;
    public Slider progressBar;
    public TextMeshProUGUI destroyedObstaclesText;
}