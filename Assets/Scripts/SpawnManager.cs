using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private RecipeType recipeVariants;
    [SerializeField] private float leftBorder = 13.0f;
    [SerializeField] private float rightBorder = -13.0f;
    [SerializeField] private float minSeconds;
    [SerializeField] private float maxSeconds;

    // Private Variables
    private float spawnYLocation = 11.0f;
    private float spawnZLocation = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnIngredients",1.0f);
    }

    // Spawns ingredients at random times
    void SpawnIngredients()
    {
        // Select a random object to spawn
        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        // Select a random location at the top of the screen
        Vector3 spawnLocation = new Vector3(Random.Range(rightBorder, leftBorder), spawnYLocation, spawnZLocation);
        
        // Create the object and repeat generation after a random time
        var ingredient = Instantiate(objectsToSpawn[randomIndex],spawnLocation, objectsToSpawn[randomIndex].transform.rotation);

        if (ingredient.CompareTag("Recipe"))
        {
            RecipeType randomType = (RecipeType)Random.Range(0, System.Enum.GetValues(typeof(RecipeType)).Length);
            ingredient.gameObject.GetComponent<Recipe>().type = randomType;
        }

        if (!GameManager.Instance.gameOver)
        {
            Invoke("SpawnIngredients", Random.Range(minSeconds, maxSeconds));
        }
    }
}
