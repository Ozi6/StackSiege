using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeForm : MonoBehaviour
{
    [Header("UI Elements")]
    public Image iconImage;
    public Image badgeImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Button getButton;

    private IUpgrade _upgrade;

    public void Setup(IUpgrade upgrade, System.Action<IUpgrade> onSelected)
    {
        _upgrade = upgrade;
        nameText.text = upgrade.Name;
        descriptionText.text = upgrade.Description;
        if (iconImage != null && upgrade.Icon != null)
        {
            iconImage.sprite = upgrade.Icon;
            iconImage.gameObject.SetActive(true);
        }
        else if (iconImage != null)
            iconImage.gameObject.SetActive(false);

        if (badgeImage != null && upgrade.BadgeIcon != null)
        {
            badgeImage.sprite = upgrade.BadgeIcon;
            badgeImage.gameObject.SetActive(true);
        }
        else if (badgeImage != null)
            badgeImage.gameObject.SetActive(false);
        getButton.onClick.RemoveAllListeners();
        getButton.onClick.AddListener(() => onSelected?.Invoke(_upgrade));
    }
}