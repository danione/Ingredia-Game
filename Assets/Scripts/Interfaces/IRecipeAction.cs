using System;

public interface IRecipeAction
{
    public string Description { get; }
    public event Action<bool> Triggered;
    public string Ingredient { get;}
    public int Amount { get;}
    public bool IsCompleted();
    public void DestroyRecipe();
}
