using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private SteamballProjectile projectile;
    private Dictionary<UpgradeData, int> upgradeCost = new();

    private HashSet<UpgradeData> upgradesPurchased = new ();
    private bool newScene = false;

    private void Start()
    {
        GameEventHandler.Instance.UpgradeTriggered += OnUpgradePurchased;
        PlayerEventHandler.Instance.PlayerDied += OnDeath;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.UpgradeTriggered -= OnUpgradePurchased;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        PlayerEventHandler.Instance.PlayerDied -= OnDeath;
    }

    public void OnDeath()
    {
        upgradeCost.Clear();
        upgradesPurchased.Clear();
        projectile.ResetAffectingPlayer();
    }

    void OnSceneUnloaded(Scene scene)
    {
        if(scene.name == "Normal Level")
        {
            OnDeath();
        }
        else
        {
            newScene = true;
            foreach (var upgrade in upgradesPurchased)
            {
                GameEventHandler.Instance.UpgradeTrigger(upgrade);
            }
            newScene = false;
        }
    }

    public bool WasUpgraded(UpgradeData upgradeData)
    {
        bool isUnlocked = upgradesPurchased.Contains(upgradeData);
        bool unlockedByDefault = false;

        if(upgradeData.GetType() == typeof(UnlockRitualUpgrade) || upgradeData.GetType() == typeof(TutorialHealingRitualUnlock))
        {
            UnlockRitualUpgrade ritual = upgradeData as UnlockRitualUpgrade;
            unlockedByDefault = GameManager.Instance.GetComponent<RitualManager>()?.IsUpgraded(ritual.ritualToUnlock.ritualName) ?? false;
        }
        return isUnlocked || unlockedByDefault;
    }

    public void AddUpgradeCost(UpgradeData data)
    {
        if (!upgradesPurchased.Contains(data) && !upgradeCost.ContainsKey(data))
        {
            upgradeCost[data] = data.goldCost;

        }
    }

    private void OnUpgradePurchased(UpgradeData upgrade)
    {
        if(!newScene && !upgradesPurchased.Contains(upgrade))
        {
            upgradeCost.Remove(upgrade);
            upgradesPurchased.Add(upgrade);
        }
    }



    public bool CanAffordUpgrades()
    {
        foreach(var upgrade in upgradeCost)
        {
            if(PlayerController.Instance.inventory.gold >= upgrade.Key.goldCost && PlayerController.Instance.inventory.sophistication >= upgrade.Key.sophisticationCost)
                return true;
        }
        return false;
    }
}
