using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Stage", menuName = "Scriptable Objects/Spawn Data/Stage")]
public class SpawnStage : ScriptableObject
{
    public List<WaveEnemy> enemyList;
    public bool isRandom = true;
}


[System.Serializable]
public class WaveEnemy
{
    public Product enemy;
    public int amount;
}
