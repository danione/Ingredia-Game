using System.Collections.Generic;
using UnityEngine;

public abstract class UnorderedRecipe : MonoBehaviour, IRecipe
{
    protected List<IRecipeAction> actionContainer = new List<IRecipeAction>();

    public List<IRecipeAction> ActionContainer => actionContainer;

    protected RecipeStatus status = RecipeStatus.Initial;
    public RecipeStatus Status => status;

    // protected float probability;
    protected int sizeOfContainer { get { return actionContainer.Count; } }

    protected bool validAction = false;
    private int counter = 0;
/*
    protected abstract void SetProbability();

    public float GetProbability()
    {
        // Ensure that SetProbability is called before accessing the probability.
        SetProbability();
        return probability;
    }
*/
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

<<<<<<< HEAD:Assets/Scripts/Rituals/Hidden Rituals/HiddenRituals/UnorderedRecipe.cs
        PlayerEventHandler.Instance.EmptyCauldron();
        PlayerEventHandler.Instance.UnlockThisRitual();

        foreach (var action in actionContainer)
        {
           action.DestroyRecipe();
        }
=======
        if (status == RecipeStatus.Completed) 
        {
            PlayerEventHandler.Instance.EmptyCauldron();
        }
        else Debug.Log("Fail!");
>>>>>>> parent of fe9afb8 (recipes appear):Assets/Scripts/Deprecated Scripts/UnorderedRecipe.cs
    }
}
