public enum RecipeStatus  { Ongoing, Completed, Failed, Initial} 
public interface IRecipe
{
    public RecipeStatus Status { get; }

    public abstract void Init(PlayerInventory inventory);
    public abstract bool IsAllCompleted();
    public abstract void StartRecipe();
    public abstract void Uninit();
}
