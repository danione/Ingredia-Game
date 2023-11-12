using System;
using UnityEngine;

public class PlayerEventHandler : MonoBehaviour
{
    public static PlayerEventHandler Instance;

    // Inventory
    public event Action<IIngredient, int> CollectedIngredient;
    public event Action<int> CollectedGold;
    public event Action EmptiedCauldron;
    public event Action<IRecipe> CollectedRecipe;
    public event Action<IRitual> BenevolentRitualCompleted;
    public event Action<bool> LaserFired;
    public event Action<bool> TransformIntoGhost;
    public event Action UnlockRitual;
    public event Action FailRitual;

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

    public void CollectRecipe(IRecipe recipe)
    {
        CollectedRecipe?.Invoke(recipe);
    }

    public void EmptyCauldron()
    {
        EmptiedCauldron?.Invoke();
    }

    public void CompleteBenevolentRitual(IRitual ritual)
    {
        BenevolentRitualCompleted?.Invoke(ritual);
    }

    public void FireLaser(bool isPlayerPressingFiring)
    {
        LaserFired?.Invoke(isPlayerPressingFiring);
    }

    public void GhostTransform(bool isTransformingIntoAGhost)
    {
        TransformIntoGhost?.Invoke(isTransformingIntoAGhost);
    }

    public void UnlockThisRitual()
    {
        UnlockRitual?.Invoke();
    }

    public void FailThisRitual()
    {
        FailRitual?.Invoke();
    }
}
