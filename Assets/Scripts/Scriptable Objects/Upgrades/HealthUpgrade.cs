using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Upgrade", menuName = "Scriptable Objects/Upgrades/Health Upgrade")]
public class HealthUpgrade : UpgradeData
{
    public int newMaxHealth;

    public override void ApplyUpgrade(GameObject obj)
    {
        obj.GetComponent<PlayerStats>().UpgradeHealth(newMaxHealth);
    }
}
