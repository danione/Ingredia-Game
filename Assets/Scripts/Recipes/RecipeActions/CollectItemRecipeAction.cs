using System;
using Unity.VisualScripting;
using UnityEngine;

public class CollectItemRecipeAction : IRecipeAction
{
    private string _description;
    public string Description => _description;

    private string _ingredient;
    private int _amount;
    private int _startingAmount;
    protected PlayerInventory _subjectToObserve;
    public event Action<IRecipeAction> Triggered;

    public CollectItemRecipeAction(string ingredient, int amount, PlayerInventory inventory)
    {
        _ingredient = ingredient;
        _amount = amount;
        _startingAmount = amount;
        _subjectToObserve = inventory;
        _subjectToObserve.CollectedIngredient += OnCollectedIngredient;
        _description = "Collect " + _amount + " " + ingredient;
    }

    private void OnCollectedIngredient(IIngredient ingredient, int amount = 0)
    {
        if(ingredient == null) _amount = _startingAmount;

        if(ingredient == null || ingredient.Name != _ingredient) 
        {
            Triggered?.Invoke(null);
        } else if (ingredient.Name == _ingredient && _amount != 0)
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
        _subjectToObserve.CollectedIngredient -= OnCollectedIngredient;
    }
}

