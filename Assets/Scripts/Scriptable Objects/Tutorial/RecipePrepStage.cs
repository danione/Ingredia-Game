using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Recipe Stage")]
public class RecipePrepStage : TutorialStage
{
    public override void InitiateStage()
    {
        TutorialManager.instance.DisableIngredientsFactory();
        TutorialManager.instance.EnableRecipeFactory();
    }

    public override void NextStage()
    {

    }

    public override void Reward()
    {
        TutorialManager.instance.EnableRecipeFactory();
    }
}
