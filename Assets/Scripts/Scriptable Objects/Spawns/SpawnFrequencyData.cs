using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Location", menuName = "Scriptable Objects/Spawn Data/Frequency")]
public class SpawnFrequencyData : ScriptableObject
{
    public float minFrequency;
    public float maxFrequency;
    public float spawnChance;
    public float spawnChanceIncrease;
    public float spawnFrequency;
}
