using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory: MonoBehaviour
{
    public int size = 0;
    public int ammo { get; private set; }
    public int gold { get; private set; }

    private Dictionary<string, int> cauldronContents = new();
    private PlayerPotionsManager powerupManager;

    private IRecipe currentRecipe;
    public IRitual possibleRitual;

    // Start is called before the first frame updates

    private void Start()
    {
        powerupManager = GetComponent<PlayerPotionsManager>();
        PlayerEventHandler.Instance.EmptiedCauldron += OnEmptiedCauldron;
        PlayerEventHandler.Instance.BenevolentRitualCompleted += OnRitualCompleted;
        ammo = 0;
    }

    private void Update()
    {
        powerupManager.HandlePotions();
    }

    public void AddPotion(IPotion potion)
    {
        powerupManager.AddPotion(potion);
    }

    public void AddToCauldron(IIngredient ingredient)
    {
        foreach (var ingr in cauldronContents)
        {
            if(cauldronContents.ContainsKey(ingredient.Name))
            {
                cauldronContents[ingredient.Name] += 1;
                PlayerEventHandler.Instance.CollectIngredient(ingredient, cauldronContents[ingredient.Name]);
                return;
            }
        }

        if (cauldronContents.Count < size)
        {
            cauldronContents[ingredient.Name] = 1;
            PlayerEventHandler.Instance.CollectIngredient(ingredient, cauldronContents[ingredient.Name]);
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

    // Have a look at that
    public void AddRecipe(IRecipe recipe)
    {
        currentRecipe = recipe;

        PlayerEventHandler.Instance.EmptyCauldron();

        PlayerEventHandler.Instance.CollectRecipe(currentRecipe);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        PlayerEventHandler.Instance.CollectGold(gold);
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;
    }

    public void SubtractAmmo()
    {
        if(ammo > 0)
        {
            ammo--;
        }
    }

    public void UsePotion(int slot)
    {
        powerupManager.UsePotion(slot);
    }
}
