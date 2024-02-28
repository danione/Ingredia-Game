using CodeMonkey.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
            GetComponent<Image>().color = Color.gray;
        }
    }

    private void OnDestroy()
    {
        try
        {
            GameEventHandler.Instance.UpgradeTriggered -= OnUpgradeUnlocked;

        }
        catch { }
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
                GetComponent<Image>().color = Color.green;

            }
            else
            {
                Debug.Log("Requires: " + string.Join(", ", requirements.Select(obj => obj.upgradeName).ToArray()));
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
            if (!HasExistingRequirements)
            {
                GetComponent<Image>().color = Color.white;
            }
        }
    }

    public string GetUpgradeName()
    {
        return upgradeInformation.upgradeName;
    }

    public string GetFullUpgradeInformation()
    {
        string fullstring = "<color=yellow>" + upgradeInformation.upgradeName + "</color>\n";
        fullstring += "<i>Upgrade Cost: </i>" + upgradeInformation.cost + "\n";
        fullstring += "------" + "\n";
        fullstring += upgradeInformation.description;
        return fullstring;
    }
}