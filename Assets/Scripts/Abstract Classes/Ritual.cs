using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Ritual : IRitual
{
    private bool isAvailable = false;
    public bool IsAvailable => isAvailable;

    private RitualScriptableObject ritualData;
    public RitualScriptableObject RitualData { get => ritualData; set { ritualData = value; } }

    protected IPotion reward = null;
    public IPotion RewardPotion => reward;

    protected Dictionary<string, int> currentRitualValues = new Dictionary<string, int>();
    protected readonly Dictionary<string, int> defaultRitualValues = new Dictionary<string, int>();

    public Ritual(RitualScriptableObject data)
    {
        ritualData = data;
        currentRitualValues.AddRange(GetRitualStages());
        defaultRitualValues.AddRange(GetRitualStages());
        reward = GetReward();
        PlayerEventHandler.Instance.EmptiedCauldron += OnCauldronEmptied;
        PlayerEventHandler.Instance.CollectedIngredient += OnIngredientCollected;
    }
    private KeyValuePair<string, int> ConvertToKeyValuePair(RitualRecipe item)
    {
        return new KeyValuePair<string, int>(item.item, item.amount);
    }
    protected Dictionary<string, int> GetRitualStages()
    {
        Dictionary<string, int> ritualStages = ritualData.ritualRecipes.ToDictionary(item => item.item, item => item.amount);
        return ritualStages;
    }
    protected abstract IPotion GetReward();
    protected abstract void CompleteAnEvent();

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
        if(ingredient == null) { return; }
        if (!currentRitualValues.ContainsKey(ingredient.IngredientName))
        {
            isAvailable = false;
            currentRitualValues.Clear();
            return;
        }

        if (currentRitualValues[ingredient.IngredientName] - amount == 0)
        {
            currentRitualValues.Remove(ingredient.IngredientName);
        }

        if(currentRitualValues.Count == 0) 
        { 
            isAvailable = true;
            CompleteAnEvent();
        }
    }
}
