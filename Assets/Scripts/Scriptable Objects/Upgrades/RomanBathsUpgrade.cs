using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Roman Baths Upgrade", menuName = "Scriptable Objects/Upgrades/Roman Baths Upgrade")]
public class RomanBathsUpgrade : UpgradeData
{
    public float areaAmount;
    public int damageAmount;
    public RitualScriptableObject newRitualYield;

    public override void ApplyUpgrade(GameObject obj)
    {
        SteamballProjectile projectile = obj.GetComponent<SteamballProjectile>();
        projectile.SetAreaOfEffect(areaAmount);
        projectile.ChangeStrength(damageAmount);
        GameManager.Instance.GetComponent<RitualManager>().ChangeYieldByIncrementing(newRitualYield.ritualName);
    }
}
