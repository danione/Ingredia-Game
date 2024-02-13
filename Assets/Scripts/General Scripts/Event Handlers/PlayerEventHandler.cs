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
    public Action<bool> LaserFired;
    public Action<bool> TransformIntoGhost;
    public event Action<string> CollectedInvalidIngredient;
    public event Action<string> UnlockedRitual;
    public event Action<RitualScriptableObject> SetUpUIRitualInterface;
    public event Action CollidedWithRecipe;
    public event Action<string, int, int> UpdatedPotions;
    public event Action OpenedScrollsMenu;
    public event Action<IRitual> PerformedRitual;
    public event Action FiredWeapon;
    public event Action HealthAdjusted;
    public event Action EscapeMenuOpened;
    public event Action ClosedAllOpenMenus;
    public Action UpgradesMenuOpen;
    public Action UpgradesMenuClose;

    private int openMenus = 0;
    private int idOfLastMenu = -1;

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

    public void CloseAllOpenMenus()
    {
        if(openMenus > 0)
        {
            ClosedAllOpenMenus?.Invoke();
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
        bool open = IsStillOpenOrOtherOpen(2);

        if (open)
        {
            OpenedScrollsMenu?.Invoke();
        }
        else
        {
            CloseAllOpenMenus();
            idOfLastMenu = 2;
            openMenus++;
            OpenedScrollsMenu?.Invoke();
        }

    }

    public void RitualHasBeenPerformed(IRitual ritual)
    {
        PerformedRitual?.Invoke(ritual);
    }

    public void AdjustHealth()
    {
        HealthAdjusted?.Invoke();
    }

    public void EscapeMenuOpen()
    {
        bool open = IsStillOpenOrOtherOpen(0);

        if (open)
        {
            EscapeMenuOpened?.Invoke();
        }
        else
        {
            CloseAllOpenMenus();
            idOfLastMenu = 0;
            openMenus++;
            EscapeMenuOpened?.Invoke();
        }
        
    }

    private bool IsStillOpenOrOtherOpen(int id)
    {
        return id == idOfLastMenu && openMenus > 0;
    }

    public void BringUpUpgradesMenu()
    {
        bool open = IsStillOpenOrOtherOpen(1);

        if (open)
        {
            UpgradesMenuOpen?.Invoke();
        }
        else
        {
            CloseAllOpenMenus();
            idOfLastMenu = 1;
            openMenus++;
            UpgradesMenuOpen?.Invoke();
        }
        
    }

    public void ClosingDownUpgradesMenu()
    {
        if(openMenus > 0) { openMenus--; }
        UpgradesMenuClose?.Invoke();
    }

    public void CloseAMenu()
    {
        if (openMenus > 0) { openMenus--; }
    }
}
