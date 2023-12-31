using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ritual", menuName = "Scriptable Objects/Ritual")]
public class RitualScriptableObject : ScriptableObject
{
    public new string name;
    public string description;
    public List<RitualRecipe> ritualRecipes = new();
    public List<PotionsData> potionRewardData;
}

[System.Serializable]
public class RitualRecipe
{
    public IngredientData item;
    public int amount;
}


