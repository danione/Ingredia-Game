using System.Collections.Generic;

public class ScoreRecipe : BaseRecipe
{
    public ScoreRecipe()
    {
        name = "Score Recipe";
        ingredients = new Dictionary<string, int> { { "Ingredient (1)(Clone)", 15 }, { "Ingredient (2)(Clone)", 15 } };
        rewards = "Reward Placeholder";
    }
}
