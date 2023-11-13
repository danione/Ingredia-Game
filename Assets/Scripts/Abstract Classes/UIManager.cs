using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class UIManager : MonoBehaviour
{
    [SerializeField] protected Transform templateObject;
    private Dictionary<string,TextMeshProUGUI> transforms = new Dictionary<string, TextMeshProUGUI>();

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        templateObject.gameObject.SetActive(false);
        templateObject.transform.parent.gameObject.SetActive(false);
    }

    public void OnAdjustInventoryUI(IIngredient itemName, int count)
    {
        if(itemName != null)
            OnAdjustInventoryUI(itemName.IngredientName, count); 
    }

    public void OnAdjustInventoryUI(string itemName, int count)
    {
        // If first item, activate the UI element
        if (transforms.Count == 0) { templateObject.transform.parent.gameObject.SetActive(true); }


        if (!transforms.ContainsKey(itemName))
        {
            Transform newObject = Instantiate(templateObject, templateObject.parent);
            float offsetY = newObject.GetComponent<RectTransform>().sizeDelta.y;

            newObject.position = newObject.position - new Vector3(0f, offsetY * transforms.Count, 0f);
            transforms[itemName] = newObject.GetComponentInChildren<TextMeshProUGUI>();
            newObject.gameObject.SetActive(true);
        }

        transforms[itemName].text = itemName + " x" + count;
    }

    public void RemoveInventoryUI(string itemName)
    {
        if (transforms.ContainsKey(itemName))
        {
            Destroy(transforms[itemName].gameObject.transform.parent.gameObject);
        }
    }

    public void RemoveAllInventoryItems()
    {
        foreach(string item in transforms.Keys)
        {
            RemoveInventoryUI(item);
        }
        transforms.Clear();
        templateObject.transform.parent.gameObject.SetActive(false);
    }
}
