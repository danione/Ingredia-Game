using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Transform inventoryMenu;
    [SerializeField] private Transform healthUIItem;
    [SerializeField] private List<UpgradeData> healthUpgrades = new();
    [SerializeField] private Transform movementSpeedUIItem;
    [SerializeField] private List<UpgradeData> movementUpgrades = new();

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GameEventHandler.Instance.UpgradesMenuOpen += OnUpgradesMenuOpened;
        GameEventHandler.Instance.UpgradesMenuClose += OnUpgradesMenuClosed;
    }

    private void OnUpgradesMenuOpened()
    {
        inventoryMenu.gameObject.SetActive(true);
        UpdateScreen();
    }

    public void OnUpgradesMenuClosed()
    {
        inventoryMenu.gameObject.SetActive(false);
    }

    private void UpdateScreen()
    {
        Upgrade(healthUIItem, healthUpgrades);
        Upgrade(movementSpeedUIItem, movementUpgrades);
    }

    private void Upgrade(Transform uiItem, List<UpgradeData> upgrades)
    {
        string upgradeName;
        string cost;
        if (upgrades.Count > 0)
        {
            upgradeName = upgrades[0].upgradeName;
            cost = upgrades[0].cost.ToString();
        }
        else
        {
            upgradeName = "Latest Upgrade Received";
            cost = "";
            uiItem.GetChild(2).gameObject.SetActive(false);
        }

        uiItem.GetChild(0).GetComponent<TextMeshProUGUI>().text = upgradeName;
        uiItem.GetChild(1).GetComponent<TextMeshProUGUI>().text = cost;
    }

    private enum UpgradeTypes
    {
        Health,
        MovementSpeed
    }


    public void OnBuyHealth() { BuyButton(UpgradeTypes.Health); }
    public void OnBuyMovement() { BuyButton(UpgradeTypes.MovementSpeed); }

    private void BuyButton(UpgradeTypes type)
    {
        switch(type)
        {
            case UpgradeTypes.Health: BuyUpgrade(healthUpgrades); break;
            case UpgradeTypes.MovementSpeed: BuyUpgrade(movementUpgrades); break;
        }
    }

    private void BuyUpgrade(List<UpgradeData> upgrade)
    {
        if(upgrade.Count > 0)
        {
            upgrade[0].ApplyUpgrade(PlayerController.Instance.gameObject);
            upgrade.RemoveAt(0);
            UpdateScreen();
        }
        UpdateScreen();
    }
}
