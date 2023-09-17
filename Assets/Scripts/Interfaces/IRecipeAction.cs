using System;

public interface IRecipeAction
{
    public string Description { get; }
    public event Action<IRecipeAction> Triggered;

    public bool IsCompleted();
    public void DestroyRecipe();
}
