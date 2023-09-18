using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerInventory inventory;
    private PlayerStats stats;

    public float cooldown;
    

    public int scoreMult = 5;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        movement = GetComponent<PlayerMovement>();
        stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver) return;
        movement.Move();
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

        Destroy(other.gameObject);
    }
}
