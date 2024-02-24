using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Full Recipe Stage")]
public class RecipeStage : TutorialStage
{
    public override void InitiateStage()
    {
        TutorialManager.instance.EnableIngredientsFactory();
        GameEventHandler.Instance.CompletedRecipe += TutorialManager.instance.OnCompletedRecipe;
    }

    public override void NextStage()
    {
        GameEventHandler.Instance.CompletedRecipe -= TutorialManager.instance.OnCompletedRecipe;
        TutorialManager.instance.DisableIngredientsFactory();
    }

    public override void Reward()
    {
        TutorialManager.instance.DisableIngredientsFactory();

    }
}