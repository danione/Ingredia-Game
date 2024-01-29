using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerInventory: MonoBehaviour
{
    [SerializeField] public int size = 0;
    [SerializeField] private int flameBombAmmo;
    [SerializeField] private int knifeAmmo;

    [SerializeField] private int maxFlameBombAmmo;
    [SerializeField] private int maxKnifeAmmo;

    [SerializeField] private List<Weapon> weapons;
    private int currentlyEquippedWeapon = 0;

    [SerializeField] public int gold;

    private Dictionary<string,int> cauldronContents = new();
    private PlayerPotionsManager powerupManager;

    public IRitual possibleRitual;

    // Start is called before the first frame updates

    private void Start()
    {
        powerupManager = GetComponent<PlayerPotionsManager>();
        PlayerEventHandler.Instance.EmptiedCauldron += OnEmptiedCauldron;
        PlayerEventHandler.Instance.BenevolentRitualCompleted += OnRitualCompleted;
    }

    public Weapon GetCurrentlyEquippedWeapon()
    {
       return weapons[currentlyEquippedWeapon];
    }

    public void SwitchEquippedWeapon()
    {
        currentlyEquippedWeapon = (currentlyEquippedWeapon + 1) % weapons.Count;
        Debug.Log(currentlyEquippedWeapon);

    }

    public void SubtractCurrentWeaponAmmo(int amount)
    {
        if (weapons[currentlyEquippedWeapon].ammo - amount > 0)
        {
            weapons[currentlyEquippedWeapon].ammo -= amount;
        }
        else { weapons[currentlyEquippedWeapon].ammo = 0; }
    }

    public void AddAmmo(string weaponName, int amount)
    {
        foreach (var weapon in weapons)
        {
            if (weaponName == weapon.weaponName) { weapon.ammo += amount; break; }
        }
    }

    public void AddPotion(PotionsData potion)
    {
        powerupManager.AddPotion(potion);
    }

    public void SetMaxAmmo(string weaponName, int newMaxAmmo)
    {
        foreach (var weapon in weapons)
        {
            if (weaponName == weapon.weaponName) { weapon.maxAmmo = newMaxAmmo; break; }
        }
    }

    public void AddToCauldron(IIngredient ingredient)
    {
        if (cauldronContents.Count >= size) return;

        if(cauldronContents.ContainsKey(ingredient.Data.ingredientName))
        {
            cauldronContents[ingredient.Data.ingredientName] += 1;
        } else if (cauldronContents.Count < size)
        {
            cauldronContents[ingredient.Data.ingredientName] = 1;
        }

        PlayerEventHandler.Instance.CollectIngredient(ingredient, cauldronContents[ingredient.Data.ingredientName]);
    }

    private void OnEmptiedCauldron()
    {
        cauldronContents.Clear();
    }

    private void OnRitualCompleted(IRitual ritual)
    {
        possibleRitual = ritual;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        PlayerEventHandler.Instance.CollectGold(gold);
    }

    public void UsePotion(int slot)
    {
        powerupManager.UsePotion(slot);
        InputEventHandler.instance.UsePotion();
    }
}
[System.Serializable]
public class Weapon
{
    [SerializeField] public string weaponName;
    [SerializeField] public int ammo;
    [SerializeField] public int maxAmmo;
    [SerializeField] private int objectPosition; // unique

    public int GetObjectPosition()
    {
        return objectPosition;
    }

    public bool HasAvailableAmmo()
    {
        return ammo > 0;
    }
}
