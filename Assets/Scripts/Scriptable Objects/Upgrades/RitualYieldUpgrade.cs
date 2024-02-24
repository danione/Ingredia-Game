using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Ritual Yield Upgrade", menuName = "Scriptable Objects/Upgrades/Ritual Yield Upgrade")]
public class RitualYieldUpgrade : UpgradeData
{
    public RitualScriptableObject ritual;
    public override void ApplyUpgrade(GameObject obj)
    {
        GameManager.Instance.GetComponent<RitualManager>().ChangeYieldByIncrementing(ritual.ritualName);
    }
}
