public interface IUpgrade
{
    public string Name { get; }
    public string Description { get; }
    public int MaxStage { get; }
    public int CurrentStage { get; }
    public bool IsAvailable { get; }
    public int GoldCost { get; }
    public void Init();
    public void Upgrade();
}
