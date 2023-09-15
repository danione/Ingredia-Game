using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float leftBorder;
    public float rightBorder;
    public float cooldown;
    public GameObject spawningProjectilePoint;
    public GameObject projectile;
    public PlayerInventory inventory;

    public int scoreMult = 5;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver) return;
        Move();
        Shoot();
        
    }

    void Move()
    {    
        float goingLeft = Input.GetAxis("Horizontal");

        if(transform.position.x > leftBorder)
        {
            transform.position = new Vector3(leftBorder, transform.position.y, transform.position.z);
        } else if (transform.position.x < rightBorder)
        {
            transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
        }else
        {
            transform.Translate(movementSpeed * goingLeft * Time.deltaTime * Vector3.left);
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.gameOver)
        {
            Instantiate(projectile, spawningProjectilePoint.transform.position, projectile.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            int accepted = inventory.CookIngredient(gameObject.name);
            if (accepted >= 0)
            {
                GameManager.Instance.AddScore(scoreMult);
            }
            else
            {
                GameManager.Instance.AddScore(-scoreMult * 2);
            }
        }

        if (other.CompareTag("Recipe"))
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
