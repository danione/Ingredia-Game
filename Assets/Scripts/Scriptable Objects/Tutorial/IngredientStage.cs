using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Ingredient Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Ingredient Stage")]
public class IngredientStage : TutorialStage
{
    public override void InitiateStage()
    {
        PlayerEventHandler.Instance.CollectedIngredient += TutorialManager.instance.OnCollectedIngredient;
        TutorialManager.instance.EnableIngredientsFactory();
    }

    public override void NextStage()
    {
        PlayerEventHandler.Instance.CollectedIngredient -= TutorialManager.instance.OnCollectedIngredient;
        TutorialManager.instance.DisableIngredientsFactory();
    }

    public override void Reward()
    {
        TutorialManager.instance.DisableIngredientsFactory();
    }
}
