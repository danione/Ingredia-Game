using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;

public class ScrollSlipUIManager : MonoBehaviour
{
    [SerializeField] private ScrollPopup popup;
    [SerializeField] private Transform scrollSlipMenu;

    [SerializeField] private GameObject contentsParent; // for contents page
    [SerializeField] private float contentsOffset;
    private Dictionary<RitualScriptableObject, GameObject> ritualToObjectContents = new();

    // More info panel
    [SerializeField] private TextMeshProUGUI ritualNameText;
    [SerializeField] private TextMeshProUGUI ritualDescription;
    [SerializeField] private TextMeshProUGUI ritualIngredients;
    [SerializeField] private GameObject ritualLockedScreen;

    private void Start()
    {
        GameEventHandler.Instance.ScrollSlipGenerated += OnScrollSlipGenerated;
        PlayerEventHandler.Instance.OpenedScrollsMenu += OnScrollSlipMenuOpen;
        PlayerEventHandler.Instance.ClosedAllOpenMenus += OnCloseMenu;
        GameEventHandler.Instance.UnlockedRitual += CheckIfUnlocked;
        GameEventHandler.Instance.ScrollSlipGenerated += CheckIfScrollCollected;
        StartCoroutine(WaitToFillInContents());
    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.ScrollSlipGenerated -= OnScrollSlipGenerated;
        PlayerEventHandler.Instance.OpenedScrollsMenu -= OnScrollSlipMenuOpen;
        PlayerEventHandler.Instance.ClosedAllOpenMenus -= OnCloseMenu;
        GameEventHandler.Instance.UnlockedRitual -= CheckIfUnlocked;
        GameEventHandler.Instance.ScrollSlipGenerated -= CheckIfScrollCollected;
    }

    private IEnumerator WaitToFillInContents()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        FillInContents();
    }


    // Fills in the contents of the grimoire
    // --
    // Gets all the rituals and creates the contents objects,
    // then enabling it
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
        if (allRituals.Count > 0)
            DisplayMoreInfo(contentsParent.transform.GetChild(1).gameObject);
    }

    // Generic function that holds all setting up
    private void SetupRitualObject(GameObject ritualTemplate, int numItem, GameObject newRitual, RitualScriptableObject ritualScr)
    {
        SetPosition(ritualTemplate, numItem, newRitual);
        SetText(newRitual, ritualScr);
        SetButton(newRitual);
    }

    // Sets the object as a button which will populate the information
    private void SetButton(GameObject newRitual)
    {
        Button_UI button = newRitual.GetComponent<Button_UI>();
        button.ClickFunc = () =>
        {
            DisplayMoreInfo(newRitual);
        };

    }

    private void DisplayMoreInfo(GameObject ritualObject)
    {
        ContentGrimoire content = ritualObject.GetComponent<ContentGrimoire>();
        
        if (content == null) return;

        ritualNameText.text = content.ritual.ritualName;
        ritualDescription.text = content.ritual.description;
        LimitInformation(content);
    }

    private void LimitInformation(ContentGrimoire content)
    {
        if (content.isUpgraded)
        {
            ritualLockedScreen.SetActive(false);
        }
        else
        {
            ritualLockedScreen.SetActive(true);
        }

        if (content.hasScrollSlip)
        {
            ritualIngredients.text = "";
            foreach (var ingredient in content.ritual.ritualRecipes)
            {
                ritualIngredients.text += ingredient.item.ingredientName + " x " + ingredient.amount + "\n";
            }
        }
        else
        {
            ritualIngredients.text = "Defeat more enemies and obtain more scroll slips to unlock the ingredients of this ritual.";
        }
    }

    // Set the Text and check if its unlocked -> to set the colour
    private void SetText(GameObject newRitual, RitualScriptableObject ritualScr)
    {
        TextMeshProUGUI component = newRitual.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        
        component.text = ritualScr.ritualName;

        CheckIfScrollCollected(ritualScr);
        CheckIfUnlocked(ritualScr);
    }

    private void CheckIfScrollCollected(RitualScriptableObject ritualScr)
    {
        ContentGrimoire content = ritualToObjectContents[ritualScr].gameObject.GetComponent<ContentGrimoire>();

        if (GameManager.Instance.GetComponent<ScrollSlipManager>()?.IsScrollUnlocked(ritualScr) ?? false)
        {
            content.hasScrollSlip = true;
        }
    }

    // Checks if unlocked - also used when unlocking a ritual
    private void CheckIfUnlocked(RitualScriptableObject ritualScr)
    {
        if(!ritualToObjectContents.ContainsKey(ritualScr)) { return; }

        RitualManager ritualManager = GameManager.Instance.GetComponent<RitualManager>();
        ContentGrimoire content = ritualToObjectContents[ritualScr].gameObject.GetComponent<ContentGrimoire>();

        if (ritualManager.IsUpgraded(ritualScr.ritualName))
        {
            content.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow;
            content.isUpgraded = true;
        }
        content.ritual = ritualScr;
    }



    // Sets the position
    private void SetPosition(GameObject ritualTemplate, int numItem, GameObject newRitual)
    {
        Vector3 pos = new Vector2(ritualTemplate.transform.position.x, ritualTemplate.transform.position.y - (contentsOffset * numItem));
        newRitual.transform.position = pos;
        newRitual.transform.SetParent(contentsParent.transform);
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
