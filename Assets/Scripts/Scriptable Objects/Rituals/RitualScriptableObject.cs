using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ritual", menuName = "Rituals")]
public class RitualScriptableObject : ScriptableObject
{
    public new string name;
    public string description;
    public List<RitualRecipe> ritualRecipes = new List<RitualRecipe>();
}

[System.Serializable]
public class RitualRecipe
{
    public string item;
    public int amount;
}
