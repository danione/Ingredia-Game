using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class RecipeFactory : MonoBehaviour
{
    [SerializeField] private Transform recipeObject;
    private List<Type> recipeTypes = new List<Type>();
    [SerializeField] private float spawnChance = 0.01f;
    [SerializeField] private float chanceIncreasePerFrame = 1f;
    [SerializeField] private const float spawnChanceDefault = 0.01f;
    [SerializeField] private float spawnCheckFrequencyInSeconds = 1f;

    [SerializeField] private float xBorderSpawnLeft;
    [SerializeField] private float xBorderSpawnRight;
    [SerializeField] private float yPointSpawn;

    private void Awake()
    {
        // Find all types in the assembly that implement IRecipe.
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IRecipe).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

        recipeTypes.AddRange(types);
    }

   /* public IRecipe GetRandomRecipe()
    {
        if (recipeTypes.Count == 0)
        {
            Debug.LogError("No classes implementing IRecipe found.");
            return null;
        }
        // Calculate the total probability sum.
        float totalProbability = recipeTypes.Sum(type =>
        {
            MethodInfo getProbabilityMethod = type.GetMethod("GetProbability");
            if (getProbabilityMethod != null)
            {
                return (float)getProbabilityMethod.Invoke(Activator.CreateInstance(type), null);
            }
            return 0f;
        });

        // Generate a random value between 0 and the total probability.
        float randomValue = UnityEngine.Random.Range(0f, totalProbability);

        // Iterate through recipe types to find the selected type.
        foreach (var type in recipeTypes)
        {
            MethodInfo getProbabilityMethod = type.GetMethod("GetProbability");
            if (getProbabilityMethod != null)
            {
                float probability = (float)getProbabilityMethod.Invoke(Activator.CreateInstance(type), null);
                if (randomValue < probability)
                {
                    // Create an instance of the selected type.
                    return Activator.CreateInstance(type) as IRecipe;
                }
                randomValue -= probability;
            }
        }

        return null;
    }*/
    private IRecipe GetRandomRecipe()
    {
        if (recipeTypes.Count == 0)
        {
            Debug.LogError("No classes implementing IRecipe found.");
            return null;
        }

        int randomType = UnityEngine.Random.Range(0, recipeTypes.Count);

        return Activator.CreateInstance(recipeTypes[randomType]) as IRecipe;
    }
    private void Start()
    {
        StartCoroutine(SpawnRecipe());
        
    }

    IEnumerator SpawnRecipe()
    {
        while (!GameManager.Instance.gameOver)
        {
            float randomValue = UnityEngine.Random.Range(0.01f, 1.00f);
            if (randomValue < spawnChance)
            {
                IRecipe recipe = GetRandomRecipe();
                if(recipe != null)
                {
                    Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(xBorderSpawnLeft, xBorderSpawnRight), yPointSpawn, 1);
                    GameObject spawnedObject = Instantiate(recipeObject.gameObject, spawnPosition, recipeObject.rotation);
                    spawnedObject.AddComponent(recipe.GetType());
                    spawnChance = spawnChanceDefault;
                }
            }
            else
            {
                spawnChance += chanceIncreasePerFrame;
            }
            yield return new WaitForSeconds(spawnCheckFrequencyInSeconds);
        }
        
    }
}
