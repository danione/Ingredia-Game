using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Transform inventoryMenu;
    [SerializeField] private int maxDisplayUpgrades;

    [SerializeField] private UIUpgradeClass healthUI;
    [SerializeField] private UIUpgradeClass movementSpeedUI;
    [SerializeField] private UIUpgradeClass projectileUI;
    [SerializeField] private Transform projectileGameObject;

    [SerializeField] private List<UIUpgradeClass> availableUpgrades = new();
    [SerializeField] private List<UnityEngine.UI.Button> buttons = new();
    [SerializeField] private List<UIUpgradeClass> randomChosenUpgrades = new();

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
        UpdateScreen();
        ChooseRandomUpgrades();
        inventoryMenu.gameObject.SetActive(true);
    }

    private void OnUpgradesMenuClosed()
    {
        inventoryMenu.gameObject.SetActive(false);
    }

    private void ChooseRandomUpgrades()
    {
        if (randomChosenUpgrades.Count > 0) return;

        ShuffleListOfUpgrades(availableUpgrades);

        availableUpgrades.Sort((x, y) => x.MinCost.CompareTo(y.MinCost));

        for(int i = 0; i < maxDisplayUpgrades; i++)
        {
            randomChosenUpgrades.Add(availableUpgrades[i]);
        }

        HookUpgradesWithButtons();
    }

    private void HookUpgradesWithButtons()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            if (i >= randomChosenUpgrades.Count) return;
            
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => randomChosenUpgrades[i].BuyUpgrade());
            randomChosenUpgrades[i].Reset(buttons[i].transform.parent.transform);
        }
    }

    void ShuffleListOfUpgrades<T>(List<T> array)
    {
        int numItems = array.Count;
        for (int i = numItems - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    private void UpdateScreen()
    {
        healthUI.Upgrade();
        movementSpeedUI.Upgrade();
        projectileUI.Upgrade();

        foreach(var upgrade in availableUpgrades)
        {
            upgrade.Upgrade();
        }
    }

    public void OnBuyHealth() { healthUI.BuyUpgrade();}
    public void OnBuyMovement() { movementSpeedUI.BuyUpgrade(); }
    public void OnBuyProjectile() { projectileUI.BuyUpgrade();}


}

[System.Serializable]
public class UIUpgradeClass
{
    [SerializeField] private Transform uIItem;
    [SerializeField] private List<UpgradeData> upgradesList = new();
    [SerializeField] private bool isRandomlyGenerated = false;
    [SerializeField] private GameObject upgradedObject;
    private bool justPurchased = false;

    public int MinCost { get { return upgradesList.Count > 0 ? upgradesList[0].cost : -1; } }

    public void Reset(Transform uIItemNew)
    {
        justPurchased = false;
        uIItem = uIItemNew;
    }

    public void BuyUpgrade()
    {
        if (upgradesList.Count > 0)
        {
            upgradesList[0].ApplyUpgrade(upgradedObject);
            upgradesList.RemoveAt(0);
            justPurchased =  isRandomlyGenerated ? true : false;
        }
        Upgrade();
    }

    public void Upgrade()
    {
        if (uIItem == null) return;
     
        string upgradeName;
        string cost;

        if (upgradesList.Count > 0 && !justPurchased)
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
