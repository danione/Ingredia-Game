using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRitualsFactory : MonoBehaviour
{
    [SerializeField] private Transform recipeObject;
    [SerializeField] private SpawnFrequencyData spawnFrequencyData;
    [SerializeField] private SpawnLocationData spawnLocationData;

    private RitualManager manager;
    private string randomRitual;
    private float currentSpawnChance;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnChance = spawnFrequencyData.spawnChance;
        manager = GameManager.Instance.GetComponent<RitualManager>();
        PlayerEventHandler.Instance.CollectedInvalidIngredient += OnCollectedWrongIngredient;
        PlayerEventHandler.Instance.UnlockedRitual += OnUnlockedRitual;
        StartCoroutine(SpawnHiddenRitual());
    }

    IEnumerator SpawnHiddenRitual()
    {
        if (manager != null)
        {
            while (!GameManager.Instance.gameOver)
            {
                GenerateNewRitual();

                yield return new WaitForSeconds(Random.Range(spawnFrequencyData.minFrequency, spawnFrequencyData.maxFrequency));
            }
        }
    }

    private void GenerateNewRitual()
    {
        if (!manager.HasLockedHiddenRituals() || randomRitual != null) { return; } 
        
        if(Random.Range(0f, 1f) < currentSpawnChance)
        {
            Vector3 position = new(Random.Range(spawnLocationData.xRightMax, spawnLocationData.xLeftMax), spawnLocationData.yLocation, 2);
            Instantiate(recipeObject, position, Quaternion.identity);
            randomRitual = manager.GetRandomLockedRitual();
            manager.UnlockRitual(randomRitual);
            PlayerEventHandler.Instance.SetUpHiddenRitual(manager.GetRitualScriptableObject(randomRitual));
            Debug.Log(randomRitual);
        }
        else
        {
            currentSpawnChance += spawnFrequencyData.spawnChanceIncrease;
        }
    }

    private void OnCollectedWrongIngredient(string wrongIngredientRitual)
    {
        if (randomRitual != null && wrongIngredientRitual == randomRitual)
        {
            manager.LockRitual(randomRitual);
            randomRitual = null;
            PlayerEventHandler.Instance.EmptyCauldron();
        }
    }

    private void OnUnlockedRitual(string ritual)
    {
        if (randomRitual != null && ritual == randomRitual)
        {
            manager.AddRitualToUnlocked(randomRitual);
            randomRitual = null;
            PlayerEventHandler.Instance.EmptyCauldron();
        } 
        else OnCollectedWrongIngredient(ritual);
    }
}
