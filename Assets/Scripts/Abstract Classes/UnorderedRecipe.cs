using System.Collections.Generic;
using UnityEngine;

public abstract class UnorderedRecipe : MonoBehaviour, IRecipe
{
    protected List<IRecipeAction> actionContainer = new List<IRecipeAction>();

    public List<IRecipeAction> ActionContainer => actionContainer;

    protected RecipeStatus status = RecipeStatus.Initial;
    public RecipeStatus Status => status;
    protected float probability;
    protected int sizeOfContainer { get { return actionContainer.Count; } }

    protected int currentNulls = 0;
    private int counter = 0;

    protected abstract void SetProbability();

    public float GetProbability()
    {
        // Ensure that SetProbability is called before accessing the probability.
        SetProbability();
        return probability;
    }

    public abstract void Init();

    protected void AddAllActions()
    {
        foreach (IRecipeAction item in actionContainer)
        {
            item.Triggered += OnActionTriggered;
        }
    } 

    public bool IsAllCompleted()
    {
        foreach (var action in actionContainer)
        {
            if (!action.IsCompleted()) return false;
        }
        status = RecipeStatus.Completed;
        return true;
    }

    public void OnActionTriggered(IRecipeAction action)
    {
        // adds number of actions
        counter++;
        
        // check if the previous action was a valid (there would 
        // always be at least one invalid) 
        if (status == RecipeStatus.Failed) return;
        if (action == null) { currentNulls++; }
        else { currentNulls = 0; }

        // If all the actions were completed and all of them were null,
        // there has been an illegal action - fail
        if(currentNulls >= sizeOfContainer && counter == sizeOfContainer) 
        {
            status = RecipeStatus.Failed;
            Uninit();
            return;
        }
        // Check if the recipe was completed
        if (IsAllCompleted()) { Uninit(); }
        // The counter would be set up at 0 when reaches full actions
        counter %= sizeOfContainer;
        currentNulls %= sizeOfContainer;
    }

    public void Uninit()
    {
        foreach (IRecipeAction item in actionContainer)
        {
            item.Triggered -= OnActionTriggered;
        }

        if (status == RecipeStatus.Completed) Debug.Log("Success!");
        else Debug.Log("Fail!");
    }
}
