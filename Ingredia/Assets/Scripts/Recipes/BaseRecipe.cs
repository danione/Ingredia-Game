using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class BaseRecipe
{
    public string name;
    public Dictionary<string, int> ingredients;
    public string rewards;

    // Check if the recipe has the ingredient
    public virtual int Cook(string ingredient)
    {
        if (!ingredients.ContainsKey(ingredient))
        {
            return -1;
        }
        // If an ingredient is needed more than once,
        // the recipe is not completed - just decrease the counter
        if (ingredients[ingredient] > 1)
        {
            ingredients[ingredient]--;
            return 1;
        } else 
        {
            // If it's needed less than once, destroy it and check if completed
            ingredients.Remove(ingredient);
            return ingredients.Count == 0 ? 0 : 1;
        }
    }

    public virtual void Reward()
    {
        // Add some rewards
        Debug.Log(rewards);
    }
}
