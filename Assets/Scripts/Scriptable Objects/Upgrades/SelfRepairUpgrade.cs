using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Self-Repair Upgrade", menuName = "Scriptable Objects/Upgrades/Self Repair Upgrade")]
public class SelfRepairUpgrade : UpgradeData
{
    public override void ApplyUpgrade(GameObject obj)
    {
        PlayerController.Instance.GetComponent<PlayerStats>().SetPermanentHealing();
    }
}
