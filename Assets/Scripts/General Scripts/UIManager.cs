using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Transform inventoryMenu;
    [SerializeField] private Transform healthUIItem;
    [SerializeField] private List<HealthUpgrade> healthUpgrades = new();

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
        HealthUpgrade();
    }

    private void HealthUpgrade()
    {
        string upgradeName;
        string cost;
        if (healthUpgrades.Count > 0)
        {
            upgradeName = healthUpgrades[0].upgradeName;
            cost = healthUpgrades[0].cost.ToString();
        }
        else
        {
            upgradeName = "Latest Endurance Upgrade Received";
            cost = "";
            healthUIItem.GetChild(2).gameObject.SetActive(false);
        }

        healthUIItem.GetChild(0).GetComponent<TextMeshProUGUI>().text = upgradeName;
        healthUIItem.GetChild(1).GetComponent<TextMeshProUGUI>().text = cost;
    }

    public void BuyHealthUpgrade()
    {
        if(healthUpgrades.Count > 0)
        {
            healthUpgrades[0].ApplyUpgrade(PlayerController.Instance.gameObject);
            healthUpgrades.RemoveAt(0);
            UpdateScreen();
        }
        UpdateScreen();
    }
}
