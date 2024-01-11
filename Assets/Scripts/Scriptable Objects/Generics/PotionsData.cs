using UnityEngine;

public abstract class PotionsData : ScriptableObject
{
    public string potionName;
    public abstract void UsePotion();
}
