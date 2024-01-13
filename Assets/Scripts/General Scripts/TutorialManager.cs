using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private int currentStage = 0;
    [SerializeField] private int stageJump = 0;
    [SerializeField] private float playerMovementThreshold;
    [SerializeField] private float cooldownBetweenStages;
    [SerializeField] private GameObject spawnManager;
    [SerializeField] private IngredientData eyeData;
    [SerializeField] private GameObject uiManager;
    [SerializeField] private List<TutorialStage> tutorialStages = new();
    [SerializeField] private TutorialManagerUI tutorialUiManager;
   
    public static TutorialManager instance;
    private bool emptied = false;

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

        for (int i = 0; i < stageJump; i++)
        {
            ExecuteCurrentStage();
        }
        
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
        ExecuteCurrentStage();
    }

    public void OnPotionUse()
    {
        ExecuteCurrentStage();
    }

    // Stage 5
    private void SpawnBat()
    {
        enemyFactory.enabled = true;
        goldenNuggets.enabled = true;
    }

    private void OnEnemyDestroyed(Vector3 pos)
    {
        enemyFactory.SetTutorialCurrency(-100);
        
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
