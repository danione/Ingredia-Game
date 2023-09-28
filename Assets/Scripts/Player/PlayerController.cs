using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private PlayerMovement movement;
    private PlayerInventory inventory;
    private PlayerStats stats;
    private PlayerPowerupManager powerupManager;
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
        movement = GetComponent<PlayerMovement>();
        stats = GetComponent<PlayerStats>();
        powerupManager = GetComponent<PlayerPowerupManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver) return;
        movement.Move();
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
