using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Manual Heal Upgrade", menuName = "Scriptable Objects/Upgrades/Manual Heal Upgrade")]
public class HealUpgrade : UpgradeData
{
    public int amount;

    public override void ApplyUpgrade(GameObject obj)
    {
        PlayerStats stats = obj.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.Heal(amount);
        }
    }
}
