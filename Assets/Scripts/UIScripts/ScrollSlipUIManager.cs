using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollSlipUIManager : MonoBehaviour
{
    [SerializeField] private ScrollPopup popup;
    [SerializeField] private Transform scrollSlipMenu;
    private Transform scrollSlipManager;

    [SerializeField] private Transform countScrollsUI;
    [SerializeField] private Transform normalSlipsContainerUI;

    private ScrollSlipManager slipManager;
    private int latestActivatedNumber = 0;
    private int slipCellsPerRow;

    private void Start()
    {
        GameEventHandler.Instance.ScrollSlipGenerated += OnScrollSlipGenerated;
        PlayerEventHandler.Instance.OpenedScrollsMenu += OnScrollSlipMenuOpen;
        PlayerEventHandler.Instance.ClosedAllOpenMenus += OnCloseMenu;
        slipManager = scrollSlipManager.GetComponent<ScrollSlipManager>();
        slipCellsPerRow = normalSlipsContainerUI.GetChild(0).childCount;
    }

    private void OnCloseMenu()
    {
        scrollSlipMenu.gameObject.SetActive(false);
    }

    private void OnScrollSlipGenerated(RitualScriptableObject scroll)
    {
        GameManager.Instance.PauseGame();
        popup.scrollSlipPopupObject.gameObject.SetActive(true);
        popup.PopulateRitualIngredients(scroll);
    }

    private void OnScrollSlipMenuOpen()
    {
        if (scrollSlipMenu.gameObject.activeSelf == false)
        {
            GameManager.Instance.PauseGame();
        }
        else
        {
            PlayerEventHandler.Instance.CloseAMenu();
            GameManager.Instance.ResumeGame();
        }

        scrollSlipMenu.gameObject.SetActive(!scrollSlipMenu.gameObject.activeSelf);

        PopulateMenus();
    }


    private void PopulateMenus()
    {
        KeyValuePair<int, int> scrollSlipsCount = slipManager.GetScrollSlipsCount();

        countScrollsUI.GetChild(0).GetComponent<TextMeshProUGUI>().text = scrollSlipsCount.Key.ToString(); // current
        countScrollsUI.GetChild(2).GetComponent<TextMeshProUGUI>().text = scrollSlipsCount.Value.ToString(); // available

        PopulateNormalSlips(scrollSlipsCount.Key);
    }

    private void PopulateNormalSlips(int currentNumberOfSlips)
    { 
        int neededRows = (currentNumberOfSlips + slipCellsPerRow - 1) / slipCellsPerRow; // round up to the nearest value

        if(neededRows + 1 > normalSlipsContainerUI.childCount)
        {
            CreateNewRows(neededRows + 1 - normalSlipsContainerUI.childCount);
        }

        ActivateNormalUI(currentNumberOfSlips);
    }

    private void CreateNewRows(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject duplicatedRow = Instantiate(normalSlipsContainerUI.GetChild(0).gameObject);

            // Set the parent transform for the duplicated UI element
            duplicatedRow.transform.SetParent(normalSlipsContainerUI);

            duplicatedRow.SetActive(true);
        }
    }

    private void ActivateNormalUI(int currentNumberOfSlips)
    {
        if (currentNumberOfSlips == latestActivatedNumber) return;
    
        while(latestActivatedNumber < currentNumberOfSlips)
        {
            int row = (latestActivatedNumber / slipCellsPerRow) + 1; // ingore the first row
            // Get the row and next activatable cell
            PopulateCellObject(normalSlipsContainerUI.GetChild(row)?.GetChild(latestActivatedNumber % slipCellsPerRow), latestActivatedNumber);
            latestActivatedNumber++;
        }
    }

    private void PopulateCellObject(Transform slipObject, int index)
    {
        // fetch ritual info and populate the cell
        RitualScriptableObject data = slipManager.GetSlipByIndex(index);
        if (slipObject == null || data == null) return;

        slipObject.gameObject.SetActive(true);
        slipObject.GetChild(0).GetComponent<TextMeshProUGUI>().text = data.name; // name of ritual
        slipObject.GetChild(2).GetComponent<TextMeshProUGUI>().text = ScrollPopup.RitualIngredientsToString(data); // ingredients of ritual
    }
}

[System.Serializable]
public class ScrollPopup
{
    public Transform scrollSlipPopupObject;
    public TextMeshProUGUI ritualName;
    public TextMeshProUGUI ritualIngredients;

    public void PopulateRitualIngredients(RitualScriptableObject data)
    {
        ritualName.text = data.name;

        ritualIngredients.text = string.Empty;

        ritualIngredients.text = RitualIngredientsToString(data);
    }

    public static string RitualIngredientsToString(RitualScriptableObject data)
    {
        StringBuilder sb = new();

        foreach (var item in data.ritualRecipes)
        {
            sb.AppendLine(item.item.name + " x" + item.amount.ToString());
        }

        return sb.ToString().TrimEnd('\n');
    }
}
