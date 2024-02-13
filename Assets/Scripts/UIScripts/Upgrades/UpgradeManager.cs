using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    private HashSet<UpgradeData> upgradesPurchased = new ();
    private bool newScene = false;

    private void Start()
    {
        GameEventHandler.Instance.UpgradeTriggered += OnUpgradePurchased;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnUpgradePurchased(UpgradeData upgrade)
    {
        if(!newScene && !upgradesPurchased.Contains(upgrade))
        {
            Debug.Log(upgrade.upgradeName);
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
}
