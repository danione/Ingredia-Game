using System.Collections;
using UnityEngine;

public class RecipeFactory : MonoBehaviour
{
   // [SerializeField] private SpawnFrequencyData spawnFrequencyData;
    //[SerializeField] private SpawnLocationData spawnLocationData;
    //[SerializeField] private ObjectsSpawner spawner;


    // Start is called before the first frame update
    void Start()
    {
        PlayerEventHandler.Instance.CollectedInvalidIngredient += OnCollectedWrongIngredient;
        PlayerEventHandler.Instance.CollidedWithRecipe += OnCollidedWithARecipeObject;
    }

    private void OnDestroy()
    {
        try
        {
            PlayerEventHandler.Instance.CollectedInvalidIngredient -= OnCollectedWrongIngredient;
            PlayerEventHandler.Instance.CollidedWithRecipe -= OnCollidedWithARecipeObject;
        }
        catch { }
    }

    private void OnCollidedWithARecipeObject()
    {
        /*
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
        PlayerEventHandler.Instance.SetUpHiddenRitual(manager.GetRitualScriptableObject(randomRitual));*/
        Debug.Log("TBA");
    }

    private void OnCollectedWrongIngredient(string wrongIngredientRitual)
    {
        /*
        if (randomRitual != null && wrongIngredientRitual == randomRitual)
        {
            OnEmptiedCauldron();
            PlayerEventHandler.Instance.EmptyCauldron();
        }
        */
    }
}
