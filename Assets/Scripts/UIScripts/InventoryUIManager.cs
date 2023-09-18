using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : UIManager
{
    [SerializeField] private PlayerInventory inventory;
    public override void Init()
    {
        base.Init();
        inventory.CollectedIngredient += OnAdjustInventoryUI;
    }

    private void OnDestroy()
    {
        inventory.CollectedIngredient -= OnAdjustInventoryUI;
    }
}
