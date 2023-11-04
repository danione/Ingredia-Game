using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RitualManager : MonoBehaviour
{
    [SerializeField] private RitualScriptableObject healingRitualData;
    [SerializeField] private RitualScriptableObject firespitterRitualData;
    private List<IRitual> rituals = new List<IRitual>();

    private void Start()
    {
        rituals.Add(new HealingRitual(healingRitualData));
        rituals.Add(new FireSpitterRitual(firespitterRitualData));
    }
}
