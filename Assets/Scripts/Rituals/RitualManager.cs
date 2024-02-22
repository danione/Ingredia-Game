using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RitualManager : MonoBehaviour
{
    [SerializeField] private List<RitualScriptableObject> unlockedRituals = new();
    [SerializeField] private List<RitualScriptableObject> lockedRituals = new();

    private List<Ritual> unlockedRitualsList = new();
    private Dictionary<string, Ritual> lockedRitualsDict = new ();

    private void Start()
    {
        foreach(var ritual in unlockedRituals)
        {
            unlockedRitualsList.Add(new Ritual(ritual));
            unlockedRitualsList[unlockedRitualsList.Count - 1].EnableRitual();
        }
               
        foreach(var hidden in lockedRituals)
        {
             lockedRitualsDict[hidden.name] = new Ritual(hidden);
        }
    }


    // Valid rituals are if they are still present in the locked dict
    // and if the ritual within it is not null
    private bool IsValidRitual(string ritual)
    {
        bool isInHiddenRitualsArray = lockedRitualsDict.ContainsKey(ritual) && lockedRitualsDict[ritual] != null;
        return isInHiddenRitualsArray;
    }

    // Unlocks a ritual with the following identifier
    // if its valid
    // -- Uses enable ritual, which triggers the flag
    // inside the ritual to start working
    public RitualScriptableObject AddRitualToUnlocked(string newRitual)
    {
        RitualScriptableObject ritualObject = null;
        if (IsValidRitual(newRitual))
        {
            ritualObject = lockedRitualsDict[newRitual].RitualData;
            lockedRitualsDict[newRitual].EnableRitual();
        }
        return ritualObject;
    }
}
