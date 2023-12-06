using System;
using System.Collections;
using UnityEngine;

public class RitualistEnemy : Enemy
{
    public static int RitualistEnemyCount = 0;

    private Transform player; // Used to detect ingredients near the player

    [SerializeField] private Transform ritualistCircle;
    [SerializeField] private Transform ingredientCircle;
    [SerializeField] private float radiusOfSwap;
    [SerializeField] private float cooldownSwapSelectSeconds;

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
        RitualistEnemyCount++;
    }

    private void OnFinishedChanneling()
    {
        Collider[] colliders = Physics.OverlapSphere(player.gameObject.transform.position, radiusOfSwap);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player")) continue;

            if (collider.CompareTag("Ingredient"))
            {
                SwapIngredient(collider.gameObject);
            }
        }

        stateMachine.TransitiontTo(stateMachine.IdleState);
        StartCoroutine(CooldownSwapPosition(stateMachine.SelectIngredientState));
    }

    IEnumerator CooldownSwapPosition(IState change)
    {
        yield return new WaitForSeconds(cooldownSwapSelectSeconds);
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

        Destroy(ingredient);

        GameEventHandler.Instance.GenerateIngredientAtPos(currentPos);
    }

    protected override void DestroyEnemy()
    {
        stateMachine.LookoutState.FinishedChanneling -= OnFinishedChanneling;
        stateMachine.SelectIngredientState.SwapPositionReady -= OnSwapPositionReady;
        RitualistEnemyCount--;
    }
}
