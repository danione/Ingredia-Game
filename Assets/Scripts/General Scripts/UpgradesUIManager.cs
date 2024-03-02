using CodeMonkey.Utils;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class UpgradesUIManager : MonoBehaviour
{
    [SerializeField] private Transform upgradesMenu;
    [SerializeField] private Transform departmentContainer;
    private UpgradeManager upgradeManager;

    private void Start()
    {
        PlayerEventHandler.Instance.UpgradesMenuOpen += OnUpgradesMenuOpened;
        PlayerEventHandler.Instance.UpgradesMenuClose += OnUpgradesMenuClosed;
        PlayerEventHandler.Instance.ClosedAllOpenMenus += OnUpgradesMenuClosed;
        GameEventHandler.Instance.SetTutorialMode += OnSetTutorialMode;
        FullInUpgradeCosts();
    }

    private void FullInUpgradeCosts()
    {
        upgradeManager = GameManager.Instance.GetComponent<UpgradeManager>();

        Button_UI[] buttons = departmentContainer.GetComponentsInChildren<Button_UI>(includeInactive: true);

        // Iterate through each button and add an onClick event listener
        foreach (Button_UI button in buttons)
        {
            UpgradeTrigger trigger = button.GetComponent<UpgradeTrigger>();
            upgradeManager.AddUpgradeCost(trigger.GetData());
        }
    }

    private void Awake()
    {
        Initialise();
    }

    private void OnDestroy()
    {
        try
        {
            PlayerEventHandler.Instance.UpgradesMenuOpen += OnUpgradesMenuOpened;
            PlayerEventHandler.Instance.UpgradesMenuClose += OnUpgradesMenuClosed;
            PlayerEventHandler.Instance.ClosedAllOpenMenus += OnUpgradesMenuClosed;
            GameEventHandler.Instance.SetTutorialMode += OnSetTutorialMode;
        }
        catch { }
    }

    /*
     * Sets up buttons
     * Foreach button within every department container, hook up the click function
     * to the ApplyUpgrade function of every UpgradeData object
     */
    private void Initialise()
    {
        Button_UI[] buttons = departmentContainer.GetComponentsInChildren<Button_UI>(includeInactive: true);

        // Iterate through each button and add an onClick event listener
        foreach (Button_UI button in buttons)
        {
            UpgradeTrigger trigger = button.GetComponent<UpgradeTrigger>();
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = trigger.GetUpgradeName();
            button.ClickFunc = () => {
                // Code to execute when the button is clicked
                trigger.Upgrade(button);
            };
            button.MouseOverOnceFunc = () =>
            {
                TooltipUI.ShowTooltip_Static(trigger.GetFullUpgradeInformation());
            };
            button.MouseOutOnceFunc = () =>
            {
                TooltipUI.HideTooltip_Static();
            };
        }
    }

    private void OnSetTutorialMode()
    {
        // Tutorial Mode
    }

    private void OnUpgradesMenuOpened()
    {
        if (upgradesMenu == null) return;

        if (upgradesMenu.gameObject.activeSelf == true)
        {
            GameManager.Instance.ResumeGame();
        }
        else
        {
            GameManager.Instance.PauseGame();
        }

        upgradesMenu.gameObject.SetActive(!upgradesMenu.gameObject.activeSelf);
    }

    private void OnUpgradesMenuClosed()
    {
        if (upgradesMenu == null) return;

        upgradesMenu.gameObject.SetActive(false);
    }
}
