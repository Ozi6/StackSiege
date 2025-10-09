public class BoostFireRateUpgrade : IUpgrade
{
    private string attackName;
    private int attackIndex;
    private float multiplier = 1.5f;

    public BoostFireRateUpgrade(int index, string name)
    {
        attackIndex = index;
        attackName = name;
    }

    public string Name => $"Boost Fire Rate: {attackName}";
    public string Description => $"Increases fire rate by {multiplier}x.";

    public void Apply(PlayerController player)
    {
        player.attacks[attackIndex].fireRate *= multiplier;
    }
}