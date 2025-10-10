using UnityEngine;

    public interface IUpgrade
    {
        string Name { get; }
        string Description { get; }
        Sprite Icon { get; }
        Sprite BadgeIcon { get; }
        void Apply(PlayerController player);
    }