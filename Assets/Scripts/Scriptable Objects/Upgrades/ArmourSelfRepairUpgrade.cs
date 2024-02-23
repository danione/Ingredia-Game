using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Armour Repair Upgrade", menuName = "Scriptable Objects/Upgrades/Self-Repair Armour")]
public class ArmourSelfRepairUpgrade : UpgradeData
{
    public override void ApplyUpgrade(GameObject obj)
    {
        PlayerController.Instance.GetComponent<PlayerStats>().SetAutoRegenArmour();
    }
}
