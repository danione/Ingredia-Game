using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Ritual Yield Upgrade", menuName = "Scriptable Objects/Upgrades/Ritual Yield Upgrade")]
public class RitualYieldUpgrade : UpgradeData
{
    public int upgradeLevel;
    public override void ApplyUpgrade(GameObject obj)
    {
        Constants.Instance.UpgradeCurrentRitualRewards(upgradeLevel);
    }
}
