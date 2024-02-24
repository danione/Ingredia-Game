using System.Collections.Generic;

public interface IRitual
{
    public RitualScriptableObject RitualData { get; set; }
    public bool IsAvailable { get; }
    public void OnCauldronEmptied();
    public void OnIngredientCollected(IIngredient ingredient, int amount);
    public PotionsData GetPotionReward();
}
