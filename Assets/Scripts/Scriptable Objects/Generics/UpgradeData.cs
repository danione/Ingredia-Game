using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;
    public int goldCost;
    public int sophisticationCost;
    public abstract void ApplyUpgrade(GameObject obj);
}
