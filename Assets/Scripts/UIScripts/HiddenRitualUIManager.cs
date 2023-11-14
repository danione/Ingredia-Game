using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRitualUIManager : UIManager
{
    public override void Init()
    {
        base.Init();
        PlayerEventHandler.Instance.CollectedIngredient += OnAdjustInventoryUIRitual;
        PlayerEventHandler.Instance.EmptiedCauldron += RemoveAllInventoryItems;
        PlayerEventHandler.Instance.SetUpUIRitualInterface += OnSetupHiddenRitual;
    }


    public void OnSetupHiddenRitual(RitualScriptableObject ritualData)
    {
        foreach(var item in ritualData.ritualRecipes)
        {
            OnAdjustInventoryUI(item.item, item.amount);
        }
    }
}
