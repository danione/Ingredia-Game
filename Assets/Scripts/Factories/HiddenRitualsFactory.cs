using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool shouldGenerate = true;

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

    private void OnDestroy()
    {
        try
        {
            PlayerEventHandler.Instance.CollectedInvalidIngredient -= OnCollectedWrongIngredient;
            PlayerEventHandler.Instance.UnlockedRitual -= OnUnlockedRitual;
            PlayerEventHandler.Instance.CollidedWithRecipe -= OnCollidedWithARecipeObject;
            PlayerEventHandler.Instance.EmptiedCauldron -= OnEmptiedCauldron;
        }
        catch { }
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

    public void SetShouldGenerate(bool newCommand)
    {
        shouldGenerate = newCommand;
    }

    private void GenerateNewRitual()
    {
        if (!manager.HasLockedHiddenRituals() || randomRitual != null || shouldGenerate == false) { return; } 
        
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
        PlayerEventHandler.Instance.EmptyCauldron();
        if (GameManager.Instance.tutorialMode)
        {
            randomRitual = manager.GetFirstLockedRitual();
        }
        else
        {
            randomRitual = manager.GetRandomLockedRitual();
        }
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
            if(!GameManager.Instance.tutorialMode)
                PlayerController.Instance.inventory.AddPotion(ritualScriptableObject.potionRewardData[0]);
            randomRitual = null;
            PlayerEventHandler.Instance.EmptyCauldron();
            GameEventHandler.Instance.CompleteRecipe();
        } 
        else OnCollectedWrongIngredient(ritual);
    }

    private void OnEmptiedCauldron()
    {
        if (randomRitual != null)
            manager.LockRitual(randomRitual);
        randomRitual = null;
    }
}
