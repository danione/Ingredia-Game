using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeRecipe : UnorderedRecipe
{

    public override void Init()
    {
        List<IRecipeAction> actions = new()
        {
            new CollectItemRecipeAction("water", 4),
            new CollectItemRecipeAction("pumpkin", 2),
            new CollectItemRecipeAction("fishbones", 2)
        };

        actionContainer.AddRange(actions);

        AddAllActions();
    }
/*
    protected override void SetProbability()
    {
        probability = 0.1f;
    }
*/
}
