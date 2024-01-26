using System;
using UnityEngine;

public class PlayerEventHandler : MonoBehaviour
{
    public static PlayerEventHandler Instance;

    // Inventory
    public event Action<IIngredient, int> CollectedIngredient;
    public event Action<int> CollectedGold;
    public event Action EmptiedCauldron;
    public event Action<IRitual> BenevolentRitualCompleted;
    public event Action<bool> LaserFired;
    public event Action<bool> TransformIntoGhost;
    public event Action<string> CollectedInvalidIngredient;
    public event Action<string> UnlockedRitual;
    public event Action<RitualScriptableObject> SetUpUIRitualInterface;
    public event Action CollidedWithRecipe;
    public event Action<string, int, int> UpdatedPotions;
    public event Action OpenedScrollsMenu;
    public event Action PerformedRitual;
    public event Action FiredWeapon;

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

    public void FiresWeapon()
    {
        FiredWeapon?.Invoke();
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

    public void CollectedWrongIngredient(string ritual)
    {
        CollectedInvalidIngredient?.Invoke(ritual);
    }

    public void UnlockARitual(string ritual)
    {
        UnlockedRitual?.Invoke(ritual);
    }

    public void SetUpHiddenRitual(RitualScriptableObject data)
    {
        SetUpUIRitualInterface?.Invoke(data);
    }

    public void CollidedWithRecipeObject()
    {
        CollidedWithRecipe?.Invoke();
    }

    public void UpdateInventoryPotions(string potionName, int amount, int slotNumber)
    {
        UpdatedPotions?.Invoke(potionName, amount, slotNumber);
    }

    public void OpenScrollMenu()
    {
        OpenedScrollsMenu?.Invoke();
    }

    public void RitualHasBeenPerformed()
    {
        PerformedRitual?.Invoke();
    }
}
