using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tutorial Upgrade", menuName = "Scriptable Objects/Upgrades/Tutorial Upgrade")]

public class TutorialSteamballUpgrade : UpgradeData
{
    public RitualScriptableObject ritualToUnlock;

    public override void ApplyUpgrade(GameObject obj)
    {
        GameManager.Instance.GetComponent<RitualManager>().AddRitualToUnlocked(ritualToUnlock.ritualName);
        TutorialManager.instance.OnUpgraded();
    }
}
