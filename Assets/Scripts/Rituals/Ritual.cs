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
    
    protected IPotion reward = null;
    public IPotion RewardPotion => reward;

    protected Dictionary<string, int> currentRitualValues = new Dictionary<string, int>();
    protected readonly Dictionary<string, int> defaultRitualValues = new Dictionary<string, int>();

    public Ritual(RitualScriptableObject data, IPotion rewardPotion)
    {
        ritualData = data;
        currentRitualValues.AddRange(GetRitualStages());
        defaultRitualValues.AddRange(GetRitualStages());
        reward = rewardPotion;
        isEnabled = false;
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

    protected Dictionary<string, int> GetRitualStages()
    {
        Dictionary<string, int> ritualStages = ritualData.ritualRecipes.ToDictionary(item => item.item, item => item.amount);
        return ritualStages;
    }

    protected void CompleteAnEvent()
    {
        PlayerEventHandler.Instance.CompleteBenevolentRitual(this);
    }

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

        if (!currentRitualValues.ContainsKey(ingredient.Data.ingredientName))
        {
            isAvailable = false;
            currentRitualValues.Clear();
            PlayerEventHandler.Instance.CollectedWrongIngredient(ritualData.name);
            return;
        }

        if (currentRitualValues[ingredient.Data.ingredientName] - amount == 0)
        {
            currentRitualValues.Remove(ingredient.Data.ingredientName);
        }

        if(currentRitualValues.Count == 0) 
        { 
            isAvailable = true;
            CompleteAnEvent();
            PlayerEventHandler.Instance.UnlockARitual(ritualData.name);
        }
    }
}
