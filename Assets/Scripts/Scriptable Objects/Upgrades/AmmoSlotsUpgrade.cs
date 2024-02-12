using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Ammo Slot Upgrade", menuName = "Scriptable Objects/Upgrades/Ammo Slot Upgrade")]
public class AmmoSlotsUpgrade : UpgradeData
{
    public int fireballMaxAmmo;
    public int athameMaxAmmo;

    public override void ApplyUpgrade(GameObject obj)
    {
        PlayerInventory inventory = obj.GetComponent<PlayerInventory>();
        inventory.SetMaxAmmo("Athame", athameMaxAmmo);
        inventory.SetMaxAmmo("Fireball", fireballMaxAmmo);
    }
}
