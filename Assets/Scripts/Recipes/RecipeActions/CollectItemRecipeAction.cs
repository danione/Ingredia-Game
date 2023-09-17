using Unity.VisualScripting;
using UnityEngine;

public class CollectItemRecipeAction : IRecipeAction
{
    private string _description;
    public string Description => _description;


    private IIngredient _ingredient;
    private int _amount;
    private int _startingAmount;
    private PlayerInventory _subjectToObserve;

    public CollectItemRecipeAction(IIngredient ingredient, int amount, PlayerInventory inventory)
    {
        _ingredient = ingredient;
        _amount = amount;
        _startingAmount = amount;
        _subjectToObserve = inventory;
        _subjectToObserve.CollectedIngredient += OnCollectedIngredient;
        _description = "Collect " + _amount + " " + ingredient.Name;
    }

    ~CollectItemRecipeAction()
    {
        _subjectToObserve.CollectedIngredient -= OnCollectedIngredient;
    }

    private void OnCollectedIngredient(IIngredient ingredient)
    {
        if(ingredient == null) 
        {
            _amount = _startingAmount;
        } else if (ingredient.GetType() == _ingredient.GetType() && _amount != 0)
        {
            _amount--;
            Debug.Log("Is completed - " + IsCompleted());
        }
    }

    public bool IsCompleted()
    {
        if(_amount == 0) { return true; }
        return false;
    }
}

