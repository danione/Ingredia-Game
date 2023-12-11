using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSlipUIManager : MonoBehaviour
{
    [SerializeField] private Transform scrollSlipPopup;
    private void Start()
    {
        GameEventHandler.Instance.ScrollSlipGenerated += OnScrollSlipGenerated;
    }


    private void OnScrollSlipGenerated()
    {
        scrollSlipPopup.gameObject.SetActive(true);
        GameManager.Instance.PauseGame();
    }


}
