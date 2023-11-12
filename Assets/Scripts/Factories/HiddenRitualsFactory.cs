using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRitualsFactory : MonoBehaviour
{
    [SerializeField] private Transform recipeObject;
    [SerializeField] private SpawnFrequencyData spawnFrequencyData;
    [SerializeField] private SpawnLocationData spawnLocationData;

    private HiddenRitual selectedRitual = null;
    private UnorderedRecipe recipe = null;
    private float currentSpawnChance;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnChance = spawnFrequencyData.spawnChance;
        StartCoroutine(SpawnHiddenRitual());
        PlayerEventHandler.Instance.UnlockRitual += OnUnlockedRitual;
        PlayerEventHandler.Instance.FailRitual += OnFailedRitual;
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
        if (!manager.HasLockedHiddenRituals() || selectedRitual != null) { return; } 
        
        if(Random.Range(0f, 1f) < currentSpawnChance)
        {
            Vector3 position = new(Random.Range(spawnLocationData.xRightMax, spawnLocationData.xLeftMax), spawnLocationData.yLocation, 2);
            Instantiate(recipeObject, position, Quaternion.identity);
            selectedRitual = manager.GetRandomLockedRitual();
            recipe = new UnorderedRecipe(manager.GetIngredientsOfRitual(selectedRitual.ritual));
            selectedRitual.TemporaryEnable();
            RecipeUIManager.Instance.Activate(recipe);
        }
        else
        {
            currentSpawnChance += spawnFrequencyData.spawnChanceIncrease;
        }
    }

    private void OnUnlockedRitual()
    {
        selectedRitual.Unlock();
        OnFailedRitual();
    }

    private void OnFailedRitual()
    {
        recipe = null;
        selectedRitual = null;
    }


}
