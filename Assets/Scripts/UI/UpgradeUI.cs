using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Button[] upgradeButtons = new Button[3];
    public TextMeshProUGUI[] nameTexts = new TextMeshProUGUI[3];
    public TextMeshProUGUI[] descTexts = new TextMeshProUGUI[3];

    private List<IUpgrade> currentUpgrades;

    public void Show(List<IUpgrade> upgrades)
    {
        if (upgrades.Count < 3)
            return;

        currentUpgrades = upgrades;
        for (int i = 0; i < 3; i++)
        {
            nameTexts[i].text = upgrades[i].Name;
            descTexts[i].text = upgrades[i].Description;
            int index = i;
            upgradeButtons[i].onClick.RemoveAllListeners();
            upgradeButtons[i].onClick.AddListener(() => SelectUpgrade(index));
        }
        gameObject.SetActive(true);
    }

    private void SelectUpgrade(int index)
    {
        currentUpgrades[index].Apply(PlayerController.Instance);
        gameObject.SetActive(false);
        GameManager.Instance.ResumeGameAfterUpgrade();
    }
}