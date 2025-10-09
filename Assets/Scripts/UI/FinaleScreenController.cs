using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinaleScreenController : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public Button restartButton;

    void Awake()
    {
        titleText.text = "YOU COMPLETED ALL LEVELS!";
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
    }
}