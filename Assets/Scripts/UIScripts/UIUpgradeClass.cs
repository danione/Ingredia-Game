using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[System.Serializable]
public class UIUpgradeClass
{
    [SerializeField] private string nameOfUpgrade;
    [SerializeField] private Transform uIItem;
    [SerializeField] private List<UpgradeData> upgradesList = new();
    [SerializeField] private bool isRandomlyGenerated = false;
    [SerializeField] private GameObject upgradedObject;
    private bool justPurchased = false;

    public int MinCost { get { return upgradesList.Count > 0 ? upgradesList[0].cost : -1; } }

    public void Reset(Transform uIItemNew)
    {
        justPurchased = false;
        uIItem = uIItemNew;
    }

    public void BuyUpgrade()
    {
        if (upgradesList.Count > 0)
        {
            upgradesList[0].ApplyUpgrade(upgradedObject);
            upgradesList.RemoveAt(0);
            justPurchased = isRandomlyGenerated;
        }
        Upgrade();
    }

    public void Upgrade()
    {
        string upgradeName;
        string cost;

        if (upgradesList.Count > 0 && !justPurchased)
        {
            upgradeName = upgradesList[0].upgradeName;
            cost = upgradesList[0].cost.ToString();
            uIItem.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            upgradeName = "Latest Upgrade Received";
            cost = "";
            uIItem.GetChild(2).gameObject.SetActive(false);
        }

        uIItem.GetChild(0).GetComponent<TextMeshProUGUI>().text = upgradeName;
        uIItem.GetChild(1).GetComponent<TextMeshProUGUI>().text = cost;
    }
}
