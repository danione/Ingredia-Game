using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tutorial Upgrade", menuName = "Scriptable Objects/Upgrades/Tutorial Upgrade")]

public class TutorialFireballUpgrade : UpgradeData
{
    public override void ApplyUpgrade(GameObject obj)
    {
        TutorialManager.instance.OnUpgraded();
    }
}
