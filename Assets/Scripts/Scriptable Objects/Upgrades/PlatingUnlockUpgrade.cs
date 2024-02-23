using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Plating Unlock Upgrade", menuName = "Scriptable Objects/Upgrades/Plating Unlock Upgrade")]

public class PlatingUnlockUpgrade : UpgradeData
{
    public override void ApplyUpgrade(GameObject obj)
    {
        PlayerController.Instance.GetComponent<PlayerStats>().EnableArmour();
    }
}
