using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class PlayerInventory: MonoBehaviour
{
    public int size = 0;
    private Dictionary<string, int> cauldronContents;
    [SerializeField] private IngredientCombos combos;
    public event Action<IIngredient, int> CollectedIngredient;
    public IRecipe currentRecipe;

    // Start is called before the first frame updates

    private void Awake()
    {
        cauldronContents = new Dictionary<string, int>();
        currentRecipe = new SpeedRecipe();
        currentRecipe.Init(this);
        RecipeUIManager.Instance.Activate(currentRecipe);
    }

    public void AddToCauldron(IIngredient ingredient)
    {
        foreach (var ingr in cauldronContents)
        {
            if(cauldronContents.ContainsKey(ingredient.Name))
            {
                cauldronContents[ingredient.Name] += 1;
                CollectedIngredient?.Invoke(ingredient, cauldronContents[ingredient.Name]);
                return;
            } else if (!combos.CheckIngredientCombos(ingredient.Name, ingr.Key))
            {
                cauldronContents.Clear();
                CollectedIngredient?.Invoke(null, 0);
                return;
            }
        }

        if (cauldronContents.Count < size)
        {
            cauldronContents[ingredient.Name] = 1;
            CollectedIngredient?.Invoke(ingredient, cauldronContents[ingredient.Name]);
        }
    }
}
