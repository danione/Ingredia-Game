using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory: MonoBehaviour
{
    public int size = 0;
    public int ammo { get; private set; }
    public int gold { get; private set; }

    private Dictionary<string, int> cauldronContents;
    [SerializeField] private IngredientCombos combos;
    private IRecipe currentRecipe;
    public IRitual possibleRitual;

    // Start is called before the first frame updates

    private void Start()
    {
        PlayerEventHandler.Instance.EmptiedCauldron += OnEmptiedCauldron;
        PlayerEventHandler.Instance.BenevolentRitualCompleted += OnRitualCompleted;
        cauldronContents = new Dictionary<string, int>();
        ammo = 0;
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
            } else if (!combos.CheckIngredientCombos(ingredient.Name, ingr.Key))
            {
                cauldronContents.Clear();
                PlayerEventHandler.Instance.CollectIngredient(null, 0);
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
}
