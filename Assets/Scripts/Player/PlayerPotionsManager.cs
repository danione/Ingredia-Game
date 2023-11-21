using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPotionsManager: MonoBehaviour
{
    private int sizeOfPotionsInventory = 3;
    private List<IPotion> potionsInUse = new();
    private List<int> potionsQuantity = new();
    private List<IPotion> potionsInInventory = new();
    [SerializeField] private Transform goldenNuggets;

    private void Awake()
    {
        // Fill the list with default values
        potionsQuantity = Enumerable.Repeat(0, sizeOfPotionsInventory).ToList();
        potionsInInventory = Enumerable.Repeat((IPotion)null, sizeOfPotionsInventory).ToList();
    }

    public void HandlePotions()
    {
        if (potionsInUse.Count == 0) return;
        for (int i = potionsInUse.Count - 1; i >= 0; i--)
        {
            potionsInUse[i].Tick();
            if (potionsInUse[i].Destroyed)
            {
                potionsInUse.RemoveAt(i);
            }
        }
    }

    public void AddPotion(IPotion potion)
    {
        if (potion == null) return;

        // Check if we have the potion
        for (int i = 0; i < potionsInInventory.Count; i++)
        {
            if (potionsInInventory[i] == null) continue;

            if(potion.GetType() == potionsInInventory[i].GetType())
            {
                potionsQuantity[i]++;
                PlayerEventHandler.Instance.UpdateInventoryPotions(potion.GetType().ToString(), potionsQuantity[i], i+1);
                return;
            }
        }

        // Check the next available slot if we don't have it
        for (int i = 0; i < potionsInInventory.Count; i++)
        {
            if (potionsInInventory[i] == null)
            {
                potionsInInventory[i] = potion;
                potionsQuantity[i]++;
                
                PlayerEventHandler.Instance.UpdateInventoryPotions(potion.GetType().ToString(), potionsQuantity[i], i + 1);
                return;
            }
        }

        // We have no space, just use it
        UsePotion(potion);
    }

    private bool PotionExists(int slot)
    {
        return slot >= 0 && slot < potionsInInventory.Count && potionsInInventory[slot] != null && potionsQuantity[slot] > 0;
    }

    // When inventory is full
    public void UsePotion(IPotion potion)
    {
        potion.Use();
        potionsInUse.Add(potion);
    }

    // Use the potion from inventory
    public void UsePotion(int slot)
    {
        if(PotionExists(slot))
        {
            potionsInInventory[slot].Reset(); // Reset it in case it was already used
            potionsInInventory[slot].Use();
            potionsInUse.Add(potionsInInventory[slot]); // Add it to the in use pile
            potionsQuantity[slot]--; // Decrease quantity of the said potion

            PlayerEventHandler.Instance.UpdateInventoryPotions(potionsInInventory[slot].GetType().ToString(), potionsQuantity[slot], slot + 1);

            if (potionsQuantity[slot] == 0) // Remove the potion if none are left
            {
                potionsInInventory[slot] = null;
            }
        }      
    }
}
