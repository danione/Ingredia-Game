public static class RecipeFactory
{
    public static BaseRecipe CreateRecipe(RecipeType type)
    {
        switch (type)
        {
            case RecipeType.Score: return new ScoreRecipe(); 
            case RecipeType.Speed: return new SwiftRecipe();
            default: return null;
        }
    }
}
