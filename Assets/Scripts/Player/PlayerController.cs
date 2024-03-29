using System;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public PlayerInventory inventory { get; private set; }
    [HideInInspector] public PlayerStats stats;
    private PlayerInputHandler input;
    public Action<int> collision;

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
        DontDestroyOnLoad(gameObject);

        SetUp();
    }

    void SetUp()
    {
        inventory = GetComponent<PlayerInventory>();
        stats = GetComponent<PlayerStats>();
        input = GetComponent<PlayerInputHandler>();

        if (input == null)
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
            case ("BansheeDetection"): GameEventHandler.Instance.BansheeDetectPlayer(); break;
            case ("Scroll Slip"): GameEventHandler.Instance.UnlockScrollSlip(); break;
        }

        if (other.CompareTag("Ingredient") || other.CompareTag("Recipe")  || other.CompareTag("Scroll Slip"))
        {
            collision?.Invoke(other.gameObject.GetInstanceID());
            GameEventHandler.Instance.DestroyObject(other.gameObject);
        }

    }
}
