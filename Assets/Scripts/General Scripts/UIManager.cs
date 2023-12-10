using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Transform inventoryMenu;
    [SerializeField] private int maxDisplayUpgrades;
    [SerializeField] private TextMeshProUGUI goldField;

    [SerializeField] private List<UIUpgradeClass> rightSideUpgrades = new();
    [SerializeField] private List<UnityEngine.UI.Button> rightsideButtons = new();

    [SerializeField] private List<UIUpgradeClass> availableUpgrades = new();
    [SerializeField] private List<UnityEngine.UI.Button> leftsideButtons = new();

    private List<UIUpgradeClass> randomChosenUpgrades = new();
    private int countChosenUpgrades;

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
        HookUpgradesWithButtons(rightsideButtons, rightSideUpgrades, rightSideUpgrades.Count);
        countChosenUpgrades = 0;
    }

    private void OnUpgradesMenuOpened()
    {
        ChooseRandomUpgrades();
        UpdateScreen();
        inventoryMenu.gameObject.SetActive(true);
    }

    private void OnUpgradesMenuClosed()
    {
        inventoryMenu.gameObject.SetActive(false);
    }

    private void ChooseRandomUpgrades()
    {
        if (countChosenUpgrades > 0 || availableUpgrades.Count == 0) return;

        ShuffleListOfUpgrades(availableUpgrades);

        randomChosenUpgrades.Clear();

        for (int i = 0; i < maxDisplayUpgrades; i++)
        {
            if (i >= availableUpgrades.Count) break;
            randomChosenUpgrades.Add(availableUpgrades[i]);
            countChosenUpgrades++;
        }

        randomChosenUpgrades.Sort((x, y) => x.MinCost.CompareTo(y.MinCost));

        HookUpgradesWithButtons(leftsideButtons, randomChosenUpgrades, countChosenUpgrades);
    }

    private void HookUpgradesWithButtons(List<UnityEngine.UI.Button> buttonList, List<UIUpgradeClass> upgradesList, int threshold)
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (i >= threshold) return;
            int currentIndex = i;
            buttonList[i].onClick.RemoveAllListeners();
            buttonList[i].onClick.AddListener(() => OnBuyClicked(currentIndex, upgradesList));
            upgradesList[i].Reset(buttonList[i].transform.parent.transform);
        }
    }

    private void OnBuyClicked(int index, List<UIUpgradeClass> upgradesList)
    {
        if(PlayerController.Instance.inventory.gold < upgradesList[index].MinCost) return;

        PlayerController.Instance.inventory.AddGold(-upgradesList[index].MinCost);
        upgradesList[index].BuyUpgrade();
        UpdateScreen();

        if(upgradesList == randomChosenUpgrades)
            countChosenUpgrades--;
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
        foreach (var upgrade in rightSideUpgrades)
        {
            upgrade?.Upgrade();
        }

        foreach (var upgrade in randomChosenUpgrades)
        {
            upgrade?.Upgrade();
        }

        UpdateGoldUI();
    }

    private void UpdateGoldUI()
    {
        goldField.text = PlayerController.Instance.inventory.gold.ToString();
    }
}
