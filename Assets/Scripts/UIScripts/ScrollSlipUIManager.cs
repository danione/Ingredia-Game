using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ScrollSlipUIManager : MonoBehaviour
{
    [SerializeField] private ScrollPopup popup;
    [SerializeField] private Transform scrollSlipMenu;

    private void Start()
    {
        GameEventHandler.Instance.ScrollSlipGenerated += OnScrollSlipGenerated;
        PlayerEventHandler.Instance.OpenedScrollsMenu += OnScrollSlipMenuOpen;
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
