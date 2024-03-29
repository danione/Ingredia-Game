using UnityEngine;
[System.Serializable]
public class Weapon
{
    [SerializeField] public string weaponName;
    [SerializeField] public int ammo;
    [SerializeField] private int objectPosition; // unique
    [SerializeField] private bool isUnlimited;
    public bool IsUnlimited => isUnlimited;

    public int GetObjectPosition()
    {
        return objectPosition;
    }

    public bool HasAvailableAmmo()
    {
        return ammo > 0;
    }

    public void SetUnlimited()
    {
        ammo = int.MaxValue;
        isUnlimited = true;
    }

    public void Reset()
    {
        ammo = 0;
        isUnlimited = false;
    }
}