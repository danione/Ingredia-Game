public class InventoryUIManager : UIObjectsAbstract
{
    public override void Init()
    {
        base.Init();
        PlayerEventHandler.Instance.CollectedIngredient += OnAdjustInventoryUI;
        PlayerEventHandler.Instance.EmptiedCauldron += RemoveAllInventoryItems;
    }

    private void OnDestroy()
    {
        try
        {
            PlayerEventHandler.Instance.CollectedIngredient -= OnAdjustInventoryUI;
            PlayerEventHandler.Instance.EmptiedCauldron -= RemoveAllInventoryItems;
        }
        catch { }
    }
}
