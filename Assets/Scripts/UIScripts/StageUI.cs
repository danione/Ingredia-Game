using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;


    private void Start()
    {
        GameEventHandler.Instance.StageChanged += OnStageChange;

    }

    private void OnDestroy()
    {
        try
        {
            GameEventHandler.Instance.StageChanged -= OnStageChange;
        }
        catch { }

    }

    private void OnStageChange(int currentStage)
    {
        m_TextMeshProUGUI.text = "Stage " + (currentStage + 1);
    }
}
