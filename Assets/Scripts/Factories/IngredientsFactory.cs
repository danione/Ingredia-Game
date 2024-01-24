using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.PlayerSettings;

public class IngredientsFactory: MonoBehaviour
{
    [SerializeField] private ObjectsSpawner spawner;
    [SerializeField] private List<IngredientData> _ingredients;
    [SerializeField] private List<IngredientData> _rareIngredients;
    [SerializeField] private SpawnLocationData spawnLocation;
    [SerializeField] private SpawnFrequencyData spawnFrequency;
    [SerializeField] private Product prefab;
    private HashSet<IngredientData> _highlight = new();
    

    // Private Variables

    private float spawnZLocation = 2.0f;

    private void Awake()
    {
        // ingredientsFactories = new ObjectFactory(prefab);
        spawner = new ObjectsSpawner(prefab);
    }

    void Start()
    {
        StartCoroutine(SpawnIngredients(() => SpawnRandomIngredient()));
        StartCoroutine(SpawnIngredients(() => SpawnRandomRareIngredient()));
        GameEventHandler.Instance.ActivatedSmartRitualHelper += OnActivateHelper;
        GameEventHandler.Instance.GeneratedIngredientAtPos += SpawnRandomIngredient;
    }

    private void SpawnRandomIngredient()
    {
        int randomIndex = UnityEngine.Random.Range(0, _ingredients.Count);
        // Select a random location at the top of the screen
        HandleIngredientSpawn(_ingredients, randomIndex);
    }

    private void SpawnRandomIngredient(Vector3 pos)
    {
        int randomIndex = UnityEngine.Random.Range(0, _ingredients.Count);
        // Select a random location at the top of the screen
        HandleIngredientSpawn(_ingredients, randomIndex, pos);
    }

    private void SpawnRandomRareIngredient()
    {
        if (_rareIngredients.Count == 0) return;
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

        float randomXPos = UnityEngine.Random.Range(spawnLocation.xRightMax,
            spawnLocation.xLeftMax);
        Vector3 newRandomLocation = new Vector3(randomXPos, spawnLocation.yLocation, spawnZLocation);

        Product product = spawner.GetProduct(newRandomLocation, list[randomIndex]);

        if (_highlight.Contains(product.gameObject.GetComponent<IIngredient>().Data))
        {
            product.GetComponent<BasicIngredient>().Highlight();
        }
    }

    private void HandleIngredientSpawn(List<IngredientData> list, int randomIndex, Vector3 pos)
    {
        Product product = spawner.GetProduct(pos, list[randomIndex]);
        if (_highlight.Contains(product.gameObject.GetComponent<IIngredient>().Data))
        {
            product.GetComponent<BasicIngredient>().Highlight();
        }
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

    private void OnActivateHelper()
    {
        PlayerEventHandler.Instance.EmptiedCauldron += OnCauldronEmpty;
        GameEventHandler.Instance.CollectedExistingIngredient += OnCollectExistingIngredient;
    }

    private void OnCauldronEmpty()
    {
        _highlight.Clear();
    }

    private void OnCollectExistingIngredient(Ritual ritual)
    {
        _highlight = new HashSet<IngredientData>(ritual.GetCurrentLeftIngredients());
    }


    // Should be used for tutorial only
    public void AppendARegularIngredient(IngredientData ingredient)
    {
        _ingredients.Add(ingredient);
    }
}
