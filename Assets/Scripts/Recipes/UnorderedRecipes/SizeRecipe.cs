using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeRecipe : UnorderedRecipe
{
    public override void Init(PlayerInventory inventory)
    {
        List<IRecipeAction> actions = new()
        {
            new CollectItemRecipeAction("water", 4, inventory),
            new CollectItemRecipeAction("pumpkin", 2, inventory),
            new CollectItemRecipeAction("fishbones", 2, inventory)
        };

        actionContainer.AddRange(actions);

        AddAllActions();
    }
}
