using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tutorial Upgrade", menuName = "Scriptable Objects/Upgrades/Tutorial Upgrade")]

public class TutorialHealingRitualUnlock : UpgradeData
{
    public RitualScriptableObject ritualToUnlock;

    public override void ApplyUpgrade(GameObject obj)
    {
        GameManager.Instance.GetComponent<RitualManager>().AddRitualToUnlocked(ritualToUnlock.ritualName);
        foreach (var item in ritualToUnlock.ritualRecipes)
        {
            GameManager.Instance.GetComponent<IngredientManager>().UnlockIngredient(item.item);
        }
        if(TutorialManager.instance != null)
        {
            TutorialManager.instance.OnUpgraded(ritualToUnlock.ritualRecipes);
        }
    }
}
