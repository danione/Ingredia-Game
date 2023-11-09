using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsFactory: MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _ingredients;
    [SerializeField] private SpawnPointsScriptableObject spawnLocation;
    [SerializeField] private float minSeconds;
    [SerializeField] private float maxSeconds;

    private List<Factory> ingredientsFactories = new List<Factory>();

    // Private Variables

    private float spawnZLocation = 2.0f;

    private void Awake()
    {
        foreach(var ingredient in _ingredients)
        {
            ingredientsFactories.Add(new ObjectFactory(ingredient));
        }
    }

    void Start()
    {
        StartCoroutine(SpawnIngredients());
    }

    // Spawns ingredients at random times
    private IEnumerator SpawnIngredients()
    {
        while (!GameManager.Instance.gameOver)
        {

            // Select a random object to spawn
            int randomIndex = Random.Range(0, ingredientsFactories.Count);
            // Select a random location at the top of the screen
            Vector3 newRandomLocation = new Vector3(Random.Range(spawnLocation.xRightMax, spawnLocation.xLeftMax), spawnLocation.yLocation, spawnZLocation);

            ingredientsFactories[randomIndex].GetProduct(newRandomLocation);
            yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));
            
        }
        yield return null;
    }

}
