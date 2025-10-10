using System;
using UnityEngine;

public class BoostFireRateUpgrade : IUpgrade
{
    private readonly int _attackIndex;
    private readonly string _attackName;
    private readonly Type _attackType;

    public BoostFireRateUpgrade(int attackIndex, string attackName)
    {
        _attackIndex = attackIndex;
        _attackName = attackName;
        _attackType = PlayerController.Instance.attacks[attackIndex].GetType();
    }

    public string Name => $"Faster {_attackName}";

    public string Description => $"Increase {_attackName} fire rate by 20%";

    public Sprite Icon
    {
        get
        {
            if (IconDatabase.Instance != null)
                return IconDatabase.Instance.GetAttackIcon(_attackType.Name);
            return null;
        }
    }

    public Sprite BadgeIcon
    {
        get
        {
            if (IconDatabase.Instance != null)
                return IconDatabase.Instance.GetFireRateBadge();
            return null;
        }
    }

    public void Apply(PlayerController player)
    {
        if (_attackIndex >= 0 && _attackIndex < player.attacks.Count)
        {
            player.attacks[_attackIndex].fireRate *= 1.2f;
        }
    }
}