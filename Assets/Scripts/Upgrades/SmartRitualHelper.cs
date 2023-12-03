using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartRitualHelper : MonoBehaviour
{
    private bool isActive;

    private void Start()
    {
        isActive = false;
        GameEventHandler.Instance.ActivatedSmartRitualHelper += OnActivate;
    }

    private void OnActivate()
    {
        isActive = true;
    }
}
