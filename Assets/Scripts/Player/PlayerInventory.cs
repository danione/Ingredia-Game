using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class PlayerInventory: MonoBehaviour
{
    public int size = 0;
    private Dictionary<string, int> cauldronContents;
    [SerializeField] private IngredientCombos combos;
    public event Action<IIngredient> CollectedIngredient;
    public List<BaseRecipe> recipes;

    public CollectItemRecipeAction testRecipeAction;
    // Start is called before the first frame updates

    private void Awake()
    {
        cauldronContents = new Dictionary<string, int>();
        recipes = new List<BaseRecipe>();
        SilkenThread silk = new SilkenThread();
        testRecipeAction = new CollectItemRecipeAction(silk, 6, this);
    }

    public void AddRecipe(BaseRecipe recipe)
    {
        if (recipes.Count > size) { return; }

        recipes.Add(recipe);
    }

    public void AddToCauldron(IIngredient ingredient)
    {
        foreach (var ingr in cauldronContents)
        {
            if(cauldronContents.ContainsKey(ingredient.Name))
            {
                cauldronContents[ingredient.Name] += 1;
                Debug.Log(ingredient.Name + " has now " + cauldronContents[ingredient.Name]);
                CollectedIngredient.Invoke(ingredient);
                return;
            } else if (!combos.CheckIngredientCombos(ingredient.Name, ingr.Key))
            {
                Debug.Log("No combination, aborting!");
                cauldronContents.Clear();
                CollectedIngredient.Invoke(null);
                return;
            }
        }

        if (cauldronContents.Count < size)
        {
            cauldronContents[ingredient.Name] = 1;
            CollectedIngredient.Invoke(ingredient);
            Debug.Log(ingredient.Name + " added to cauldron!");
        }
    }

    public int CookIngredient(string ingredientName)
    {
        bool hasRecipe = false;
        for (int i = recipes.Count - 1; i >= 0; i--)
        {
            int getResult = recipes[i].Cook(ingredientName);

            if (getResult == 0)
            {
                hasRecipe = true;
                recipes[i].Reward();
                recipes.Remove(recipes[i]);
                break;
            } else if (getResult == 1) { hasRecipe = true;}
        }
        return hasRecipe ? -1 : 0;
    }
}
