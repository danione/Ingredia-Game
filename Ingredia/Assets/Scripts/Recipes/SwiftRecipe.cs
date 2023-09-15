using System.Collections.Generic;

public class SwiftRecipe : BaseRecipe
{
    public SwiftRecipe()
    {
        name = "Swift Recipe";
        ingredients = new Dictionary<string, int> { { "Ingredient (1)(Clone)", 2 }, { "Ingredient (2)(Clone)", 1 } };
        rewards = "Reward Placeholder";
    }
}
