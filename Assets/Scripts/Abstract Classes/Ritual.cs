using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ritual : IRitual
{
    private bool isAvailable = false;
    public bool IsAvailable => isAvailable;

    private Dictionary<string, int> currentRitualValues;
    private readonly Dictionary<string, int> defaultRitualValues;


    public Ritual(Dictionary<string, int> currentRitualValues)
    {
        this.currentRitualValues.AddRange(currentRitualValues);
        defaultRitualValues.AddRange(currentRitualValues);
        PlayerEventHandler.Instance.EmptiedCauldron += OnCauldronEmptied;
        PlayerEventHandler.Instance.CollectedIngredient += OnIngredientCollected;
    }

    public bool AvailableRitual()
    {
        return isAvailable;
    }

    public void OnCauldronEmptied()
    {
        currentRitualValues.AddRange(defaultRitualValues);
    }

    public void OnIngredientCollected(IIngredient ingredient, int amount)
    {
        if (!currentRitualValues.ContainsKey(ingredient.Name))
        {
            isAvailable = false;
            currentRitualValues.Clear();
            return;
        }

        if (currentRitualValues[ingredient.Name] - amount == 0)
        {
            currentRitualValues.Remove(ingredient.Name);
        }

        if(currentRitualValues.Count == 0) 
        { 
            isAvailable = true;
            PlayerEventHandler.Instance.EnableRitual(this);
        }
    }
}
