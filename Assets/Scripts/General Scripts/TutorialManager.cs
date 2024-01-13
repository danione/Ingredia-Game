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
    public static TutorialManager instance;

    private List<Action> sections = new();
    private IngredientsFactory ingredientsFactory;
    private EnemyFactory enemyFactory;
    private GoldenNuggetsFactory goldenNuggets;

    private void Start()
    {
        if(instance == null) {  instance = this; } else { Destroy(this); }

        InputEventHandler.instance.PlayerMoved += OnPlayerMoved;
        PlayerEventHandler.Instance.EmptiedCauldron += OnEmptiedCauldron;
        InputEventHandler.instance.UsedPotion += OnPotionUse;
        GameEventHandler.Instance.DestroyedEnemy = OnEnemyDestroyed;
        PlayerInputHandler.permissions.LockAll();

        ingredientsFactory = spawnManager.GetComponent<IngredientsFactory>();
        enemyFactory = spawnManager.GetComponent<EnemyFactory>();
        goldenNuggets = spawnManager.GetComponent<GoldenNuggetsFactory>();

        InitialiseNextStage();

        /*
        sections.Add(MovementStage);
        sections.Add(IngredientStage);
        sections.Add(EmptyCauldron);
        sections.Add(FirstRitual);
        sections.Add(FirstPotion);
        sections.Add(SpawnBat);

        for(int i = 0; i < currentStage; i++)
        {
            sections[i].Invoke();
        }*/
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

    // Stage 2
    private void EmptyCauldron()
    {
        PlayerInputHandler.permissions.canEmptyCauldron = true;

    }

    // Move from stage 2 to next
    private void OnEmptiedCauldron()
    {
        StartCoroutine(CooldownBetweenStages());
        PlayerEventHandler.Instance.EmptiedCauldron -= OnEmptiedCauldron;

    }

    // Stage 3
    private void FirstRitual()
    {
        ingredientsFactory.AppendARegularIngredient(eyeData);
        PlayerInputHandler.permissions.canPerformRituals = true;
        GameManager.Instance.gameObject.GetComponent<RitualManager>().enabled = true;
        uiManager.gameObject.SetActive(true);
        StartCoroutine(CooldownBetweenStages());
    }

    // Stage 4
    private void FirstPotion()
    {
        PlayerInputHandler.permissions.canUsePotions = true;
    }

    private void OnPotionUse()
    {
        StartCoroutine(CooldownBetweenStages());
        InputEventHandler.instance.UsedPotion -= OnPotionUse;
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
        StartCoroutine(CooldownBetweenStages());
    }

    private void InitialiseNextStage()
    {
        if(currentStage >= tutorialStages.Count) return;

        tutorialStages[currentStage].InitiateStage();
    }


}
