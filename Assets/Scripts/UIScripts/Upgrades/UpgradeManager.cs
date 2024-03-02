using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    private Dictionary<UpgradeData, int> upgradeCost = new();

    private HashSet<UpgradeData> upgradesPurchased = new ();
    private bool newScene = false;

    private void Start()
    {
        GameEventHandler.Instance.UpgradeTriggered += OnUpgradePurchased;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void AddUpgradeCost(UpgradeData data)
    {
        upgradeCost[data] = data.cost;
    }

    private void OnUpgradePurchased(UpgradeData upgrade)
    {
        if(!newScene && !upgradesPurchased.Contains(upgrade))
        {
            upgradeCost.Remove(upgrade);
            upgradesPurchased.Add(upgrade);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {   
        newScene = true;
        foreach(var upgrade in upgradesPurchased) { 
            GameEventHandler.Instance.UpgradeTrigger(upgrade);
        }
        newScene = false;
    }

    public bool canAffordUpgrades()
    {
        foreach(var upgrade in upgradeCost)
        {
            if(PlayerController.Instance.inventory.gold > upgrade.Key.cost)
                return true;
        }
        return false;
    }
}
