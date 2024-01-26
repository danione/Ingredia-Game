using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_HealthAmount;

    private void Start()
    {
        PlayerEventHandler.Instance.HealthAdjusted += OnHealthAdjust;
        OnHealthAdjust();
    }

    private void OnHealthAdjust()
    {
        m_HealthAmount.text = PlayerController.Instance.stats.Health.ToString();
    }
}
