using UnityEngine;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour
{
    public Button playButton;

    void Awake()
    {
        playButton.onClick.AddListener(() => GameManager.Instance.StartGame());
    }
}