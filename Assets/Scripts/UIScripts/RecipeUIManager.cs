using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUIManager : UIManager
{
    public static RecipeUIManager Instance;

    private IRecipe recipe;

    public override void Init()
    {
        base.Init();
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        PlayerEventHandler.Instance.CollectedRecipe += OnCollectedRecipe;
    }

    public void Activate(IRecipe _recipe)
    {
        recipe = _recipe;
        foreach (IRecipeAction action in _recipe.ActionContainer)
        {
            OnAdjustInventoryUI(action.Ingredient, action.Amount);
            action.Triggered += WrapperForActions;
        }

        templateObject.transform.parent.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(recipe != null && (recipe.Status == RecipeStatus.Completed || recipe.Status == RecipeStatus.Failed)) { Deactivate(); }
    }

    private void Deactivate()
    {
        foreach (IRecipeAction action in recipe.ActionContainer)
        {
            action.Triggered -= WrapperForActions;
        }
        RemoveAllInventoryItems(); 
        recipe = null;
    }

    private void OnCollectedRecipe(IRecipe recipe)
    {
        recipe.Init();
        Activate(recipe);
    }
}
