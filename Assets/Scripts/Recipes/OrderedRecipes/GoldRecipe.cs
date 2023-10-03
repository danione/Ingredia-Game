using System.Collections.Generic;

public class GoldRecipe : OrderedRecipe
{
    public override void Init()
    {
        List<IRecipeAction> recipes = new()
        {
            new CollectItemRecipeAction("water", 3),
            new CollectItemRecipeAction("fishbones", 5),
            new CollectItemRecipeAction("pumpkin", 3)
        };

        actionContainer.AddRange(recipes);

        ListenRecipes();
    }
/*
    protected override void SetProbability()
    {
        probability = 0.5f;
    }
*/
}
