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

    [SerializeField] private UIUpgradeClass healthUI;
    [SerializeField] private UIUpgradeClass movementSpeedUI;
    [SerializeField] private UIUpgradeClass projectileUI;

    [SerializeField] private List<UIUpgradeClass> availableUpgrades = new();
    [SerializeField] private List<UnityEngine.UI.Button> buttons = new();

    [SerializeField] private List<UIUpgradeClass> randomChosenUpgrades = new();
    [SerializeField] private int countChosenUpgrades;

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

        availableUpgrades.Sort((x, y) => x.MinCost.CompareTo(y.MinCost));

        for(int i = 0; i < maxDisplayUpgrades; i++)
        {
            if (i >= availableUpgrades.Count) break;
            randomChosenUpgrades.Add(availableUpgrades[i]);
            countChosenUpgrades++;
        }

        HookUpgradesWithButtons();
    }

    private void HookUpgradesWithButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i >= countChosenUpgrades) return;
            int currentIndex = i;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => OnBuyClicked(currentIndex));
            randomChosenUpgrades[i].Reset(buttons[i].transform.parent.transform);
        }
    }

    private void OnBuyClicked(int index)
    {
        if(index >= countChosenUpgrades) return;

        randomChosenUpgrades[index].BuyUpgrade();
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
        healthUI.Upgrade();
        movementSpeedUI.Upgrade();
        projectileUI.Upgrade();

        foreach(var upgrade in randomChosenUpgrades)
        {
            if (upgrade != null)
                upgrade.Upgrade();
        }
    }

    public void OnBuyHealth() { healthUI.BuyUpgrade();}
    public void OnBuyMovement() { movementSpeedUI.BuyUpgrade();}
    public void OnBuyProjectile() { projectileUI.BuyUpgrade();}
}
