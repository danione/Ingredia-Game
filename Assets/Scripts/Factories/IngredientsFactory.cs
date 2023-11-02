using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsFactory: MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _ingredients;

    [SerializeField] private float leftBorder = 13.0f;
    [SerializeField] private float rightBorder = -13.0f;
    [SerializeField] private float minSeconds;
    [SerializeField] private float maxSeconds;
    [SerializeField] private float spawnYLocation = 11.0f;

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
            Vector3 spawnLocation = new Vector3(Random.Range(rightBorder, leftBorder), spawnYLocation, spawnZLocation);

            ingredientsFactories[randomIndex].GetProduct(spawnLocation);
            yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));
            
        }
        yield return null;
    }

}
