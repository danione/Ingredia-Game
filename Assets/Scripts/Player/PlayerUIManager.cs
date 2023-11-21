using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_GoldCounter;
    [SerializeField] private GameObject m_slotsContainer;
    [SerializeField] private int m_maxSlots = 3;

    private void Start()
    {
        PlayerEventHandler.Instance.CollectedGold += OnCollectedGold;
        PlayerEventHandler.Instance.UpdatedPotions += OnUpdatedPotionsInventory;
    }

    private void OnCollectedGold(int gold)
    {
        m_GoldCounter.text = "Gold: " + gold;
    }

    private void OnUpdatedPotionsInventory(string name, int amount, int slotNumber)
    {
        if (slotNumber < 1 || slotNumber > m_maxSlots) return;

        GameObject slotGameObject = GameObject.FindGameObjectWithTag("Slot " + slotNumber);
        GameObject amountObject = slotGameObject.transform.GetChild(2).gameObject;
        GameObject nameOfPotion = slotGameObject.transform.GetChild(1).gameObject;

        if (name == null || amount == 0)
        {
            nameOfPotion.GetComponent<TextMeshProUGUI>().text = "Empty";
            amountObject.SetActive(false);
        } else if(amount != 0)
        {
            nameOfPotion.GetComponent<TextMeshProUGUI>().text = name;
            amountObject.SetActive(true);
            amountObject.GetComponent<TextMeshProUGUI>().text = "x" + amount.ToString();
        }
    }
}
