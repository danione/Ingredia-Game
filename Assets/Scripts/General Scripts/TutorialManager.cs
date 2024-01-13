using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private int currentStage = 0;
    [SerializeField] private float playerMovementThreshold;
    [SerializeField] private float cooldownBetweenStages;
    [SerializeField] private GameObject spawnManager;
    [SerializeField] private IngredientData eyeData;
    [SerializeField] private GameObject uiManager;
    [SerializeField] private List<TutorialStage> tutorialStages = new();
    [SerializeField] private TutorialManagerUI tutorialUiManager;
    [SerializeField] private OverloadElixirData overloadElixirData;
    [SerializeField] private GameObject conjuredWall;
    [SerializeField] private GameObject laserStarterPosition;
   
    public static TutorialManager instance;
    private bool emptied = false;
    private bool hasNotUsed = false;
    private bool performed = false;

    private IngredientsFactory ingredientsFactory;
    private EnemyFactory enemyFactory;
    private GoldenNuggetsFactory goldenNuggets;

    private void Start()
    {
        if(instance == null) {  instance = this; } else { Destroy(this); }

        PlayerInputHandler.permissions.LockAll();

        ingredientsFactory = spawnManager.GetComponent<IngredientsFactory>();
        enemyFactory = spawnManager.GetComponent<EnemyFactory>();
        goldenNuggets = spawnManager.GetComponent<GoldenNuggetsFactory>();

        InitialiseNextStage();       
    }

    public void OnPlayerMoved(float direction)
    {
        playerMovementThreshold -= Time.deltaTime;
        if(playerMovementThreshold < 0)
        {
            ExecuteCurrentStage();
            StartCoroutine(CooldownBetweenStages());
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
    }

    public void OnEmptiedCauldron()
    {
        if (emptied) return;

        emptied = true;
        ExecuteCurrentStage();

    }

    public void FirstRitual()
    {
        ingredientsFactory.AppendARegularIngredient(eyeData);
        uiManager.SetActive(true);
    }

    public void OnPerformedFirstRitual()
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
        enemyFactory.SetTutorialCurrency(-100);
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
        goldenNuggets.enabled = true;
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

    public void OnUpgraded()
    {
        ExecuteCurrentStage();
    }

    public void UnlockSlips()
    {
        GameManager.Instance.gameObject.GetComponent<ScrollSlipManager>().enabled = true;
        ingredientsFactory.gameObject.GetComponent<ScrollSlipFactory>().enabled = true;


    }

    public void ExecuteCurrentStage()
    {
        if (currentStage >= tutorialStages.Count) return;
        tutorialStages[currentStage++].NextStage();
        tutorialUiManager.DisplayCongrats();

        Debug.Log(currentStage);
        StartCoroutine(CooldownBetweenStages());
    }

    private void InitialiseNextStage()
    {
        if(currentStage >= tutorialStages.Count) return;
        tutorialStages[currentStage].InitiateStage();
        tutorialUiManager.DisplayFromTutorialStage(tutorialStages[currentStage]);
    }
}
