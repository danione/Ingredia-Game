using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform templateObject;
    [SerializeField] private PlayerInventory inventory;
    private Dictionary<string,TextMeshProUGUI> transforms = new Dictionary<string, TextMeshProUGUI>();

    // Start is called before the first frame update
    void Start()
    {
        templateObject.gameObject.SetActive(false);
        inventory.CollectedIngredient += OnAdjustInventoryUI; 
    }

    public void OnAdjustInventoryUI(IIngredient itemName, int count)
    {
        if(!transforms.ContainsKey(itemName.Name))
        {
            Transform newObject = Instantiate(templateObject, templateObject.parent);
            float offsetY = newObject.GetComponent<RectTransform>().sizeDelta.y;

            newObject.position = newObject.position - new Vector3(0f, offsetY * transforms.Count, 0f);
            transforms[itemName.Name] = newObject.GetComponentInChildren<TextMeshProUGUI>();
            newObject.gameObject.SetActive(true);
        } 
            
        transforms[itemName.Name].text = itemName.Name + " x" + count;
   
    }

    public void RemoveInventoryUI(string itemName)
    {
        if (transforms.ContainsKey(itemName))
        {
            Destroy(transforms[itemName].transform.parent);
            transforms.Remove(itemName);
        }
    }

    public void RemoveAllInventoryItems()
    {
        foreach(string item in transforms.Keys)
        {
            RemoveInventoryUI(item);
        }
    }
}
