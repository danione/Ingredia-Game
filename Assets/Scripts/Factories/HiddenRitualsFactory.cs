using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRitualsFactory : MonoBehaviour
{
    [SerializeField] private Transform recipeObject;
    [SerializeField] private ObjectFactory recipeFactory;
    [SerializeField] private SpawnFrequencyData spawnFrequencyData;
    [SerializeField] private SpawnLocationData spawnLocationData;
    
    private float currentSpawnChance;

    // Start is called before the first frame update
    void Start()
    {
        recipeFactory = new ObjectFactory(recipeObject);
        currentSpawnChance = spawnFrequencyData.spawnChance;
        StartCoroutine(SpawnHiddenRitual());
    }

    IEnumerator SpawnHiddenRitual()
    {
        RitualManager ritualManager = GameManager.Instance.GetComponent<RitualManager>();
        if (ritualManager != null)
        {
            while (!GameManager.Instance.gameOver)
            {
                GenerateNewRitual(ritualManager);

                yield return new WaitForSeconds(Random.Range(spawnFrequencyData.minFrequency, spawnFrequencyData.maxFrequency));
            }
        }
    }

    private void GenerateNewRitual(RitualManager manager)
    {
        if (manager.HasLockedHiddenRituals()) { return; } 
        
        if(Random.Range(0f, 1f) < currentSpawnChance)
        {
            Vector3 position = new(Random.Range(spawnLocationData.xRightMax, spawnLocationData.xLeftMax), spawnLocationData.yLocation, 2);
            recipeFactory.GetProduct(position);
        }
        else
        {
            currentSpawnChance += spawnFrequencyData.spawnChanceIncrease;
        }
    }
}
