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
    [SerializeField] private List<IngredientData> _rareIngredients;
    [SerializeField] private SpawnLocationData spawnLocation;
    [SerializeField] private SpawnFrequencyData spawnFrequency;
    [SerializeField] private Product prefab;

    private HashSet<IngredientData> _highlight = new();

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

        StartCoroutine(SpawnIngredients(() => SpawnRandomIngredient()));
        StartCoroutine(SpawnIngredients(() => SpawnRandomRareIngredient()));
        StartCoroutine(ResetNextPoint());

        GameEventHandler.Instance.ActivatedSmartRitualHelper += OnActivateHelper;
        GameEventHandler.Instance.GeneratedIngredientAtPos += SpawnRandomIngredient;
    }

    private IEnumerator ResetNextPoint()
    {
        while (!GameManager.Instance.gameOver)
        {
            Debug.Log(spawnPointsOnCooldown.Count);
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
        SpawnPoint newXPoint = PickNewRandomPointX();

        if (newXPoint == null) return;

        Vector3 newRandomLocation = new Vector3(newXPoint.xPos, spawnLocation.yLocation, spawnZLocation);

        Product product = spawner.GetProduct(newRandomLocation, list[randomIndex]);

        if (product == null) return;

        if (_highlight.Contains(product.gameObject.GetComponent<IIngredient>().Data))
        {
            product.GetComponent<BasicIngredient>().Highlight();
        }
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
