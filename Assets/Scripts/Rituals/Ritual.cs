using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Ritual : IRitual
{
    private bool isAvailable = false;
    public bool IsAvailable => isAvailable;

    public bool isEnabled { get; private set; }

    private RitualScriptableObject ritualData;
    public RitualScriptableObject RitualData { get => ritualData; set { ritualData = value; } }

    protected Dictionary<IngredientData, int> currentRitualValues;
    protected Dictionary<IngredientData, int> defaultRitualValues;

    private int currentReward = 0;

    private int countCompleted;
    private bool shouldIterate = false;

    public Ritual(RitualScriptableObject data)
    {
        countCompleted = 0;
        currentRitualValues = new();
        defaultRitualValues = new();
        ritualData = data;
        AddRange(GetRitualStages(), currentRitualValues);
        AddRange(GetRitualStages(), defaultRitualValues);
        isEnabled = false;
        shouldIterate = data.iterationRewards.Count <= data.potionRewardData.Count; 
    }

    ~Ritual()
    {
        try
        {
            PlayerEventHandler.Instance.EmptiedCauldron -= OnCauldronEmptied;
            PlayerEventHandler.Instance.CollectedIngredient -= OnIngredientCollected;
            PlayerEventHandler.Instance.PerformedRitual -= AwardSophistication;
        }
        catch { }
    }

    public void Reset()
    {
        countCompleted = 0;
        currentRitualValues = new();
        defaultRitualValues = new();
        AddRange(GetRitualStages(), currentRitualValues);
        AddRange(GetRitualStages(), defaultRitualValues);
        isEnabled = false;
        currentReward = 0;
    }

    private float GetSophistication()
    {
        return ritualData.sophisticationReward * Mathf.Exp(-(countCompleted * Constants.Instance.sophisticationDecayConstant));
    }

    public void EnableRitual()
    {
        if (isEnabled) return;

        isEnabled = true;
        PlayerEventHandler.Instance.EmptiedCauldron += OnCauldronEmptied;
        PlayerEventHandler.Instance.CollectedIngredient += OnIngredientCollected;
        PlayerEventHandler.Instance.PerformedRitual += AwardSophistication;
    }


    protected Dictionary<IngredientData, int> GetRitualStages()
    {
        Dictionary<IngredientData, int> ritualStages = ritualData.ritualRecipes.ToDictionary(item => item.item, item => item.amount);
        return ritualStages;
    }

    private void AddRange(Dictionary<IngredientData, int> source, Dictionary<IngredientData, int> destination)
    {
        foreach (var kvp in source)
        {
            if (!destination.ContainsKey(kvp.Key))
            {
                // Add the key-value pair to the destination dictionary
                destination.Add(kvp.Key, kvp.Value);
            }
        }
    }

    public void OnCauldronEmptied()
    {
        isAvailable = false;
        currentRitualValues.Clear();
        AddRange(defaultRitualValues, currentRitualValues);
    }

    public void OnIngredientCollected(IIngredient ingredient, int amount)
    {
        if(ingredient == null) { return; }

        if (!currentRitualValues.ContainsKey(ingredient.Data))
        {
            isAvailable = false;
            currentRitualValues.Clear();
            PlayerEventHandler.Instance.CollectedWrongIngredient(ritualData.ritualName);
            return;
        }

        if (currentRitualValues[ingredient.Data] - amount == 0)
        {
            currentRitualValues.Remove(ingredient.Data);
            GameEventHandler.Instance.CollectExistingIngredient(this);
        }

        if(currentRitualValues.Count == 0) 
        { 
            isAvailable = true;
            PlayerEventHandler.Instance.CompleteBenevolentRitual(this);
        }
    }

    private void AwardSophistication(IRitual rit)
    {
        if (rit != this) return;

        PlayerController.Instance.inventory.AdjustSophistication(GetSophistication());

        if (shouldIterate)
        {
            countCompleted++;
            if(countCompleted >= ritualData.iterationRewards[currentReward])
            {
                IncrementPotionYield();
            }
        }
    }

    public void IncrementPotionYield()
    {
        if (ritualData.potionRewardData.Count > currentReward + 1)
            currentReward++;
    }

    public PotionsData GetPotionReward()
    {
        return RitualData.potionRewardData[currentReward];
    }
}
