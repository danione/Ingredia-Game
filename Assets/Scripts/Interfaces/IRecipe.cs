using System.Collections.Generic;

public enum RecipeStatus  { Ongoing, Completed, Failed, Initial} 
public interface IRecipe
{
    public List<IRecipeAction> ActionContainer { get; }
    public RecipeStatus Status { get; }
    public abstract bool IsAllCompleted();
    public abstract void OnActionTriggered(IRecipeAction action);
    public abstract void Uninit();
   // public abstract float GetProbability();
}
