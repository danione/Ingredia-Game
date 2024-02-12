using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Full Recipe Stage")]
public class RecipeStage : TutorialStage
{
    public override void InitiateStage()
    {
        GameEventHandler.Instance.CompletedRecipe += TutorialManager.instance.OnCompletedRecipe;
    }

    public override void NextStage()
    {
        GameEventHandler.Instance.CompletedRecipe -= TutorialManager.instance.OnCompletedRecipe;
        TutorialManager.instance.DisableIngredientsFactory();
        TutorialManager.instance.DisableRecipeFactory();
    }

    public override void Reward()
    {
        TutorialManager.instance.DisableIngredientsFactory();
        TutorialManager.instance.DisableRecipeFactory();

    }
}
