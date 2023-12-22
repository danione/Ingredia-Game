using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
   // [SerializeField] private int currentStage = 1;
    [SerializeField] private float playerMovementThreshold;
    [SerializeField] private float cooldownBetweenStages;
    [SerializeField] private GameObject spawnManager;
    [SerializeField] private IngredientData eyeData;

    private List<Action> sections = new();
    private int currentStage = 0;
    private IngredientsFactory ingredientsFactory;

    private void Start()
    {
        InputEventHandler.instance.PlayerMoved += OnPlayerMoved;
        PlayerEventHandler.Instance.EmptiedCauldron += OnEmptiedCauldron;
        ingredientsFactory = spawnManager.GetComponent<IngredientsFactory>();
        sections.Add(MovementStage);
        sections.Add(IngredientStage);
        sections.Add(EmptyCauldron);
        sections.Add(FirstRitual);
    }

    private void OnPlayerMoved(float direction)
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
        ExecuteCurrentStage();
    }

    private void MovementStage()
    {
        PlayerInputHandler.permissions.canMove = true;
        InputEventHandler.instance.PlayerMoved -= OnPlayerMoved;
    }

    private void IngredientStage()
    {
        ingredientsFactory.enabled = true;
        StartCoroutine(CooldownBetweenStages());
    }

    private void EmptyCauldron()
    {
        PlayerInputHandler.permissions.canEmptyCauldron = true;
    }

    private void OnEmptiedCauldron()
    {
        ExecuteCurrentStage();
    }

    private void FirstRitual()
    {
        ingredientsFactory.AppendARegularIngredient(eyeData);
        PlayerInputHandler.permissions.canPerformRituals = true;
    }

    private void ExecuteCurrentStage()
    {
        if (currentStage >= sections.Count) return;

        sections[currentStage]?.Invoke();
        currentStage++;
    }


}
