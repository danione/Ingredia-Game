using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollSlipManager : MonoBehaviour
{
    private List<RitualScriptableObject> unlockedScrollSlips = new();
    private List<RitualScriptableObject> availableScrollSlips = new();
    private bool isTutorial = false;


    private void Start()
    {
        GameEventHandler.Instance.UnlockedScrollSlip += OnScrollSlipUnlock;
        GameEventHandler.Instance.SetTutorialMode += OnSetTutorialMode;
        availableScrollSlips = GetComponent<RitualManager>().GetAllRituals();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.UnlockedScrollSlip -= OnScrollSlipUnlock;
        GameEventHandler.Instance.SetTutorialMode -= OnSetTutorialMode;
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene a, LoadSceneMode b)
    {
        if(a.name == "Normal Level")
        {
            isTutorial = false;
        }
    }

    public bool IsScrollUnlocked(RitualScriptableObject scroll)
    {
        return unlockedScrollSlips.Contains(scroll);
    }

    public void OnSetTutorialMode()
    {
        isTutorial = true;
    }

    // Get the unlocked scrolls and the total amount of scrolls available
    public KeyValuePair<int, int> GetScrollSlipsCount()
    {
        return new KeyValuePair<int, int>(unlockedScrollSlips.Count, availableScrollSlips.Count + unlockedScrollSlips.Count);
    }

    public void AddRitual(RitualScriptableObject newUnlockedRitual)
    {
        if(newUnlockedRitual == null) { return; }

        availableScrollSlips.Add(newUnlockedRitual);
    }


    private void OnScrollSlipUnlock()
    {
        if(availableScrollSlips.Count == 0) return;

        RitualScriptableObject randomSlip;

        if (isTutorial)
        {
            randomSlip = GetSlipForTutorial();
        }
        else
        {
            randomSlip = GetRandomScrollSlip();
        }

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

    private RitualScriptableObject GetSlipForTutorial()
    {
        RitualScriptableObject scroll = availableScrollSlips[0];
        availableScrollSlips.RemoveAt(0);
        return scroll;
    }

    private RitualScriptableObject GetRandomScrollSlip()
    {
        int randomIndex = Random.Range(0, availableScrollSlips.Count);
        RitualScriptableObject scroll = availableScrollSlips[randomIndex];
        availableScrollSlips.RemoveAt(randomIndex);
        return scroll;
    }
}
