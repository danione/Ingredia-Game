using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RitualManager : MonoBehaviour
{
    [SerializeField] private List<RitualScriptableObject> defaultRituals = new();
    [SerializeField] private List<RitualScriptableObject> unlockedRituals = new();
    [SerializeField] private List<RitualScriptableObject> lockedRituals = new();

    private Dictionary<string, Ritual> unlockedRitualsDict = new();
    private Dictionary<string, Ritual> lockedRitualsDict = new ();

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        InitRituals();
    }

    private void InitRituals()
    {
        lockedRituals = lockedRituals.Except(defaultRituals).ToList();
        unlockedRituals.Clear();
        lockedRitualsDict.Clear();
        unlockedRitualsDict.Clear();

        foreach (var ritual in defaultRituals)
        {
            unlockedRituals.Add(ritual);
        }

        foreach (var ritual in unlockedRituals)
        {
            unlockedRitualsDict[ritual.ritualName] = new Ritual(ritual);
            unlockedRitualsDict[ritual.ritualName].EnableRitual();
        }

        foreach (var locked in lockedRituals)
        {
            if (locked.ritualName == null) continue;
            lockedRitualsDict[locked.ritualName] = new Ritual(locked);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene a, LoadSceneMode b)
    {
        InitRituals();
    }

    public List<RitualScriptableObject> GetAllRituals()
    {
        List<RitualScriptableObject> combinedList = new List<RitualScriptableObject>();

        // Add all elements from list1
        combinedList.AddRange(unlockedRituals);

        // Add all elements from list2
        combinedList.AddRange(lockedRituals);

        return combinedList;
    }


    // Valid rituals are if they are still present in the dict
    // and if the ritual within it is not null
    private bool IsValidRitual(string ritual, Dictionary<string, Ritual> dict)
    {
        bool isInLockedRitualsArray = dict.ContainsKey(ritual) && dict[ritual] != null;
        return isInLockedRitualsArray;
    }

    // Unlocks a ritual with the following identifier
    // if its valid
    // -- Uses enable ritual, which triggers the flag
    // inside the ritual to start working
    public RitualScriptableObject AddRitualToUnlocked(string newRitual)
    {
        RitualScriptableObject ritualObject = null;
        if (IsValidRitual(newRitual, lockedRitualsDict))
        {
            ritualObject = lockedRitualsDict[newRitual].RitualData;
            lockedRitualsDict[newRitual].EnableRitual();
            unlockedRitualsDict[newRitual] = lockedRitualsDict[newRitual];
            lockedRitualsDict.Remove(newRitual);
            GameEventHandler.Instance.UnlocksRitual(ritualObject);
        }
        return ritualObject;
    }

    // To be used only from the tutorial when we want the ritual to remain unlocked
    public void AddToDefaultUnlocked(RitualScriptableObject ritual)
    {
        if(!defaultRituals.Contains(ritual))
            defaultRituals.Add(ritual);
    }

    public bool IsUpgraded(string ritual)
    {
        if (unlockedRitualsDict.ContainsKey(ritual))
        {
            return true;
        } else if(defaultRituals.Any(ritualName => ritualName.ritualName == ritual))
        {
            return true;
        }
        return false;
    }

    public void ChangeYieldByIncrementing(string id)
    {
        if (IsValidRitual(id, lockedRitualsDict))
        {
            lockedRitualsDict[id].IncrementPotionYield();
        }
        else if (IsValidRitual(id, unlockedRitualsDict))
        {
            unlockedRitualsDict[id].IncrementPotionYield();
        }
        else
        {
            Debug.LogWarning("Ritual does not exist");
        }
    }
}
