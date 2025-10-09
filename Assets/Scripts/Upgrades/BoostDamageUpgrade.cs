public class BoostDamageUpgrade : IUpgrade
{
    private string attackName;
    private int attackIndex;
    private int boost = 1;

    public BoostDamageUpgrade(int index, string name)
    {
        attackIndex = index;
        attackName = name;
    }

    public string Name => $"Boost Damage: {attackName}";
    public string Description => $"Increases damage by {boost}.";

    public void Apply(PlayerController player)
    {
        player.attacks[attackIndex].damage += boost;
    }
}