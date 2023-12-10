using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory: MonoBehaviour
{
    [SerializeField] public int size = 0;
    [SerializeField] private int flameBombAmmo;
    [SerializeField] private int knifeAmmo;

    [SerializeField] private int maxFlameBombAmmo;
    [SerializeField] private int maxKnifeAmmo;

    [SerializeField] public int gold;

    private Dictionary<string, int> cauldronContents = new();
    private PlayerPotionsManager powerupManager;

    public IRitual possibleRitual;

    // Start is called before the first frame updates

    private void Start()
    {
        powerupManager = GetComponent<PlayerPotionsManager>();
        PlayerEventHandler.Instance.EmptiedCauldron += OnEmptiedCauldron;
        PlayerEventHandler.Instance.BenevolentRitualCompleted += OnRitualCompleted;
    }

    public void AddPotion(PotionsData potion)
    {
        powerupManager.AddPotion(potion);
    }

    public void AddToCauldron(IIngredient ingredient)
    {
        foreach (var ingr in cauldronContents)
        {
            if(cauldronContents.ContainsKey(ingredient.Data.ingredientName))
            {
                cauldronContents[ingredient.Data.ingredientName] += 1;
                PlayerEventHandler.Instance.CollectIngredient(ingredient, cauldronContents[ingredient.Data.ingredientName]);
                return;
            }
        }

        if (cauldronContents.Count < size)
        {
            cauldronContents[ingredient.Data.ingredientName] = 1;
            PlayerEventHandler.Instance.CollectIngredient(ingredient, cauldronContents[ingredient.Data.ingredientName]);
        }
    }

    private void OnEmptiedCauldron()
    {
        cauldronContents.Clear();
    }

    private void OnRitualCompleted(IRitual ritual)
    {
        Debug.Log(ritual);
        possibleRitual = ritual;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        PlayerEventHandler.Instance.CollectGold(gold);
    }

    public void AddFlameBombAmmo(int amount)
    {
        if (maxFlameBombAmmo >= flameBombAmmo + amount)
            flameBombAmmo += amount;
        else
            flameBombAmmo = maxFlameBombAmmo;
    }

    public void AddKnifeAmmo(int ammo)
    {
        if (maxKnifeAmmo >= knifeAmmo + ammo)
            knifeAmmo += ammo;
        else
            knifeAmmo = maxKnifeAmmo;
    }

    public int GetFlameBombAmmo() { return flameBombAmmo; }
    public int GetKnifeAmmo() {  return knifeAmmo; }

    public void SetMaxAmmo(int newMaxBombAmmo, int newMaxKnifeAmmo)
    {
        maxFlameBombAmmo = newMaxBombAmmo > 0 ? newMaxBombAmmo : maxFlameBombAmmo;
        maxKnifeAmmo = newMaxKnifeAmmo > 0 ? newMaxKnifeAmmo : maxKnifeAmmo;
    }

    public void SubtractAmmo()
    {
        if(flameBombAmmo > 0) flameBombAmmo--;
    }

    public void SubtractKnifeAmmo()
    {
        if (knifeAmmo > 0) knifeAmmo--;
    }

    public void UsePotion(int slot)
    {
        powerupManager.UsePotion(slot);
    }
}
