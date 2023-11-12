using System;
using System.Diagnostics;
using UnityEngine;
public class CollectItemRecipeAction : IRecipeAction
{
    private string _description;
    public string Description => _description;

    private string _ingredient;
    public string Ingredient => _ingredient;

    private int _amount;
    public int Amount => _amount;

    private int _startingAmount;
    public event Action<IRecipeAction> Triggered;

    public CollectItemRecipeAction(string ingredient, int amount)
    {
        _ingredient = ingredient;
        _amount = amount;
        _startingAmount = amount;
        PlayerEventHandler.Instance.CollectedIngredient += OnCollectedIngredient;
        _description = "Collect " + _amount + " " + ingredient;
    }

    private void OnCollectedIngredient(IIngredient ingredient, int amount = 0)
    {
        if(ingredient == null) _amount = _startingAmount;

        if(ingredient == null || ingredient.IngredientName != _ingredient) 
        {
            Triggered?.Invoke(null);
        } else if (ingredient.IngredientName == _ingredient && _amount != 0)
        {
            _amount--;
            Triggered?.Invoke(this);
        }
    }

    public bool IsCompleted()
    {
        if(_amount == 0) { return true; }
        return false;
    }

    public void DestroyRecipe()
    {
        PlayerEventHandler.Instance.CollectedIngredient -= OnCollectedIngredient;
    }
}

