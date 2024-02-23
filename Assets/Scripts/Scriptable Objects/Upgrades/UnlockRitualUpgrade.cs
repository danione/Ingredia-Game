using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Ritual Unlock", menuName = "Scriptable Objects/Upgrades/Ritual Unlock")]

public class UnlockRitualUpgrade : UpgradeData
{
    public RitualScriptableObject ritualToUnlock;

    public override void ApplyUpgrade(GameObject obj)
    {
        GameManager.Instance.GetComponent<RitualManager>().AddRitualToUnlocked(ritualToUnlock.ritualName);
    }
}
