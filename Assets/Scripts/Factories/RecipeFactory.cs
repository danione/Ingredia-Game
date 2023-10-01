using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeFactory : MonoBehaviour
{
    [SerializeField] private Transform recipeObject;

    private List<Type> recipeTypes = new List<Type>();

    private void Awake()
    {
        // Find all types in the assembly that implement IRecipe.
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IRecipe).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

        recipeTypes.AddRange(types);
    }

    public IRecipe GetRandomRecipe()
    {
        if (recipeTypes.Count == 0)
        {
            Debug.LogError("No classes implementing IRecipe found.");
            return null;
        }

        // Choose a random type that implements IRecipe.
        Type randomRecipeType = recipeTypes[UnityEngine.Random.Range(0, recipeTypes.Count)];

        // Create an instance of the selected type.
        return Activator.CreateInstance(randomRecipeType) as IRecipe;
    }

    private void Start()
    {
        IRecipe recipe = GetRandomRecipe();
        Debug.Log("Recipe: " + recipe.GetType().Name + " probability " + recipe.GetProbability());
    }
}
