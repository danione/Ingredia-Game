using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRecipe : IRecipe
{
    public List<IRecipeAction> ActionContainer => throw new System.NotImplementedException();
    private float probability;
    public RecipeStatus Status => throw new System.NotImplementedException();

    public float GetProbability()
    {
        return probability = 0.7f;
    }

    public void Init(PlayerInventory inventory)
    {
        throw new System.NotImplementedException();
    }

    public bool IsAllCompleted()
    {
        throw new System.NotImplementedException();
    }

    public void OnActionTriggered(IRecipeAction action)
    {
        throw new System.NotImplementedException();
    }

    public void Uninit()
    {
        throw new System.NotImplementedException();
    }
}
