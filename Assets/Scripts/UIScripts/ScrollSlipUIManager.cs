using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ScrollSlipUIManager : MonoBehaviour
{
    [SerializeField] private ScrollPopup popup;
    [SerializeField] private Transform scrollSlipMenu;
    [SerializeField] private Transform scrollSlipManager;

    [SerializeField] private Transform countScrollsUI;

    private ScrollSlipManager slipManager;

    private void Start()
    {
        GameEventHandler.Instance.ScrollSlipGenerated += OnScrollSlipGenerated;
        PlayerEventHandler.Instance.OpenedScrollsMenu += OnScrollSlipMenuOpen;
        slipManager = scrollSlipManager.GetComponent<ScrollSlipManager>();
    }


    private void OnScrollSlipGenerated(RitualScriptableObject scroll)
    {
        GameManager.Instance.PauseGame();
        popup.scrollSlipPopupObject.gameObject.SetActive(true);
        popup.PopulateRitualIngredients(scroll);
    }

    private void OnScrollSlipMenuOpen()
    {
        scrollSlipMenu.gameObject.SetActive(true);
        GameManager.Instance.PauseGame();
        PopulateMenus();
    }


    private void PopulateMenus()
    {
        KeyValuePair<int, int> scrollSlipsCount = slipManager.GetScrollSlipsCount();

        countScrollsUI.GetChild(0).GetComponent<TextMeshProUGUI>().text = scrollSlipsCount.Key.ToString(); // current
        countScrollsUI.GetChild(2).GetComponent<TextMeshProUGUI>().text = scrollSlipsCount.Value.ToString(); // available
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

        StringBuilder sb = new();

        foreach (var item in data.ritualRecipes)
        {
            sb.AppendLine(item.item.name + " x" + item.amount.ToString());
        }

        ritualIngredients.text = sb.ToString().TrimEnd('\n');
    }
}
