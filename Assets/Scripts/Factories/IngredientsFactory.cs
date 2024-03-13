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
    [SerializeField] private BoundariesData boundariesData;
    [SerializeField] private SpawnFrequencyData spawnFrequency;
    [SerializeField] private Product prefab;
    [SerializeField] private float spawnModifier;
    private float currentModifier = 0;
    private bool isSpawning;
    private List<IngredientData> _ingredients = new();
    private SpawnPointManager spawnPointManager;
    
    // Private Variables
    private float spawnZLocation = 2.0f;

    private void Awake()
    {
        spawner = new ObjectsSpawner(prefab);

    }

    void Start()
    {
        spawnPointManager = new SpawnPointManager(boundariesData, (spawnFrequency.maxFrequency - spawnFrequency.minFrequency) / 2);
        _ingredients = GameManager.Instance.GetComponent<IngredientManager>().GetIngredients();

        isSpawning = true;

        StartCoroutine(SpawnIngredients(() => SpawnRandomIngredient()));
        StartCoroutine(spawnPointManager.ResetNextPoint());

        GameEventHandler.Instance.GeneratedIngredientAtPos += SpawnRandomIngredient;
        GameEventHandler.Instance.UnlockedRitual += OnNewRitualUnlocked;
    }

    private void OnDestroy()
    {
        try
        {
            GameEventHandler.Instance.GeneratedIngredientAtPos -= SpawnRandomIngredient;
            GameEventHandler.Instance.UnlockedRitual -= OnNewRitualUnlocked;
        }
        catch { }
    }

    private void OnNewRitualUnlocked(RitualScriptableObject ritual)
    {
        currentModifier += spawnModifier;
        spawnPointManager.ChangeDequeueTime((spawnFrequency.maxFrequency - spawnFrequency.minFrequency - currentModifier) / 2);
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
            if (isSpawning && _ingredients.Count > 0) spawnMethod();
            yield return new WaitForSeconds(UnityEngine.Random.Range(spawnFrequency.minFrequency, spawnFrequency.maxFrequency - currentModifier));

        }
        yield return null;
    }
}
