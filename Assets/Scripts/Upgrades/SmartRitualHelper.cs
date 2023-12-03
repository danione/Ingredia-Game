using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartRitualHelper : MonoBehaviour
{
    private bool isActive;
    private Ritual easiestRitualNow = null;

    private void Start()
    {
        isActive = false;
        GameEventHandler.Instance.ActivatedSmartRitualHelper += OnActivate;
        GameEventHandler.Instance.CollectedExistingIngredient += OnCollectedExistingIngredient;
        PlayerEventHandler.Instance.EmptiedCauldron += ResetOrNoAvailableRitual;
    }

    private void OnActivate()
    {
        isActive = true;
    }

    private void OnCollectedExistingIngredient(Ritual ritual)
    {
        if(easiestRitualNow == null || ritual.Difficulty < easiestRitualNow.Difficulty)
        {
            easiestRitualNow = ritual;
        }
        
        if(easiestRitualNow.Difficulty == float.PositiveInfinity)
        {
            ResetOrNoAvailableRitual();
        }

        HighlightIngredients();
    }

    private void ResetOrNoAvailableRitual()
    {
        easiestRitualNow = null;
    }

    private void HighlightIngredients()
    {
        if (easiestRitualNow == null) return;

        easiestRitualNow.HighlightIngredients();
    }
}
