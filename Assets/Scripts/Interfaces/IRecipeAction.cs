public interface IRecipeAction
{
    public string Description { get; }

    public bool IsCompleted();
}
