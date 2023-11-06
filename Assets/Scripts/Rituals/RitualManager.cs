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
    private List<IRitual> rituals = new List<IRitual>();

    private void Start()
    {
        rituals.Add(new HealingRitual(healingRitualData));
        rituals.Add(new FireSpitterRitual(firespitterRitualData));
        rituals.Add(new ProtectionRitual(protectionRitualData));
        rituals.Add(new SteelSpitterRitual(steelspitterRitualData));
        rituals.Add(new GoldenRitual(goldenRitualData));
    }
}
