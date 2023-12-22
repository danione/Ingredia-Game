using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSlipManager : MonoBehaviour
{
    private List<RitualScriptableObject> unlockedScrollSlips = new();
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

        unlockedScrollSlips.Add(randomSlip);
        
        GameEventHandler.Instance.ScrollSlipGenerate(randomSlip);
    }

    public int GetAvailableSlipsCount()
    {
        return availableScrollSlips.Count;
    }

    public RitualScriptableObject GetSlipByIndex(int index)
    {
        if (index >= 0 && index < unlockedScrollSlips.Count)
        {
            return unlockedScrollSlips[index];
        }
        else
        {
            return null;
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
