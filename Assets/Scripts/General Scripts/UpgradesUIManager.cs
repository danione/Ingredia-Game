using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UIElements;
public class UpgradesUIManager : MonoBehaviour
{

    [SerializeField] private Transform upgradesMenu;
    [SerializeField] private Transform departmentContainer;

    private void Start()
    {
        PlayerEventHandler.Instance.UpgradesMenuOpen += OnUpgradesMenuOpened;
        PlayerEventHandler.Instance.UpgradesMenuClose += OnUpgradesMenuClosed;
        PlayerEventHandler.Instance.ClosedAllOpenMenus += OnUpgradesMenuClosed;
        GameEventHandler.Instance.SetTutorialMode += OnSetTutorialMode;

        Initialise();
        

    }

    /*
     * Sets up buttons
     * Foreach button within every department container, hook up the click function
     * to the ApplyUpgrade function of every UpgradeData object
     */
    private void Initialise()
    {
        Button_UI[] buttons = departmentContainer.GetComponentsInChildren<Button_UI>();

        // Iterate through each button and add an onClick event listener
        foreach (Button_UI button in buttons)
        {
            button.ClickFunc = () => {
                // Code to execute when the button is clicked
                button.GetComponent<UpgradeTrigger>().Upgrade(button);
            };
        }
    }

    private void OnSetTutorialMode()
    {
        // Tutorial Mode
    }

    private void OnUpgradesMenuOpened()
    {
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
        upgradesMenu.gameObject.SetActive(false);
    }
}
