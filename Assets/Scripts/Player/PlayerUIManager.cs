using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_GoldCounter;
    private void Start()
    {
        PlayerEventHandler.Instance.CollectedGold += OnCollectedGold;
    }

    private void OnDestroy()
    {
        PlayerEventHandler.Instance.CollectedGold -= OnCollectedGold;
    }

    private void OnCollectedGold(int gold)
    {
        m_GoldCounter.text = "Gold: " + gold;
    }
}
