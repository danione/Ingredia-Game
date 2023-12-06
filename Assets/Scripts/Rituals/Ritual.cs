using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class Ritual : IRitual
{
    private bool isAvailable = false;
    public bool IsAvailable => isAvailable;

    public bool isEnabled { get; private set; }

    private RitualScriptableObject ritualData;
    public RitualScriptableObject RitualData { get => ritualData; set { ritualData = value; } }

    protected Dictionary<IngredientData, int> currentRitualValues;
    protected readonly Dictionary<IngredientData, int> defaultRitualValues;

    public float Difficulty { get; private set; }


    public Ritual(RitualScriptableObject data)
    {
        currentRitualValues = new();
        defaultRitualValues = new();
        ritualData = data;
        currentRitualValues.AddRange(GetRitualStages());
        defaultRitualValues.AddRange(GetRitualStages());
        isEnabled = false;
        Difficulty = CalculateDifficulty();
    }

    private float CalculateDifficulty()
    {
        float countOfItems = currentRitualValues.Values.Count * Constants.Instance.DifficultyModifiers.CountWeight;
        float quantityOfItems = currentRitualValues.Values.Sum() * Constants.Instance.DifficultyModifiers.QuantityWeight;
        float spawnTimeOfItems = currentRitualValues.Keys.Sum(key => key.spawnChance == 0 ? (1.0f / Constants.Instance.IngredientsCount) : key.spawnChance);
        return countOfItems + quantityOfItems + spawnTimeOfItems;
    }

    public void EnableRitual()
    {
        isEnabled = true;
        PlayerEventHandler.Instance.EmptiedCauldron += OnCauldronEmptied;
        PlayerEventHandler.Instance.CollectedIngredient += OnIngredientCollected;
    }

    public void DisableRitual() { 
        isEnabled = false;
        PlayerEventHandler.Instance.EmptiedCauldron -= OnCauldronEmptied;
        PlayerEventHandler.Instance.CollectedIngredient -= OnIngredientCollected;
        OnCauldronEmptied();
    }

    protected Dictionary<IngredientData, int> GetRitualStages()
    {
        Dictionary<IngredientData, int> ritualStages = ritualData.ritualRecipes.ToDictionary(item => item.item, item => item.amount);
        return ritualStages;
    }

    protected void CompleteAnEvent()
    {
        PlayerEventHandler.Instance.CompleteBenevolentRitual(this);
    }

    public void OnCauldronEmptied()
    {
        isAvailable = false;
        currentRitualValues.Clear();
        currentRitualValues.AddRange(defaultRitualValues);
        Difficulty = CalculateDifficulty();
    }

    public void OnIngredientCollected(IIngredient ingredient, int amount)
    {
        if(ingredient == null) { return; }

        if (!currentRitualValues.ContainsKey(ingredient.Data))
        {
            isAvailable = false;
            currentRitualValues.Clear();
            PlayerEventHandler.Instance.CollectedWrongIngredient(ritualData.name);
            Difficulty = float.PositiveInfinity;
            return;
        }

        if (currentRitualValues[ingredient.Data] - amount == 0)
        {
            currentRitualValues.Remove(ingredient.Data);
            Difficulty = CalculateDifficulty();
            GameEventHandler.Instance.CollectExistingIngredient(this);
        }

        if(currentRitualValues.Count == 0) 
        { 
            isAvailable = true;
            CompleteAnEvent();
            PlayerEventHandler.Instance.UnlockARitual(ritualData.name);
        }
    }

    public void HighlightIngredients()
    {
        foreach(var item in currentRitualValues)
        {
            GameEventHandler.Instance.HighlightIngredient(item.Key);
        }
    }

    public List<IngredientData> GetCurrentLeftIngredients()
    {
        return currentRitualValues.Keys.ToList();
    }
}
