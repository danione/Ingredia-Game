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
    private List<IRitual> basicRituals = new ();
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
        bool isInHiddenRitualsArray = hiddenRituals.ContainsKey(ritual) && hiddenRituals[ritual] != null && hiddenRituals[ritual].isEnabled;
        bool isInLockedRitualsSet = lockedHiddenRituals.Contains(ritual);
        return isInHiddenRitualsArray && isInLockedRitualsSet;
    }

    public void AddRitual(string newRitual)
    {
        if (IsValidRitual(newRitual))
        {
            hiddenRituals[newRitual].EnableRitual();
            lockedHiddenRituals.Remove(newRitual);
        }
    }

    public void RemoveRitual(string newRitual)
    {
        if (IsValidRitual(newRitual))
        {
            hiddenRituals[newRitual].EnableRitual();
            lockedHiddenRituals.Add(newRitual);
        }
    }

    public bool HasLockedHiddenRituals()
    {
        return lockedHiddenRituals.Count > 0;
    }

    public HiddenRitual GetRandomLockedRitual()
    {
        if (!HasLockedHiddenRituals()) return null;

        string[] asArray = lockedHiddenRituals.ToArray();
        int randomIndex = Random.Range(0, asArray.Length);
        string randomElement = asArray[randomIndex];
        return new HiddenRitual(randomElement);
    } 
}
