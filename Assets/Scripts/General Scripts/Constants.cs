using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public static Constants Instance;
    [SerializeField] public DifficultyModifiers DifficultyModifiers = new();
    [SerializeField] public int currentRitualRewards { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        currentRitualRewards = 0;
    }

    public void UpgradeCurrentRitualRewards(int newRitualRewards)
    {
        currentRitualRewards = newRitualRewards;
    }
}

[System.Serializable]
public class DifficultyModifiers {
    [SerializeField] private float countWeight;
    public float CountWeight { get { return countWeight; } }
    [SerializeField] private float quantityWeight;
    public float QuantityWeight { get { return quantityWeight; } }
    [SerializeField] private float timeWeight;
    public float TimeWeight { get { return timeWeight; } }
}
