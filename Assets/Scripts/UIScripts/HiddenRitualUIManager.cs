using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRitualUIManager : UIObjectsAbstract
{
    public override void Init()
    {
        base.Init();
        PlayerEventHandler.Instance.CollectedIngredient += OnAdjustInventoryUIRitual;
        PlayerEventHandler.Instance.EmptiedCauldron += RemoveAllInventoryItems;
        PlayerEventHandler.Instance.SetUpUIRitualInterface += OnSetupHiddenRitual;
    }

    private void OnDestroy()
    {
        try
        {
            PlayerEventHandler.Instance.CollectedIngredient -= OnAdjustInventoryUIRitual;
            PlayerEventHandler.Instance.EmptiedCauldron -= RemoveAllInventoryItems;
            PlayerEventHandler.Instance.SetUpUIRitualInterface -= OnSetupHiddenRitual;
        }
        catch { }
    }


    public void OnSetupHiddenRitual(RitualScriptableObject ritualData)
    {
        foreach(var item in ritualData.ritualRecipes)
        {
            OnAdjustInventoryUI(item.item.ingredientName, item.amount);
        }
    }
}
