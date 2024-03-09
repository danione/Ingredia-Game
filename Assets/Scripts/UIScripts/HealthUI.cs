using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_HealthAmount;
    [SerializeField] private TextMeshProUGUI m_ArmourAmount;

    private void Start()
    {
        PlayerEventHandler.Instance.HealthAdjusted += OnHealthAdjust;
        GameEventHandler.Instance.UpdatedUI += OnUpdateUI;
        OnHealthAdjust();
    }

    private void OnDestroy()
    {
        try
        {
            PlayerEventHandler.Instance.HealthAdjusted -= OnHealthAdjust;
            GameEventHandler.Instance.UpdatedUI -= OnUpdateUI;
        }
        catch { }
    }

    private void OnHealthAdjust()
    {
        m_HealthAmount.text = PlayerController.Instance.stats.Health.ToString();
        m_ArmourAmount.text = PlayerController.Instance.stats.Armour.ToString();
    }

    private void OnUpdateUI()
    {
        OnHealthAdjust();
    }
}
