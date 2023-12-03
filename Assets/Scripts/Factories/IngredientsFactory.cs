using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsFactory: MonoBehaviour
{
    [SerializeField] private List<IngredientData> _ingredients;
    [SerializeField] private List<IngredientData> _rareIngredients;
    [SerializeField] private SpawnLocationData spawnLocation;
    [SerializeField] private SpawnFrequencyData spawnFrequency;
    [SerializeField] private Transform prefab;
    private Factory ingredientsFactories;

    // Private Variables

    private float spawnZLocation = 2.0f;

    private void Awake()
    {
        ingredientsFactories = new ObjectFactory(prefab);
    }

    void Start()
    {
        StartCoroutine(SpawnIngredients(() => SpawnRandomIngredient()));
        StartCoroutine(SpawnIngredients(() => SpawnRandomRareIngredient()));
    }

    private void SpawnRandomIngredient()
    {
        int randomIndex = UnityEngine.Random.Range(0, _ingredients.Count);
        // Select a random location at the top of the screen
        HandleIngredientSpawn(_ingredients, randomIndex);
    }

    private void SpawnRandomRareIngredient()
    {
        float randomChance = UnityEngine.Random.Range(0f, 1f);
        int randomIndex = UnityEngine.Random.Range(0, _rareIngredients.Count);
        if(randomChance < _rareIngredients[randomIndex].spawnChance)
        {
            HandleIngredientSpawn(_rareIngredients,randomIndex);
        }
    }

    private void HandleIngredientSpawn(List<IngredientData> list, int randomIndex)
    {
        // Select a random location at the top of the screen
        Vector3 newRandomLocation = new Vector3(UnityEngine.Random.Range(spawnLocation.xRightMax,
            spawnLocation.xLeftMax), spawnLocation.yLocation, spawnZLocation);

        ingredientsFactories.GetProduct(newRandomLocation, list[randomIndex]);
    }

    // Spawns ingredients at random times
    private IEnumerator SpawnIngredients(Action spawnMethod)
    {
        while (!GameManager.Instance.gameOver)
        {
            // Select a random object to spawn
            spawnMethod();
            yield return new WaitForSeconds(UnityEngine.Random.Range(spawnFrequency.minFrequency, spawnFrequency.maxFrequency));

        }
        yield return null;
    }

    public int GetCountOfIngredients()
    {
        return _ingredients.Count;
    }
}
