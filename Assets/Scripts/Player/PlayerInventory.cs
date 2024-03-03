using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory: MonoBehaviour
{
    [SerializeField] public int size = 0;

    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private List<Weapon> lockedWeapons;
    private int currentlyEquippedWeapon = 0;

    [SerializeField] public int gold = 0;
    [SerializeField] public float sophistication = 0;

    private Dictionary<string,int> cauldronContents = new();
    private PlayerPotionsManager powerupManager;

    public IRitual possibleRitual;

    // Start is called before the first frame updates

    private void Start()
    {
        powerupManager = GetComponent<PlayerPotionsManager>();
        PlayerEventHandler.Instance.EmptiedCauldron += OnEmptiedCauldron;
        PlayerEventHandler.Instance.BenevolentRitualCompleted += OnRitualCompleted;
        GameEventHandler.Instance.SpawnGoldenNugget += AddGold;
    }

    private void OnDestroy()
    {
        try
        {
            PlayerEventHandler.Instance.BenevolentRitualCompleted -= OnRitualCompleted;
            PlayerEventHandler.Instance.EmptiedCauldron -= OnEmptiedCauldron;
            GameEventHandler.Instance.SpawnGoldenNugget -= AddGold;

        }
        catch { }
    }

    public void SetUnlimitedWeapon(string weaponName)
    {
        foreach (var weapon in weapons)
        {
            if (weaponName == weapon.weaponName) { weapon.SetUnlimited(); }
        }
    }

    public Weapon GetCurrentlyEquippedWeapon()
    {
       return weapons[currentlyEquippedWeapon];
    }

    public void SwitchEquippedWeapon()
    {
        currentlyEquippedWeapon = (currentlyEquippedWeapon + 1) % weapons.Count;
    }

    public void SubtractCurrentWeaponAmmo(int amount)
    {
        if (weapons[currentlyEquippedWeapon].IsUnlimited) return;

        if (weapons[currentlyEquippedWeapon].ammo - amount > 0)
        {
            weapons[currentlyEquippedWeapon].ammo -= amount;
        }
        else { weapons[currentlyEquippedWeapon].ammo = 0; }
    }

    public void AddAmmo(string weaponName, int amount)
    {
        if (weapons[currentlyEquippedWeapon].weaponName == weaponName && weapons[currentlyEquippedWeapon].IsUnlimited) return;

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
        if (weapons[currentlyEquippedWeapon].weaponName == weaponName && weapons[currentlyEquippedWeapon].IsUnlimited) return;

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

    public void AddGold(int amount, Vector3 atPos = new Vector3())
    {
        gold += amount;
        PlayerEventHandler.Instance.CollectGold(gold);
    }

    public void UsePotion(int slot)
    {
        powerupManager.UsePotion(slot);
        InputEventHandler.instance.UsePotion();
    }

    public bool AdjustSophistication(float amount)
    {
        if(amount + sophistication > 0)
        {
            sophistication += amount;
            PlayerEventHandler.Instance.CollectSophistication((int)amount);
            return true;
        }

        return false;
    }

    public float GetSophistication()
    {
        return sophistication;
    }
}

