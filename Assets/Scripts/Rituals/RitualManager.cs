using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RitualManager : MonoBehaviour
{

    [SerializeField] private RitualScriptableObject healingRitualData;
    [SerializeField] private RitualScriptableObject firespitterRitualData;
    [SerializeField] private RitualScriptableObject protectionRitualData;
    [SerializeField] private RitualScriptableObject steelspitterRitualData;
    [SerializeField] private RitualScriptableObject goldenRitualData;
    [SerializeField] private RitualScriptableObject overloadRitualData;
    [SerializeField] private RitualScriptableObject ghostRitualData;
    private List<IRitual> basicRituals = new();
    private Dictionary<string, Ritual> hiddenRituals = new ();
    private HashSet<string> lockedHiddenRituals = new ();

    private void Start()
    {
        basicRituals.Add(new HealingRitual(healingRitualData));
        basicRituals.Add(new FireSpitterRitual(firespitterRitualData));
        basicRituals.Add(new ProtectionRitual(protectionRitualData));
        basicRituals.Add(new SteelSpitterRitual(steelspitterRitualData));

        hiddenRituals[goldenRitualData.name] = new GoldenRitual(goldenRitualData);
        hiddenRituals[overloadRitualData.name] = new OverloadRitual(overloadRitualData);
        hiddenRituals[ghostRitualData.name] = new GhostProtectionRitual(ghostRitualData);

        lockedHiddenRituals = hiddenRituals.Keys.ToHashSet();

        foreach (Ritual ritual in basicRituals)
        {
            ritual.EnableRitual();
        }
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
