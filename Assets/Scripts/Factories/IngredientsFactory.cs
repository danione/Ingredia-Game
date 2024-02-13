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
    [SerializeField] private SpawnLocationData spawnLocation;
    [SerializeField] private SpawnFrequencyData spawnFrequency;
    [SerializeField] private Product prefab;
    private bool isSpawning;

    private List<SpawnPoint> xPoints = new();
    private Queue<SpawnPoint> spawnPointsOnCooldown = new ();
    [SerializeField] private float offsetX;
    

    // Private Variables
    private float spawnZLocation = 2.0f;

    private void Awake()
    {
        // ingredientsFactories = new ObjectFactory(prefab);
        spawner = new ObjectsSpawner(prefab);
    }

    void Start()
    {
        int numberOfPoints = CountXPointsBetween(spawnLocation.xLeftMax, spawnLocation.xRightMax, offsetX);
        xPoints = Enumerable.Range((int) spawnLocation.xLeftMax, numberOfPoints - 1).Select(x => new SpawnPoint(x + offsetX)).ToList();
        isSpawning = true;

        StartCoroutine(SpawnIngredients(() => SpawnRandomIngredient()));
        StartCoroutine(ResetNextPoint());

        GameEventHandler.Instance.GeneratedIngredientAtPos += SpawnRandomIngredient;
    }

    public void SetSpawning(bool isSpawning)
    {
        this.isSpawning = isSpawning;
    }

    private IEnumerator ResetNextPoint()
    {
        while (!GameManager.Instance.gameOver)
        {
            if(spawnPointsOnCooldown.Count > 0)
            {
                SpawnPoint point = spawnPointsOnCooldown.Dequeue();
                point.isInQueue = false;
            }
            yield return new WaitForSeconds((spawnFrequency.maxFrequency - spawnFrequency.minFrequency)/2);
        }
    }

    private int CountXPointsBetween(float startX, float endX, float offsetX)
    {
        // Calculate the distance between the x-coordinates of the start and end points
        float distanceX = Mathf.Abs(endX - startX);
        // Calculate the number of points that can fit along the x-axis between the two points
        int numberOfPoints = Mathf.FloorToInt(distanceX / offsetX);
        return numberOfPoints;
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
        spawner.GetProduct(pos, _ingredients[randomIndex]);

    }

    private void HandleIngredientSpawn(List<IngredientData> list, int randomIndex)
    {
        // Select a random location at the top of the screen
        SpawnPoint newXPoint = PickNewRandomPointX();

        if (newXPoint == null || list.Count == 0) return;

        Vector3 newRandomLocation = new Vector3(newXPoint.xPos, spawnLocation.yLocation, spawnZLocation);

        spawner.GetProduct(newRandomLocation, list[randomIndex]);
    }

    private SpawnPoint PickNewRandomPointX()
    {
        SpawnPoint newXPoint = null;
        newXPoint = xPoints.Where(p => !p.isInQueue).OrderBy(p => Guid.NewGuid()).FirstOrDefault();

        if (newXPoint == null) return null;

        spawnPointsOnCooldown.Enqueue(newXPoint);
        newXPoint.isInQueue = true;
        return newXPoint;
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
