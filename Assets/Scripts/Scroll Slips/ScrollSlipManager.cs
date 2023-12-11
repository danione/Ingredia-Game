using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSlipManager : MonoBehaviour
{
    private HashSet<RitualScriptableObject> unlockedScrollSlips = new();
    [SerializeField] private List<RitualScriptableObject> availableScrollSlips = new();


    private void Start()
    {
        GameEventHandler.Instance.UnlockedScrollSlip += OnScrollSlipUnlock;
    }


    private void OnScrollSlipUnlock()
    {
        if(availableScrollSlips.Count == 0) return;

        unlockedScrollSlips.Add(GetRandomScrollSlip());
    }

    private RitualScriptableObject GetRandomScrollSlip()
    {
        int randomIndex = Random.Range(0, availableScrollSlips.Count);
        RitualScriptableObject scroll = availableScrollSlips[randomIndex];
        availableScrollSlips.RemoveAt(randomIndex);
        return scroll;
    }
}
