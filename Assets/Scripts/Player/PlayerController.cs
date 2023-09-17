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
        if (other.CompareTag("Ingredient"))
        {
            IIngredient ingredient = other.GetComponent<IIngredient>();
            inventory.AddToCauldron(ingredient);

        }
        else if (other.CompareTag("Recipe"))
        {
            Debug.Log(other.gameObject.GetComponent<Recipe>().type);
            BaseRecipe baseRecipe = RecipeFactory.CreateRecipe(other.gameObject.GetComponent<Recipe>().type);
            if (inventory != null)
            {
                inventory.recipes.Add(baseRecipe);
            }
            else
            {
                Debug.Log("Not initialised");
            }
        }
        Destroy(other.gameObject);
    }
}
