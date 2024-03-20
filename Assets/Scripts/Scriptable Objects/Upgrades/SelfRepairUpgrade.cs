using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Self-Repair Upgrade", menuName = "Scriptable Objects/Upgrades/Self Repair Upgrade")]
public class SelfRepairUpgrade : UpgradeData
{
    public RitualScriptableObject ritual;
    public UpgradeData repairRitual;
    public override void ApplyUpgrade(GameObject obj)
    {
        if (!GameManager.Instance.GetComponent<UpgradeManager>().WasUpgraded(repairRitual))
        {
            repairRitual.ApplyUpgrade(obj);
            GameEventHandler.Instance.UpgradeTrigger(repairRitual);
        }
        PlayerController.Instance.GetComponent<PlayerStats>().SetPermanentHealing();
        GameManager.Instance.GetComponent<IngredientManager>().RemoveRitualValues(ritual);
    }
}
