using CodeMonkey.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Upgrade Buttons class connecting UI with upgrade scriptable objects
 */

public class UpgradeTrigger : MonoBehaviour
{
    [SerializeField] private UpgradeData upgradeInformation;
    [SerializeField] private GameObject upgradeSubject;
    [SerializeField] private List<UpgradeData> requirements;

    private bool isUpgraded = false;
    private bool HasExistingRequirements => requirements.Count > 0;

    private void Start()
    {
        GameEventHandler.Instance.UpgradeTriggered += OnUpgradeUnlocked;
        if (HasExistingRequirements)
        {
            
        }
    }

    /*
     * Check if the upgrade is still available and has requirements
     * then broadcast the information to others to remove requirements
     */
    public void Upgrade(Button_UI button)
    {
        if (!isUpgraded)
        {
            if (!HasExistingRequirements)
            {
                upgradeInformation.ApplyUpgrade(upgradeSubject);
                isUpgraded = true;
                GameEventHandler.Instance.UpgradeTrigger(upgradeInformation);
                try
                {
                    GameEventHandler.Instance.UpgradeTriggered -= OnUpgradeUnlocked;
                }
                catch { Debug.Log("Tried to unsubscribe"); }
            }
            else
            {
                Debug.Log("Requires: + " + string.Join(", ", requirements.Select(obj => obj.upgradeName).ToArray()));
            }
        }
    }

    /*
     * Check if upgrade has requirements. If the required upgrade
     * was achieved, remove the requirement
     */
    private void OnUpgradeUnlocked(UpgradeData upgrade)
    {
        if (requirements.Contains(upgrade))
        {
            requirements.Remove(upgrade);
        }
    }
}