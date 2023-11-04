using System.Collections.Generic;

public interface IRitual
{
    public RitualScriptableObject ScriptableObject { get; }
    public IReward Reward { get; }
    public bool IsAvailable { get; }
    public void OnCauldronEmptied();
    public void OnIngredientCollected(IIngredient ingredient, int amount);
    public bool AvailableRitual();
}
