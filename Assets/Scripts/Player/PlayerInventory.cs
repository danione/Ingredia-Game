using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory: MonoBehaviour
{
    public int size = 0;
    private List<IIngredient> cauldronContents;
    [SerializeField] private IngredientCombos combos;

    public List<BaseRecipe> recipes;
    // Start is called before the first frame updates

    private void Awake()
    {
        cauldronContents = new List<IIngredient>();
        recipes = new List<BaseRecipe>();
    }

    public void AddRecipe(BaseRecipe recipe)
    {
        if (recipes.Count > size) { return; }

        recipes.Add(recipe);
    }

    public void AddToCauldron(IIngredient ingredient)
    {
        foreach (IIngredient ingr in cauldronContents)
        {
            if(!combos.CheckIngredientCombos(ingredient, ingr))
            {
                return;
            }
        }
        Debug.Log("Great!");
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
