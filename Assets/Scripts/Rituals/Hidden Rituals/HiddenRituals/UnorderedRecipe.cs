using System.Collections.Generic;
using UnityEngine;

public class UnorderedRecipe : IRecipe
{
    protected List<IRecipeAction> actionContainer = new List<IRecipeAction>();

    public List<IRecipeAction> ActionContainer => actionContainer;

    protected RecipeStatus status = RecipeStatus.Initial;
    public RecipeStatus Status => status;

    protected int sizeOfContainer { get { return actionContainer.Count; } }

    protected bool validAction = false;
    private int counter = 0;

    public UnorderedRecipe(List<KeyValuePair<string, int>> ingredients)
    {   
        foreach (var ingredient in ingredients)
        {
            actionContainer.Add(new CollectItemRecipeAction(ingredient.Key, ingredient.Value));
            AddAllActions();
        }
    }

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

    public void OnActionTriggered(bool action)
    {
        counter++;
        if (validAction && counter < sizeOfContainer || status == RecipeStatus.Failed) return;
        
        if (counter == sizeOfContainer - 1 && !validAction)
        {
            status = RecipeStatus.Failed;
            Uninit();
            return;
        }
        else
        {
            validAction = false;
            counter = 0;
        }

        if (IsAllCompleted()) { Uninit(); }
    }

    public void Uninit()
    {
        foreach (IRecipeAction item in actionContainer)
        {
            item.Triggered -= OnActionTriggered;
        }

        PlayerEventHandler.Instance.EmptyCauldron();
        PlayerEventHandler.Instance.UnlockThisRitual();

        foreach (var action in actionContainer)
        {
           action.DestroyRecipe();
        }
    }
}
