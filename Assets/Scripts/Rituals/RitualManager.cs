using System.Collections.Generic;
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

    private void Awake()
    {
        basicRituals.Add(new HealingRitual(healingRitualData));
        basicRituals.Add(new FireSpitterRitual(firespitterRitualData));
        basicRituals.Add(new ProtectionRitual(protectionRitualData));
        basicRituals.Add(new SteelSpitterRitual(steelspitterRitualData));

        hiddenRituals[goldenRitualData.name] = new GoldenRitual(goldenRitualData);
        hiddenRituals[overloadRitualData.name] = new OverloadRitual(overloadRitualData);
        hiddenRituals[ghostRitualData.name] = new GhostProtectionRitual(ghostRitualData);
    }

    private void Start()
    {
        foreach(Ritual ritual in basicRituals)
        {
            ritual.EnableRitual();
        }
    }

    public void AddRitual(string newRitual)
    {
        if (hiddenRituals.ContainsKey(newRitual) && hiddenRituals[newRitual] != null && hiddenRituals[newRitual].isEnabled)
        {
            hiddenRituals[newRitual].EnableRitual();
        }
    }
}
