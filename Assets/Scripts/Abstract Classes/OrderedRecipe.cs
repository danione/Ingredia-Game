using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class OrderedRecipe : MonoBehaviour, IRecipe
{
    protected List<IRecipeAction> actionContainer;
    public List<IRecipeAction> ActionContainer => actionContainer;

    protected RecipeStatus status = RecipeStatus.Initial;
    public RecipeStatus Status => status;

    protected int currentAction = 0;

    public virtual void Uninit()
    {
        for (int i = currentAction; i < actionContainer.Count; i++)
        {
            actionContainer[i].DestroyRecipe();
            actionContainer[i].Triggered -= OnActionTriggered;
        }
        actionContainer.Clear();

        if(status == RecipeStatus.Completed)
        {
            Debug.Log("Success!");
        }
        else
        {
            Debug.Log("Fail!");
        }
    }

    public virtual void ListenRecipes()
    {
        actionContainer[currentAction].Triggered += OnActionTriggered;
    }

    public virtual void Init(PlayerInventory inventory)
    {
        actionContainer = new List<IRecipeAction>();
    }

    public virtual void OnActionTriggered(IRecipeAction action)
    {
        if(currentAction > actionContainer.Count - 1) { return; } 
       
        if (ReferenceEquals(action, actionContainer[currentAction]))
        {
            Debug.Log("Correct Action");
            if(IsAllCompleted()) { Uninit(); }
        }
        else
        {
            Debug.Log("Incorrect Action");
            Uninit();
        }
    }

    public virtual bool IsAllCompleted()
    {
        for (int i = 0; i < actionContainer.Count; i++)
        {             
            if (i > currentAction)
            {
                IterateActions();
            }

            if (!actionContainer[i].IsCompleted())
            {
                return false;
            }
        }
        status = RecipeStatus.Completed;
        return true;
    }

    private void IterateActions()
    {
        actionContainer[currentAction].DestroyRecipe();
        actionContainer[currentAction++].Triggered -= OnActionTriggered;
        ListenRecipes();
    }

    public virtual void StartRecipe()
    {
        status = RecipeStatus.Ongoing;
    }
}
