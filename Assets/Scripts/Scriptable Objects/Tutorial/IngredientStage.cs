using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Ingredient Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Ingredient Stage")]
public class IngredientStage : TutorialStage
{
    public override void InitiateStage()
    {
        TutorialManager.instance.TransitionOnly();
    }

    public override void NextStage()
    {
        TutorialManager.instance.EnableIngredientsFactory();
    }

    public override void Reward()
    {
        TutorialManager.instance.EnableIngredientsFactory();
    }
}
