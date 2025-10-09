using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreenController : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public Button retryButton;
    public Button nextButton;

    void Awake()
    {
        retryButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
        nextButton.onClick.AddListener(() => GameManager.Instance.NextLevel());
    }

    public void Setup(bool isWin)
    {
        titleText.text = isWin ? "LEVEL COMPLETE!" : "GAME OVER";
        retryButton.gameObject.SetActive(!isWin);
        nextButton.gameObject.SetActive(isWin);
    }
}