public interface IUpgrade
{
    string Name { get; }
    string Description { get; }
    void Apply(PlayerController player);
}