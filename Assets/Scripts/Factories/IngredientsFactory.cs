using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsFactory: MonoBehaviour
{
    [SerializeField] private Ashes ashes;
    [SerializeField] private Fishbones fishbones;
    [SerializeField] private Pumpkin pumpkin;
    [SerializeField] private SilkenThread silkenthread;
    [SerializeField] private Water water;

    [SerializeField] private float leftBorder = 13.0f;
    [SerializeField] private float rightBorder = -13.0f;
    [SerializeField] private float minSeconds;
    [SerializeField] private float maxSeconds;

    private List<Factory> ingredientsFactories = new List<Factory>();

    // Private Variables
    private float spawnYLocation = 11.0f;
    private float spawnZLocation = 2.0f;

    private void Awake()
    {
        ingredientsFactories.Add(new ObjectFactory<Ashes>(ashes));
        ingredientsFactories.Add(new ObjectFactory<Fishbones>(fishbones));
        ingredientsFactories.Add(new ObjectFactory<Pumpkin>(pumpkin));
        ingredientsFactories.Add(new ObjectFactory<SilkenThread>(silkenthread));
        ingredientsFactories.Add(new ObjectFactory<Water>(water));
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
