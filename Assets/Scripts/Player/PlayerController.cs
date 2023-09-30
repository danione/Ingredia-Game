using System;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(PlayerInventory), typeof(PlayerStats), typeof(PlayerPowerupManager))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private PlayerInventory inventory;
    private PlayerStats stats;
    private PlayerPowerupManager powerupManager;
    private PlayerInputHandler input;
    public Action<int> collision;

    public float cooldown;
    

    public int scoreMult = 5;
    // Start is called before the first frame update

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
    }

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        stats = GetComponent<PlayerStats>();
        powerupManager = GetComponent<PlayerPowerupManager>();
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
        powerupManager.HandlePowerups();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            default:
                break;
            case ("Ingredient"): inventory.AddToCauldron(other.GetComponent<IIngredient>()); break;
            case ("Recipe"): Debug.Log("TBA"); break;
            case ("Loot"): inventory.AddGold(105); break;

        }

        if (!other.CompareTag("Untagged") && !other.CompareTag("Protection Barrier"))
        {
            collision?.Invoke(other.gameObject.GetInstanceID());
            Destroy(other.gameObject);
        }

    }
}
