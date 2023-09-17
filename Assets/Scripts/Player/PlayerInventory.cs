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

    // Start is called before the first frame updates

    private void Awake()
    {
        cauldronContents = new Dictionary<string, int>();
    }

    public void AddToCauldron(IIngredient ingredient)
    {
        foreach (var ingr in cauldronContents)
        {
            if(cauldronContents.ContainsKey(ingredient.Name))
            {
                cauldronContents[ingredient.Name] += 1;
                Debug.Log(ingredient.Name + " has now " + cauldronContents[ingredient.Name]);
                CollectedIngredient?.Invoke(ingredient);
                return;
            } else if (!combos.CheckIngredientCombos(ingredient.Name, ingr.Key))
            {
                Debug.Log("No combination, aborting!");
                cauldronContents.Clear();
                CollectedIngredient?.Invoke(null);
                return;
            }
        }

        if (cauldronContents.Count < size)
        {
            cauldronContents[ingredient.Name] = 1;
            CollectedIngredient?.Invoke(ingredient);
            Debug.Log(ingredient.Name + " added to cauldron!");
        }
    }
}
