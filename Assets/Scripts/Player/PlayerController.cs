using System;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(PlayerInventory), typeof(PlayerStats), typeof(PlayerPotionsManager))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public PlayerInventory inventory { get; private set; }
    public PlayerStats stats;
    private PlayerInputHandler input;
    public Action<int> collision;
    public float cooldown;

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

        SetUp();
    }

    void SetUp()
    {
        inventory = GetComponent<PlayerInventory>();
        stats = GetComponent<PlayerStats>();
        input = GetComponent<PlayerInputHandler>();

        if(input == null)
        {
            gameObject.AddComponent<PlayerInputHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver) return;

        input.HandleInput();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            default:
                break;
            case ("Ingredient"): inventory.AddToCauldron(other.GetComponent<IIngredient>()); break;
            case ("Recipe"): PlayerEventHandler.Instance.CollidedWithRecipeObject();  break;
            case ("Loot"): inventory.AddGold(other.GetComponent<GoldenNuggets>().Amount); break;
            case ("BansheeDetection"): GameEventHandler.Instance.BansheeDetectPlayer(); break;
        }

        if (other.CompareTag("Ingredient") || other.CompareTag("Recipe") || other.CompareTag("Projectile") || other.CompareTag("Loot"))
        {
            collision?.Invoke(other.gameObject.GetInstanceID());
            Destroy(other.gameObject);
        }

    }
}
