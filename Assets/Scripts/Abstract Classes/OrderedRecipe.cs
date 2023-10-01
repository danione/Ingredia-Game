using System.Collections.Generic;
using UnityEngine;

public abstract class OrderedRecipe : MonoBehaviour, IRecipe
{
    protected List<IRecipeAction> actionContainer = new List<IRecipeAction>();
    public List<IRecipeAction> ActionContainer => actionContainer;

    protected RecipeStatus status = RecipeStatus.Initial;
    public RecipeStatus Status => status;

    protected float probability;
    
    protected int currentAction = 0;

    protected abstract void SetProbability();

    public float GetProbability()
    {
        // Ensure that SetProbability is called before accessing the probability.
        SetProbability();
        return probability;
    }

    // Clean the code after completed or after failed
    public virtual void Uninit()
    {
        CleanupRestOfActions();
        DropOrLog();
    }

    // To be destroyed, for testing purposes
    private void DropOrLog()
    {
        if (status == RecipeStatus.Completed)
        {
            Debug.Log("Success!");
        }
        else
        {
            Debug.Log("Fail!");
        }
    }

    // Clean up in case we have leftovers after recipe fails
    private void CleanupRestOfActions()
    {
        for (int i = currentAction; i < actionContainer.Count; i++)
        {
            actionContainer[i].DestroyRecipe();
            actionContainer[i].Triggered -= OnActionTriggered;
        }
        actionContainer.Clear();
    }

    // Add one event handler
    public virtual void ListenRecipes()
    {
        actionContainer[currentAction].Triggered += OnActionTriggered;
    }

    // Will be initialised as a child class
    public abstract void Init(PlayerInventory inventory);

    // When an action was triggered, check if the current action
    // is the action needed
    public virtual void OnActionTriggered(IRecipeAction action)
    {
        if(currentAction > actionContainer.Count - 1) { return; } 
       
        if (ReferenceEquals(action, actionContainer[currentAction]))
        {
            if(IsAllCompleted()) Uninit();
        }
        else
        {
            status = RecipeStatus.Failed;
            Uninit();
        }
    }

    // Check if recipe was completed
    public virtual bool IsAllCompleted()
    {
        for (int i = currentAction; i < actionContainer.Count; i++)
        {             
            if (i > currentAction) IterateActions();

            if (!actionContainer[i].IsCompleted()) return false;
        }
        status = RecipeStatus.Completed;
        return true;
    }

    // Destroy the recipe, and reattach event handlers. 
    private void IterateActions()
    {
        actionContainer[currentAction].DestroyRecipe();
        actionContainer[currentAction++].Triggered -= OnActionTriggered;
        ListenRecipes();
    }
}
