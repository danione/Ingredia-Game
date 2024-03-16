using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Self-Repair Upgrade", menuName = "Scriptable Objects/Upgrades/Self Repair Upgrade")]
public class SelfRepairUpgrade : UpgradeData
{
    public RitualScriptableObject ritual;
    public override void ApplyUpgrade(GameObject obj)
    {
        PlayerController.Instance.GetComponent<PlayerStats>().SetPermanentHealing();
        GameManager.Instance.GetComponent<IngredientManager>().RemoveRitualValues(ritual);
    }
}
