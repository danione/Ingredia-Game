using System;
using System.Collections;
using UnityEngine;

public class RitualistEnemy : Enemy
{
    public static int RitualistEnemyCount = 0;

    private Transform player; // Used to detect ingredients near the player
    private Transform ritualistEnemyCapsule; // Reference to the actual capsule detector

    [SerializeField] private Transform ritualistCircle;
    [SerializeField] private Transform ingredientCircle;
    [SerializeField] public GameObject[] ingredientTypes;


    private RitualistStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new RitualistStateMachine(ritualistCircle, ingredientCircle);
        stateMachine.Initialise(stateMachine.LookoutState);
        stateMachine.LookoutState.FinishedChanneling += OnFinishedChanneling;
        stateMachine.SelectIngredientState.SwapPositionReady += OnSwapPositionReady;
    }

    private void Start()
    {
        player = PlayerController.Instance.gameObject.GetComponent<Transform>();
        ritualistEnemyCapsule = player.GetChild(1); // Second child should always be the ritualist circle
        if(ritualistEnemyCapsule.GetComponent<GetNearbyObjects>() == null)
        {
            Destroy(gameObject);
        }
        ritualistEnemyCapsule.gameObject.SetActive(true);

        RitualistEnemyCount++;
    }

    private void OnFinishedChanneling()
    {
        foreach (var ingredient in ritualistEnemyCapsule.GetComponent<GetNearbyObjects>().ingredients)
        {
            SwapIngredient(ingredient.Value);
        }
        stateMachine.TransitiontTo(stateMachine.IdleState);
        StartCoroutine(CooldownSwapPosition(stateMachine.SelectIngredientState));
    }

    IEnumerator CooldownSwapPosition(IState change)
    {
        yield return new WaitForSeconds(2);
        stateMachine.TransitiontTo(change);
    }

    private void OnSwapPositionReady(Transform target)
    {
        Vector3 tempPosition = transform.position;
        transform.position = target.position;
        target.position = tempPosition;

        stateMachine.TransitiontTo(stateMachine.IdleState);
        StartCoroutine(CooldownSwapPosition(stateMachine.LookoutState));
    }

    private void Update()
    {
        stateMachine.Update();
        
    }

    private void SwapIngredient(GameObject ingredient)
    {
        if(ingredient == null) { return; }
        Vector3 currentPos = ingredient.transform.position;
        GameObject newIngredient = ingredientTypes[UnityEngine.Random.Range(0, ingredientTypes.Length)];
        
        Destroy(ingredient);

        ingredient = Instantiate(newIngredient, currentPos, Quaternion.identity);
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed Enemy");
        stateMachine.LookoutState.FinishedChanneling -= OnFinishedChanneling;
        stateMachine.SelectIngredientState.SwapPositionReady -= OnSwapPositionReady;
        RitualistEnemyCount--;
        if(RitualistEnemyCount <= 0 && ritualistEnemyCapsule.gameObject != null)
        {
            ritualistEnemyCapsule.gameObject.SetActive(false);
        }
    }
}
