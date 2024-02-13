using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Coating Upgrade", menuName = "Scriptable Objects/Upgrades/Health Upgrade")]
public class HealthUpgrade : UpgradeData
{
    public int newMaxHealth;

    public override void ApplyUpgrade(GameObject obj)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().UpgradeHealth(newMaxHealth);
    }
}
