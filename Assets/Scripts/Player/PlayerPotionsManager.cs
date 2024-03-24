using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPotionsManager: MonoBehaviour
{
    private int sizeOfPotionsInventory = 3;
    private List<int> potionsQuantity = new();
    private List<PotionsData> potionsInInventory = new();

    private void Awake()
    {
        // Fill the list with default values
        potionsQuantity = Enumerable.Repeat(0, sizeOfPotionsInventory).ToList();
        potionsInInventory = Enumerable.Repeat((PotionsData)null, sizeOfPotionsInventory).ToList();
    }

    public void PurgeContents()
    {
        potionsInInventory = Enumerable.Repeat((PotionsData)null, sizeOfPotionsInventory).ToList();
        potionsQuantity = Enumerable.Repeat(0, sizeOfPotionsInventory).ToList();    
    }

    public void AddPotion(PotionsData potion)
    {
        if (potion == null) return;

        // Check if we have the potion
        for (int i = 0; i < potionsInInventory.Count; i++)
        {
            if (potionsInInventory[i] == null) continue;

            if (potion.GetType() == potionsInInventory[i].GetType())
            {
                potionsQuantity[i]++;
                PlayerEventHandler.Instance.UpdateInventoryPotions(potion.potionName, potionsQuantity[i], i + 1);
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

                PlayerEventHandler.Instance.UpdateInventoryPotions(potion.potionName, potionsQuantity[i], i + 1);
                return;
            }
        }

        // We have no space, just use it
        potion.UsePotion();
    }

    private bool PotionExists(int slot)
    {
        return slot >= 0 && slot < potionsInInventory.Count && potionsInInventory[slot] != null && potionsQuantity[slot] > 0;
    }

    // Use the potion from inventory
    public void UsePotion(int slot)
    {
        if(PotionExists(slot))
        {
            potionsInInventory[slot].UsePotion();
            potionsQuantity[slot]--; // Decrease quantity of the said potion

            PlayerEventHandler.Instance.UpdateInventoryPotions(potionsInInventory[slot].potionName, potionsQuantity[slot], slot + 1);

            if (potionsQuantity[slot] == 0) // Remove the potion if none are left
            {
                potionsInInventory[slot] = null;
            }
        }      
    }
}
