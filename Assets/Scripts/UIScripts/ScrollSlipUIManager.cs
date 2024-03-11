using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ScrollSlipUIManager : MonoBehaviour
{
    [SerializeField] private ScrollPopup popup;
    [SerializeField] private Transform scrollSlipMenu;

    [SerializeField] private Transform countScrollsUI;
    [SerializeField] private Transform normalSlipsContainerUI;


    private void Start()
    {
        GameEventHandler.Instance.ScrollSlipGenerated += OnScrollSlipGenerated;
        PlayerEventHandler.Instance.OpenedScrollsMenu += OnScrollSlipMenuOpen;
        PlayerEventHandler.Instance.ClosedAllOpenMenus += OnCloseMenu;
    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.ScrollSlipGenerated -= OnScrollSlipGenerated;
        PlayerEventHandler.Instance.OpenedScrollsMenu -= OnScrollSlipMenuOpen;
        PlayerEventHandler.Instance.ClosedAllOpenMenus -= OnCloseMenu;
    }

    private void OnCloseMenu()
    {
        scrollSlipMenu.gameObject.SetActive(false);
    }

    private void OnScrollSlipGenerated(RitualScriptableObject scroll)
    {
        if(popup.scrollSlipPopupObject == null) { return; }
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

        //PopulateMenus();
    }

}

[System.Serializable]
public class ScrollPopup
{
    public Transform scrollSlipPopupObject;
    public TextMeshProUGUI ritualName;
    public TextMeshProUGUI ritualDescription;
    public TextMeshProUGUI ritualIngredients;

    public void PopulateRitualIngredients(RitualScriptableObject data)
    {
        ritualName.text = data.ritualName;

        ritualDescription.text = data.description;

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
