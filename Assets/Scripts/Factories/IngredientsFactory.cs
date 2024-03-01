using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using UnityEngine;

public class IngredientsFactory: MonoBehaviour
{
    [SerializeField] private ObjectsSpawner spawner;
    [SerializeField] private List<IngredientData> _ingredients;
    [SerializeField] private BoundariesData boundariesData;
    [SerializeField] private SpawnFrequencyData spawnFrequency;
    [SerializeField] private Product prefab;
    private bool isSpawning;

    private SpawnPointManager spawnPointManager;
    
    // Private Variables
    private float spawnZLocation = 2.0f;

    private void Awake()
    {
        // ingredientsFactories = new ObjectFactory(prefab);
        spawner = new ObjectsSpawner(prefab);
    }

    void Start()
    {
        spawnPointManager = new SpawnPointManager(boundariesData, (spawnFrequency.maxFrequency - spawnFrequency.minFrequency) / 2);
        
        isSpawning = true;

        StartCoroutine(SpawnIngredients(() => SpawnRandomIngredient()));
        StartCoroutine(spawnPointManager.ResetNextPoint());

        GameEventHandler.Instance.GeneratedIngredientAtPos += SpawnRandomIngredient;
    }

    private void OnDestroy()
    {
        try
        {
            GameEventHandler.Instance.GeneratedIngredientAtPos -= SpawnRandomIngredient;
        }
        catch { }
    }

    public void SetSpawning(bool isSpawning)
    {
        this.isSpawning = isSpawning;
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
        spawner.GetProduct(pos, _ingredients[randomIndex]).GetComponent<FallableObject>().SwapToMove();
    }

    private void HandleIngredientSpawn(List<IngredientData> list, int randomIndex)
    {
        // Select a random location at the top of the screen
        SpawnPoint newXPoint = spawnPointManager.PickNewRandomPoint(isXPoint: true);
        SpawnPoint newYPoint = spawnPointManager.PickNewRandomPoint(isXPoint: false);

        if (newXPoint == null || list.Count == 0) return;

        Vector3 newRandomLocation = new Vector3(newXPoint.pos, newYPoint.pos, spawnZLocation);

        spawner.GetProduct(newRandomLocation, list[randomIndex]).GetComponent<FallableObject>().SwapToMove();
    }

    // Spawns ingredients at random times
    private IEnumerator SpawnIngredients(Action spawnMethod)
    {
        while (!GameManager.Instance.gameOver)
        {
            // Select a random object to spawn
            if(isSpawning) spawnMethod();
            yield return new WaitForSeconds(UnityEngine.Random.Range(spawnFrequency.minFrequency, spawnFrequency.maxFrequency));

        }
        yield return null;
    }

    public int GetCountOfIngredients()
    {
        return _ingredients.Count;
    }
    
    // Should be used for tutorial only
    public void AppendARegularIngredient(IngredientData ingredient)
    {
        _ingredients.Add(ingredient);
    }

    public void RemoveRegularIngredient()
    {
        if(_ingredients.Count == 0) return;

        _ingredients.RemoveAt(_ingredients.Count - 1);

        if(_ingredients.Count == 0) isSpawning = false;
    }
}
