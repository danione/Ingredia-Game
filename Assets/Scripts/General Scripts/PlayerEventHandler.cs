using System;
using UnityEngine;

public class PlayerEventHandler : MonoBehaviour
{
    public static PlayerEventHandler Instance;

    // Inventory
    public event Action<IIngredient, int> CollectedIngredient;
    public event Action<int> CollectedGold;
    public event Action EmptiedCauldron;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void CollectIngredient(IIngredient ingredient, int amount)
    {
        CollectedIngredient?.Invoke(ingredient, amount);
    }

    public void CollectGold(int amount)
    {
        CollectedGold?.Invoke(amount);
    }

    public void EmptyCauldron()
    {
        EmptiedCauldron?.Invoke();
    }
}
