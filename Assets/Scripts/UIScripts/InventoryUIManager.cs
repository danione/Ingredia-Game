using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : UIObjectsAbstract
{
    public override void Init()
    {
        base.Init();
        PlayerEventHandler.Instance.CollectedIngredient += OnAdjustInventoryUI;
        PlayerEventHandler.Instance.EmptiedCauldron += RemoveAllInventoryItems;
    }
}
