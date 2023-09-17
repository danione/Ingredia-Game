public interface IRecipe<TContainer>
{
    public TContainer ActionContainer { get; }
    public abstract void Init(TContainer container);
    public abstract bool IsAllCompleted();
    public abstract void StartRecipe();
    public abstract void Destroy();
}
