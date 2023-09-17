using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldRecipe : OrderedRecipe
{
    public override void Init(PlayerInventory inventory)
    {
        List<IRecipeAction> recipes = new()
        {
            new CollectItemRecipeAction("water", 3, inventory),
            new CollectItemRecipeAction("fishbones", 5, inventory),
            new CollectItemRecipeAction("pumpkin", 3, inventory)
        };

        actionContainer.AddRange(recipes);

        ListenRecipes();
    }
}
