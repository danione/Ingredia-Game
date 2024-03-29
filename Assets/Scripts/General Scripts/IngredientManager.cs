using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngredientManager : MonoBehaviour
{
    [SerializeField] private List<IngredientData> defaultIngredients = new();
    [SerializeField] private List<IngredientData> ingredients = new();
    private HashSet<IngredientData> ingredientsSet = new();

    private void Start()
    {
        ingredientsSet = ingredients.ToHashSet();
        PlayerEventHandler.Instance.PlayerDied += RevertToDefault;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        PlayerEventHandler.Instance.PlayerDied -= RevertToDefault;
    }

    private void OnSceneUnloaded(Scene a)
    {
        if(a.name == "Normal Level")
        {
            RevertToDefault();
        }
    }

    public void UnlockIngredient(IngredientData ingredient)
    {
        if(ingredientsSet.Contains(ingredient)) { return; }

        ingredients.Add(ingredient);
        ingredientsSet.Add(ingredient);
    }

    public List<IngredientData> GetIngredients()
    {
        return ingredients;
    }

    public void RemoveOneIngredient()
    {
        if(ingredients.Count == 0) { return; }

        IngredientData data = ingredients[ingredients.Count - 1];
        ingredients.RemoveAt(ingredients.Count - 1);
        ingredientsSet.Remove(data);
    }

    // Should only be used from the tutorial
    // --
    // Adds a default unlockable ingredient
    public void UnlockIngredientDefault(IngredientData ingredient)
    {
        defaultIngredients.Add(ingredient);
    }

    public void RevertToDefault()
    {
        ingredients.Clear();
        ingredientsSet.Clear();
        foreach(var ingredient in defaultIngredients)
        {
            ingredients.Add(ingredient);
        }
        ingredientsSet = ingredients.ToHashSet();
    }

    public void RemoveRitualValues(RitualScriptableObject ritual)
    {
        foreach(var ingredient in ritual.ritualRecipes)
        {
            if (ingredientsSet.Contains(ingredient.item))
            {
                ingredientsSet.Remove(ingredient.item);
                ingredients.Remove(ingredient.item);
            }
        }

    }
}
