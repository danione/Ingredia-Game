using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Ritual : IRitual
{
    private bool isAvailable = false;
    public bool IsAvailable => isAvailable;

    protected IReward reward = null;
    public IReward Reward => reward;

    protected Dictionary<string, int> currentRitualValues = new Dictionary<string, int>();
    protected readonly Dictionary<string, int> defaultRitualValues = new Dictionary<string, int>();


    public Ritual()
    {
        currentRitualValues.AddRange(GetRitualStages());
        defaultRitualValues.AddRange(GetRitualStages());
        reward = GetReward();
        PlayerEventHandler.Instance.EmptiedCauldron += OnCauldronEmptied;
        PlayerEventHandler.Instance.CollectedIngredient += OnIngredientCollected;
    }

    protected abstract Dictionary<string, int> GetRitualStages();
    protected abstract IReward GetReward();
    protected abstract void GenerateEvent();

    public bool AvailableRitual()
    {
        return isAvailable;
    }

    public void OnCauldronEmptied()
    {
        isAvailable = false;
        currentRitualValues.Clear();
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
            GenerateEvent();
        }
    }
}
