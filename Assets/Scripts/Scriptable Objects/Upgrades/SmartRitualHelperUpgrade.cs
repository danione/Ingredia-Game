using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Smart Ritual Helper Upgrade", menuName = "Scriptable Objects/Upgrades/Smart Ritual Helper Upgrade")]
public class SmartRitualHelperUpgrade : UpgradeData
{
    public override void ApplyUpgrade(GameObject obj)
    {
        GameEventHandler.Instance.ActivateSmartRitualHelper();
    }
}
