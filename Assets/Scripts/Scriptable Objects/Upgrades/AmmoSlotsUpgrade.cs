using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Ammo Slot Upgrade", menuName = "Scriptable Objects/Upgrades/Ammo Slot Upgrade")]
public class AmmoSlotsUpgrade : UpgradeData
{
    public int fireMaxBombAmmo;
    public int knifeMaxAmmo;

    public override void ApplyUpgrade(GameObject obj)
    {
        PlayerInventory inventory = obj.GetComponent<PlayerInventory>();
        inventory.SetMaxAmmo(fireMaxBombAmmo, knifeMaxAmmo);
    }
}
