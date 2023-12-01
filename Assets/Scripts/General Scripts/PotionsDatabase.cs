using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsDatabase : MonoBehaviour
{
    public static PotionsDatabase Instance;
    [SerializeField] private Transform goldenNugget;
    private Dictionary<string, IPotion> potionsDatabase = new Dictionary<string, IPotion>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // potionsDatabase["FireSpitterElixir"] = new FireSpitterElixir();
        // potionsDatabase["GhostPotion"] = new GhostPotion(PlayerController.Instance.gameObject.transform);
        // potionsDatabase["GoldenTicketElixir"] = new GoldenTicketElixir(goldenNugget);
        // potionsDatabase["HealingPotion"] = new HealingPotion();
        potionsDatabase["OverloadElixir"] = new OverloadElixir();
        potionsDatabase["ProtectionElixir"] = new ProtectionElixir();
        // potionsDatabase["SteelSpitterElixir"] = new SteelSpitterElixir();
    }

    public bool AddEntry(IPotion potion)
    {
        bool hasBeenAdded = false;

        if (potionsDatabase.ContainsKey(potion.ToString()))
        {
            potionsDatabase[potion.ToString()] = potion;
            hasBeenAdded = true;
        }

        return hasBeenAdded;
    }

    public IPotion GetPotion(string potionName)
    {
        if (potionsDatabase.ContainsKey(potionName))
        {
            return potionsDatabase[potionName];
        }

        return null;

    }
}
