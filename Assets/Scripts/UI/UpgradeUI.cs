using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [Header("References")]
    public Transform upgradeContainer;
    public GameObject upgradeFormPrefab;

    private List<GameObject> _spawnedForms = new List<GameObject>();
    private List<IUpgrade> _currentUpgrades;

    public void Show(List<IUpgrade> upgrades)
    {
        if (upgrades == null || upgrades.Count == 0)
            return;
        _currentUpgrades = upgrades;
        ClearUpgradeForms();
        foreach (IUpgrade upgrade in upgrades)
        {
            GameObject formObj = Instantiate(upgradeFormPrefab, upgradeContainer);
            UpgradeForm form = formObj.GetComponent<UpgradeForm>();
            if (form != null)
            {
                form.Setup(upgrade, SelectUpgrade);
                _spawnedForms.Add(formObj);
            }
        }
        gameObject.SetActive(true);
    }

    private void SelectUpgrade(IUpgrade upgrade)
    {
        upgrade.Apply(PlayerController.Instance);
        gameObject.SetActive(false);
        GameManager.Instance.ResumeGameAfterUpgrade();
    }

    private void ClearUpgradeForms()
    {
        foreach (GameObject form in _spawnedForms)
            if (form != null)
                Destroy(form);
        _spawnedForms.Clear();
    }

    private void OnDisable()
    {
        ClearUpgradeForms();
    }
}