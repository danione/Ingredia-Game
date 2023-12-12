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

    // Get the unlocked scrolls and the total amount of scrolls available
    public KeyValuePair<int, int> GetScrollSlipsCount()
    {
        return new KeyValuePair<int, int>(unlockedScrollSlips.Count, availableScrollSlips.Count + unlockedScrollSlips.Count);
    }


    private void OnScrollSlipUnlock()
    {
        if(availableScrollSlips.Count == 0) return;

        RitualScriptableObject randomSlip = GetRandomScrollSlip();

        if (unlockedScrollSlips.Add(randomSlip))
        {
            GameEventHandler.Instance.ScrollSlipGenerate(randomSlip);
        }

    }

    private RitualScriptableObject GetRandomScrollSlip()
    {
        int randomIndex = Random.Range(0, availableScrollSlips.Count);
        RitualScriptableObject scroll = availableScrollSlips[randomIndex];
        availableScrollSlips.RemoveAt(randomIndex);
        return scroll;
    }
}
