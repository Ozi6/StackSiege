using System;

public class AddNewAttackUpgrade : IUpgrade
{
    public Type AttackType { get; }

    public AddNewAttackUpgrade(Type t)
    {
        AttackType = t;
    }

    public string Name => $"Unlock {AttackType.Name.Replace("Attack", "")}";
    public string Description => $"Adds a new {AttackType.Name.Replace("Attack", "")} attack to your arsenal.";

    public void Apply(PlayerController player)
    {
        Attack newAttack = (Attack)Activator.CreateInstance(AttackType);
        player.attacks.Add(newAttack);
    }
}