using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_GoldCounter;
    private PlayerInventory m_PlayerInventory;
    private void Awake()
    {
        m_PlayerInventory = GetComponent<PlayerInventory>();
        m_PlayerInventory.CollectedGold += OnCollectedGold;
    }

    private void OnDestroy()
    {
        m_PlayerInventory.CollectedGold -= OnCollectedGold;
    }

    private void OnCollectedGold(int gold)
    {
        m_GoldCounter.text = "Gold: " + gold;
    }
}
