using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Transform inventoryMenu;

    [SerializeField] private UIUpgradeClass healthUI;
    [SerializeField] private UIUpgradeClass movementSpeedUI;
    [SerializeField] private UIUpgradeClass projectileUI;
    [SerializeField] private Transform projectileGameObject;
    [SerializeField] private UIUpgradeClass ammoUI;
    [SerializeField] private UIUpgradeClass fireballAreaUI;

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
        healthUI.Upgrade();
        movementSpeedUI.Upgrade();
        projectileUI.Upgrade();
        ammoUI.Upgrade();
        fireballAreaUI.Upgrade();
    }

    public void OnBuyHealth() { healthUI.BuyUpgrade(); UpdateScreen(); }
    public void OnBuyMovement() { movementSpeedUI.BuyUpgrade(); UpdateScreen(); }
    public void OnBuyProjectile() { projectileUI.BuyUpgrade(projectileGameObject.gameObject); UpdateScreen(); }
    public void OnBuyAmmoSlot() { ammoUI.BuyUpgrade(); UpdateScreen(); }
    public void OnBuyFireballArea() { fireballAreaUI.BuyUpgrade(projectileGameObject.gameObject); UpdateScreen(); }

}

[System.Serializable]
public class UIUpgradeClass
{
    [SerializeField] private Transform uIItem;
    [SerializeField] private List<UpgradeData> upgradesList = new();

    public void BuyUpgrade(GameObject obj)
    {
        if (upgradesList.Count > 0)
        {
            upgradesList[0].ApplyUpgrade(obj);
            upgradesList.RemoveAt(0);
        }
    }

    public void BuyUpgrade()
    {
        if (upgradesList.Count > 0)
        {
            upgradesList[0].ApplyUpgrade(PlayerController.Instance.gameObject);
            upgradesList.RemoveAt(0);
        }
    }

    public void Upgrade()
    {
        string upgradeName;
        string cost;

        if (upgradesList.Count > 0)
        {
            upgradeName = upgradesList[0].upgradeName;
            cost = upgradesList[0].cost.ToString();
        }
        else
        {
            upgradeName = "Latest Upgrade Received";
            cost = "";
            uIItem.GetChild(2).gameObject.SetActive(false);
        }

        uIItem.GetChild(0).GetComponent<TextMeshProUGUI>().text = upgradeName;
        uIItem.GetChild(1).GetComponent<TextMeshProUGUI>().text = cost;
    }
}
