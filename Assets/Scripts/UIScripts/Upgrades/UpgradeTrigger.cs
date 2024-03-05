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
    private PlayerInventory playerInventory;

    private void Start()
    {
        GameEventHandler.Instance.UpgradeTriggered += OnUpgradeUnlocked;
        PlayerEventHandler.Instance.CollectedGold += OnResourceCollected;
        PlayerEventHandler.Instance.CollectedSophistication += OnResourceCollected;

        playerInventory = PlayerController.Instance.inventory;
        if (HasExistingRequirements || !CanAfford())
        {
            GetComponent<Image>().color = Color.gray;
        }
    }

    private void OnResourceCollected(int resouce)
    {
        if(!isUpgraded && CanAfford())
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    private bool CanAfford()
    {
        bool canAffordGold = (playerInventory.gold - upgradeInformation.goldCost) >= 0;
        bool canAffordSophistication = (playerInventory.sophistication - upgradeInformation.sophisticationCost) >= 0;
        return canAffordGold && canAffordSophistication;
    }

    private void OnDestroy()
    {
        try
        {
            GameEventHandler.Instance.UpgradeTriggered -= OnUpgradeUnlocked;
            PlayerEventHandler.Instance.CollectedGold -= OnResourceCollected;
            PlayerEventHandler.Instance.CollectedSophistication -= OnResourceCollected;

        }
        catch { }
    }

    public UpgradeData GetData()
    {
        return upgradeInformation;
    }

    /*
     * Check if the upgrade is still available and has requirements
     * then broadcast the information to others to remove requirements
     */
    public void Upgrade(Button_UI button)
    {
        if (!isUpgraded)
        {
            if (!HasExistingRequirements && CanAfford())
            {
                upgradeInformation.ApplyUpgrade(upgradeSubject);
                isUpgraded = true;
                GameEventHandler.Instance.UpgradeTrigger(upgradeInformation);
                try
                {
                    GameEventHandler.Instance.UpgradeTriggered -= OnUpgradeUnlocked;
                    PlayerEventHandler.Instance.CollectedGold -= OnResourceCollected;
                    PlayerEventHandler.Instance.CollectedSophistication -= OnResourceCollected;
                }
                catch { Debug.Log("Tried to unsubscribe"); }
                GetComponent<Image>().color = Color.green;
                playerInventory.AddGold(-upgradeInformation.goldCost);
                playerInventory.AdjustSophistication(-upgradeInformation.sophisticationCost);
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
            if (!HasExistingRequirements && CanAfford())
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
        string fullstring = "<b>" + upgradeInformation.upgradeName + "</b>\n";
        if (upgradeInformation.goldCost > 0)
        {
            fullstring += "<color=yellow>Gold Cost: </color>" + upgradeInformation.goldCost + "\n";
        }
        if(upgradeInformation.sophisticationCost > 0)
        {
            fullstring += "<color=blue>Sophistication Cost: </color>" + upgradeInformation.sophisticationCost + "\n";

        }
        if (upgradeInformation.GetType() == typeof(UnlockRitualUpgrade))
        {
            UnlockRitualUpgrade upgrade = (UnlockRitualUpgrade)upgradeInformation;
            if(upgrade.ritualToUnlock.ritualRecipes.Count != 0)
            {
                fullstring += "<color=yellow><i>Ingredients: ";
                for (int i = 0; i < upgrade.ritualToUnlock.ritualRecipes.Count - 1; i++)
                {
                    fullstring += "<b>" + upgrade.ritualToUnlock.ritualRecipes[i].item.ingredientName + ",</b> ";
                }
                fullstring += "<b>" + upgrade.ritualToUnlock.ritualRecipes[upgrade.ritualToUnlock.ritualRecipes.Count - 1].item.ingredientName + "</b></i></color>\n";
            }
        }
        fullstring += "------" + "\n";
        fullstring += upgradeInformation.description;
        return fullstring;
    }
}