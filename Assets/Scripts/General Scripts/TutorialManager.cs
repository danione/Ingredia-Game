using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private int currentStage = 0;
    [SerializeField] private float playerMovementThreshold;
    [SerializeField] private float cooldownBetweenStages;
    [SerializeField] private GameObject spawnManager;
    [SerializeField] private IngredientData athameData;
    [SerializeField] private IngredientData eyeData;
    [SerializeField] private IngredientData fireData;
    [SerializeField] private GameObject inventorySlotsUI;
    [SerializeField] private GameObject sophisticationUI;
    [SerializeField] private List<TutorialStage> tutorialStages = new();
    [SerializeField] private TutorialManagerUI tutorialUiManager;
    [SerializeField] private OverloadElixirData overloadElixirData;
    [SerializeField] private GameObject conjuredWall;
    [SerializeField] private GameObject laserStarterPosition;
    [SerializeField] private GameObject scrollSlip;
    [SerializeField] private string postTutorialSceneName;
    [SerializeField] private float tickUntilNextScene;
    [SerializeField] private GameObject SwapUI;
    [SerializeField] private int forwardToStage = 0;


    public static TutorialManager instance;
    private bool emptied = false;
    private bool hasNotUsed = false;
    private bool performed = false;
    private bool swapped = false;
    private bool openedScroll = false;

    private IngredientsFactory ingredientsFactory;
    private EnemyFactory enemyFactory;

    private void Start()
    {
        if (instance == null) { instance = this; } else { Destroy(this); }

        ingredientsFactory = spawnManager.GetComponent<IngredientsFactory>();
        enemyFactory = spawnManager.GetComponent<EnemyFactory>();
        StartCoroutine(WaitForInitialisationOfObjects());
        GameManager.Instance.tutorialMode = true;
        
    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.SetsNormalMode();
        GameManager.Instance.tutorialMode = false;

    }

    private IEnumerator WaitForInitialisationOfObjects()
    {
        yield return new WaitForEndOfFrame();

        GameEventHandler.Instance.SetsTutorialMode();


        for (int i = 0; i < forwardToStage; i++)
        {
            tutorialStages[i].Reward();
        }
        currentStage = forwardToStage;

        InitialiseNextStage();
    }

    public void OnPlayerMoved(float direction)
    {
        playerMovementThreshold -= Time.deltaTime;
        if(playerMovementThreshold < 0)
        {
            ExecuteCurrentStage();
        }
    }

    IEnumerator CooldownBetweenStages()
    {
        yield return new WaitForSeconds(cooldownBetweenStages);
        InitialiseNextStage();
    }

    public void EnableIngredientsFactory()
    {
        ingredientsFactory.enabled = true;
        ingredientsFactory.SetSpawning(true);
    }

    public void OnEmptiedCauldron()
    {
        if (emptied) return;

        emptied = true;
        ExecuteCurrentStage();
    }

    public void RemoveOneSpawnIngredient()
    {
        GameManager.Instance.GetComponent<IngredientManager>()?.RemoveOneIngredient();
    }

    public void OnCollectedIngredient(IIngredient ingredient, int count)
    {
        if(count > 2)
        {
            ExecuteCurrentStage();
        }
    }

    public void DisableIngredientsFactory()
    {
        ingredientsFactory.SetSpawning(false);
    }

    public void FirstRitual()
    {
        GameManager.Instance.GetComponent<IngredientManager>()?.UnlockIngredient(eyeData);
        GameManager.Instance.GetComponent<IngredientManager>()?.UnlockIngredient(athameData);
        inventorySlotsUI.SetActive(true);
        sophisticationUI.SetActive(true);
    }

    public void OnPerformedFirstRitual(IRitual ritual)
    {
        if(performed) return;

        performed = true;
        ExecuteCurrentStage();
    }

    public void OnPotionUse()
    {
        if (hasNotUsed) return;

        hasNotUsed = true;
        ExecuteCurrentStage();
    }

    public void SpawnBat()
    {
        enemyFactory.enabled = true;
    }

    public void OnEnemyDestroyed(Vector3 pos)
    {
        enemyFactory.SetTutorialCurrency();
        ExecuteCurrentStage();
    }

    public void OnWallDestroyed(Vector3 pos)
    {
        if(conjuredWall.transform.childCount == 1)
        {
            ExecuteCurrentStage();
        }
    }

    public void AddLaserBeam()
    {
        PlayerController.Instance.inventory.AddPotion(overloadElixirData);
        conjuredWall.SetActive(true);
    }

    public void Collided()
    {
        ExecuteCurrentStage();
    }

    public void LaserPreparation()
    {
        laserStarterPosition.SetActive(true);
    }

    public void OnUpgraded(List<RitualRecipe> recipe)
    {
        IngredientManager ingreMan = GameManager.Instance.GetComponent<IngredientManager>();
        foreach(var ingredient in recipe)
        {
            ingreMan.UnlockIngredientDefault(ingredient.item);
        }
        ExecuteCurrentStage();
    }

    public void EnableSwapUI()
    {
        SwapUI.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void EnableAmmoUI()
    {
        SwapUI.SetActive(true);
    }

    public void OnSwappedProjectiles()
    {
        if (swapped) return;

        swapped = true;
        ExecuteCurrentStage();
    }

    public void ScrollGenerate()
    {
        if(scrollSlip != null)
        {
            scrollSlip.gameObject.SetActive(true);
            GameManager.Instance.gameObject.GetComponent<ScrollSlipManager>().enabled = true;
        }
    }

    public void OnScrollMenuOpened()
    {
        if (openedScroll) return;

        openedScroll = true;
        ExecuteCurrentStage() ;
    }

    public void TransitionOnly()
    {
        StartCoroutine(TransitionFunction());
    }

    IEnumerator TransitionFunction()
    {
        yield return new WaitForSeconds(tickUntilNextScene);
        ExecuteCurrentStage();
    }

    public void ExitTutorial()
    {
        try
        {
            SceneManager.LoadScene(postTutorialSceneName);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
       
    }

    public void ExecuteCurrentStage()
    {
        if (currentStage >= tutorialStages.Count) return;
        tutorialStages[currentStage++].NextStage();
        tutorialUiManager.DisplayCongrats();

        StartCoroutine(CooldownBetweenStages());
    }

    private void InitialiseNextStage()
    {
        if(currentStage >= tutorialStages.Count) return;
        tutorialStages[currentStage].InitiateStage();
        tutorialUiManager.DisplayFromTutorialStage(tutorialStages[currentStage]);
    }
}
