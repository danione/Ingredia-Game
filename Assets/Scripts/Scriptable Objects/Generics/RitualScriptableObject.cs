using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ritual", menuName = "Scriptable Objects/Ritual")]
public class RitualScriptableObject : ScriptableObject
{
    public string ritualName;
    public string description;
    public float sophisticationReward;
    public List<RitualRecipe> ritualRecipes = new();
    public List<PotionsData> potionRewardData;
    public List<int> iterationRewards;
}

[System.Serializable]
public class RitualRecipe
{
    public IngredientData item;
    public int amount;
}


