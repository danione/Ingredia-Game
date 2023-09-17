using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRecipe : UnorderedRecipe
{
    public override void Init(PlayerInventory inventory)
    {
        List<IRecipeAction> actions = new()
        { 
            new CollectItemRecipeAction("water", 4, inventory),
            new CollectItemRecipeAction("pumpkin", 2, inventory)
        };

        actionContainer.AddRange(actions);

        AddAllActions();
    }
}
