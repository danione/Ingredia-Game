using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRitualsFactory : MonoBehaviour
{
    [SerializeField] private SpawnFrequencyData spawnFrequencyData;
    [SerializeField] private SpawnLocationData spawnLocationData;
    [SerializeField] private ObjectsSpawner spawner;
    private ScrollSlipManager scrollSlipManager;

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
        PlayerEventHandler.Instance.CollidedWithRecipe += OnCollidedWithARecipeObject;
        PlayerEventHandler.Instance.EmptiedCauldron += OnEmptiedCauldron;
        scrollSlipManager = GameManager.Instance.GetComponent<ScrollSlipManager>();
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
            spawner._pool.Get().gameObject.transform.position = position;
            currentSpawnChance = spawnFrequencyData.spawnChance;
        }
        else
        {
            currentSpawnChance += spawnFrequencyData.spawnChanceIncrease;
        }
    }

    private void OnCollidedWithARecipeObject()
    {
        randomRitual = manager.GetRandomLockedRitual();
        manager.UnlockRitual(randomRitual);
        PlayerEventHandler.Instance.SetUpHiddenRitual(manager.GetRitualScriptableObject(randomRitual));
    }

    private void OnCollectedWrongIngredient(string wrongIngredientRitual)
    {
        if (randomRitual != null && wrongIngredientRitual == randomRitual)
        {
            OnEmptiedCauldron();
            PlayerEventHandler.Instance.EmptyCauldron();
        }
    }

    private void OnUnlockedRitual(string ritual)
    {
        if (randomRitual != null && ritual == randomRitual)
        {
            RitualScriptableObject ritualScriptableObject = manager.AddRitualToUnlocked(randomRitual);
            scrollSlipManager.AddRitual(ritualScriptableObject);
            PlayerController.Instance.inventory.AddPotion(ritualScriptableObject.potionRewardData[0]);
            randomRitual = null;
            PlayerEventHandler.Instance.EmptyCauldron();
        } 
        else OnCollectedWrongIngredient(ritual);
    }

    private void OnEmptiedCauldron()
    {
        if(randomRitual != null)
            manager.LockRitual(randomRitual);
        randomRitual = null;
    }
}
