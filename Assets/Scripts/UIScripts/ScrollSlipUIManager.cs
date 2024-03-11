using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ScrollSlipUIManager : MonoBehaviour
{
    [SerializeField] private ScrollPopup popup;
    [SerializeField] private Transform scrollSlipMenu;

    [SerializeField] private GameObject contentsParent; // for contents page
    [SerializeField] private float contentsOffset;
    private Dictionary<RitualScriptableObject, GameObject> ritualToObjectContents = new();

    private void Start()
    {
        GameEventHandler.Instance.ScrollSlipGenerated += OnScrollSlipGenerated;
        PlayerEventHandler.Instance.OpenedScrollsMenu += OnScrollSlipMenuOpen;
        PlayerEventHandler.Instance.ClosedAllOpenMenus += OnCloseMenu;
        GameEventHandler.Instance.UnlockedRitual += CheckIfUnlocked;
        FillInContents();
    }

    // Fills in the contents of the grimoire
    // --
    // Gets all the rituals and creates the contents objects,
    // filling them in with ritual name, offseting them
    private void FillInContents()
    {
        GameObject ritualTemplate = contentsParent.transform.GetChild(0).gameObject;
        RitualManager ritualManager = GameManager.Instance.GetComponent<RitualManager>();
        List<RitualScriptableObject> allRituals = ritualManager.GetAllRituals();

        for (int i = 0; i < allRituals.Count; i++)
        {
            if (!ritualToObjectContents.ContainsKey(allRituals[i]))
            {
                GameObject newRitual = Instantiate(ritualTemplate, contentsParent.transform);
                ritualToObjectContents[allRituals[i]] = newRitual;
                newRitual.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = allRituals[i].ritualName;
                SetupRitualObject(ritualTemplate, i, newRitual, allRituals[i]);
                newRitual.SetActive(true);
            }
        }
    }

    private void SetupRitualObject(GameObject ritualTemplate, int numItem, GameObject newRitual, RitualScriptableObject ritualScr)
    {
        SetPosition(ritualTemplate, numItem, newRitual);
        SetText(newRitual, ritualScr);
    }

    private void SetText(GameObject newRitual, RitualScriptableObject ritualScr)
    {
        newRitual.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ritualScr.ritualName;
        CheckIfUnlocked(ritualScr);
    }

    private void CheckIfUnlocked(RitualScriptableObject ritualScr)
    {
        RitualManager ritualManager = GameManager.Instance.GetComponent<RitualManager>();
        if (ritualManager.IsUpgraded(ritualScr.ritualName) && ritualToObjectContents.ContainsKey(ritualScr))
        {
            ritualToObjectContents[ritualScr].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow;
        }
    }

    private void SetPosition(GameObject ritualTemplate, int numItem, GameObject newRitual)
    {
        Vector3 pos = new Vector2(ritualTemplate.transform.position.x, ritualTemplate.transform.position.y - (contentsOffset * numItem));
        newRitual.transform.position = pos;
        newRitual.transform.SetParent(contentsParent.transform);
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

    private void CheckCountRituals()
    {
        int contentsAmount = contentsParent.transform.childCount - 1;
        
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
