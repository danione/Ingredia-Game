using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Checks and keeps ingredient combinations
 */
public class IngredientCombos: MonoBehaviour
{
    private HashSet<string> combinations;

    private void Awake()
    {
        combinations = new HashSet<string>();

        Stack<string> st = new Stack<string>();

        // Read from the file the combinations
        // e.g water pumpkin
        using (StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "Combinations.txt")))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] str = line.Split(' ');
                AddIngredientCombo(str[0], str[1]);
            }
        }
    }

    public bool CheckIngredientCombos(IIngredient ingredient1, IIngredient ingredient2)
    {
        return combinations.Contains(GetCombinationKey(ingredient1.Name, ingredient2.Name));
    }

    private bool CheckIngredientCombos(string combo)
    {
        return combinations.Contains(combo);
    }

    private void AddIngredientCombo(string ingredient1, string ingredient2)
    {
        string key = GetCombinationKey(ingredient1, ingredient2);
        if (!CheckIngredientCombos(key))
        {
            combinations.Add(key);
        }
    }

    private string GetCombinationKey(string name1, string name2)
    {
        // Sort the ingredient names to create a unique combination key
        string[] ingredientNames = { name1, name2 };
        Array.Sort(ingredientNames);
        return string.Join("_", ingredientNames);
    }
}



