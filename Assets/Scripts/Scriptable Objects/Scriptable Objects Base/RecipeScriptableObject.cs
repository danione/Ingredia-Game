using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Scriptable Objects/Recipe")]
public class RecipeScriptableObject : ScriptableObject
{
    public new string name;
    public string description;
    public List<RitualRecipe> ritualRecipes = new List<RitualRecipe>();
}
