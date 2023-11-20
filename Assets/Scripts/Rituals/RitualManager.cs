using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RitualManager : MonoBehaviour
{
    [SerializeField] private List<RitualScriptableObject> basicRitualsData = new();
    [SerializeField] private List<RitualScriptableObject> hiddenRitualsData = new();

    private List<Ritual> basicRituals = new();
    private Dictionary<string, Ritual> hiddenRituals = new ();
    private HashSet<string> lockedHiddenRituals = new ();

    private void Start()
    {
        foreach(var ritual in basicRitualsData)
        {
            basicRituals.Add(new Ritual(ritual, PotionsDatabase.Instance.GetPotion(ritual.reward)));
            basicRituals[basicRituals.Count - 1].EnableRitual();
        }

        foreach(var hidden in hiddenRitualsData)
        {
             hiddenRituals[hidden.name] = new Ritual(hidden, PotionsDatabase.Instance.GetPotion(hidden.reward));
        }

        lockedHiddenRituals = hiddenRituals.Keys.ToHashSet();
    }

    private bool IsValidRitual(string ritual)
    {
        bool isInHiddenRitualsArray = hiddenRituals.ContainsKey(ritual) && hiddenRituals[ritual] != null;
        bool isInLockedRitualsSet = lockedHiddenRituals.Contains(ritual);
        return isInHiddenRitualsArray && isInLockedRitualsSet;
    }

    // The ritual won't be selected, this is considered permanent
    // unlocking
    public void AddRitualToUnlocked(string newRitual)
    {
        if (IsValidRitual(newRitual) && !hiddenRituals[newRitual].isEnabled)
        {
            hiddenRituals[newRitual].EnableRitual();
            lockedHiddenRituals.Remove(newRitual);
        }
    }

    // The ritual won't be selected, this is considered permanent
    // locking
    // Would not work, will have to be modified !!!!!!
    public void RemoveRitualFromUnlocked(string newRitual)
    {
        if (IsValidRitual(newRitual) && hiddenRituals[newRitual].isEnabled)
        {
            hiddenRituals[newRitual].DisableRitual();
            lockedHiddenRituals.Add(newRitual);
        }
    }

    // Temporary Unlocking
    public void UnlockRitual(string ritual)
    {
        if (IsValidRitual(ritual) && !hiddenRituals[ritual].isEnabled)
        {
            hiddenRituals[ritual].EnableRitual();
        }
    }

    // Temporary Locking
    public void LockRitual(string ritual)
    {
        if (IsValidRitual(ritual) && hiddenRituals[ritual].isEnabled)
        {
            hiddenRituals[ritual].DisableRitual();
        }
    }

    public bool HasLockedHiddenRituals()
    {
        return lockedHiddenRituals.Count > 0;
    }

    public string GetRandomLockedRitual()
    {
        if (!HasLockedHiddenRituals()) return null;
   
        string[] asArray = lockedHiddenRituals.ToArray();
        int randomIndex = Random.Range(0, asArray.Length);
        string randomElement = asArray[randomIndex];
        return randomElement;
    } 

    public RitualScriptableObject GetRitualScriptableObject(string ritual)
    {
        if (!IsValidRitual(ritual)) return null;

        return hiddenRituals[ritual].RitualData;
    }
}
