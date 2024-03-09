using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SophisticationUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sophistication;
    private PlayerInventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = PlayerController.Instance.inventory;
        PlayerEventHandler.Instance.CollectedSophistication += OnSophisticationAdjust;
        GameEventHandler.Instance.UpdatedUI += OnUpdateUI;
        OnSophisticationAdjust();
    }

    private void OnDestroy()
    {
        PlayerEventHandler.Instance.CollectedSophistication -= OnSophisticationAdjust;
        GameEventHandler.Instance.UpdatedUI -= OnUpdateUI;
    }

    private void OnSophisticationAdjust(int amount = 0)
    {
        sophistication.text = "Sophistication " + Mathf.FloorToInt(inventory.sophistication);
    }

    private void OnUpdateUI()
    {
        OnSophisticationAdjust();
    }

}

