using System.Collections.Generic;
using UnityEngine;

public class IconDatabase : Singleton<IconDatabase>
{
    [System.Serializable]
    public class AttackIconMapping
    {
        public string attackTypeName;
        public Sprite icon;
    }

    [Header("Attack Icons")]
    public AttackIconMapping[] attackIcons;

    [Header("Badge Icons (Overlays)")]
    public Sprite fireRateBadge;
    public Sprite damageBadge;

    [Header("Fallback")]
    public Sprite defaultIcon;

    protected override bool Persistent => true;

    private Dictionary<string, Sprite> _iconCache;

    private void Awake()
    {
        base.Awake();
        CacheIcons();
    }

    private void CacheIcons()
    {
        _iconCache = new Dictionary<string, Sprite>();
        if (attackIcons != null)
        {
            foreach (var mapping in attackIcons)
            {
                if (!string.IsNullOrEmpty(mapping.attackTypeName) && mapping.icon != null)
                {
                    _iconCache[mapping.attackTypeName] = mapping.icon;
                }
            }
        }
    }

    public Sprite GetAttackIcon(string attackTypeName)
    {
        string cleanName = attackTypeName.Replace("Attack", "");
        if (_iconCache.TryGetValue(attackTypeName, out Sprite icon))
            return icon;
        if (_iconCache.TryGetValue(cleanName, out icon))
            return icon;

        return defaultIcon;
    }

    public Sprite GetFireRateBadge() => fireRateBadge;
    public Sprite GetDamageBadge() => damageBadge;
}