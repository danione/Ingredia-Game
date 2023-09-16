public interface IRecipe<TContainer>
{
    public TContainer ActionContainer { get; }
    public abstract void Init(TContainer container);
    public abstract void Destroy();
}
