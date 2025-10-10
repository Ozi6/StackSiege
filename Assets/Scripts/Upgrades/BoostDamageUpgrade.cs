using System;
using UnityEngine;

public class BoostDamageUpgrade : IUpgrade
{
    private readonly int _attackIndex;
    private readonly string _attackName;
    private readonly Type _attackType;

    public BoostDamageUpgrade(int attackIndex, string attackName)
    {
        _attackIndex = attackIndex;
        _attackName = attackName;
        _attackType = PlayerController.Instance.attacks[attackIndex].GetType();
    }

    public string Name => $"Stronger {_attackName}";

    public string Description => $"Increase {_attackName} damage by 100%";

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
                return IconDatabase.Instance.GetDamageBadge();
            return null;
        }
    }

    public void Apply(PlayerController player)
    {
        if (_attackIndex >= 0 && _attackIndex < player.attacks.Count)
        {
            player.attacks[_attackIndex].damage =
                Mathf.RoundToInt(player.attacks[_attackIndex].damage * 2f);
        }
    }
}