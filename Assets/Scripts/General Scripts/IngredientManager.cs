using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    [SerializeField] private List<IngredientData> ingredients = new();
    private HashSet<IngredientData> ingredientsSet = new();

    private void Start()
    {
        ingredientsSet = ingredients.ToHashSet();
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
}
