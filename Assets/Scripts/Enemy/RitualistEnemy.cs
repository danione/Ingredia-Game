using System;
using System.Collections;
using UnityEngine;

public class RitualistEnemy : Enemy
{
    [SerializeField] private Transform ritualistCircle;
    [SerializeField] private Transform ingredientCircle;
    [SerializeField] private float radiusOfSwap;
    [SerializeField] private float cooldownSwapSelectSeconds;
    [SerializeField] private float damageInCircle;

    private RitualistStateMachine stateMachine;

    private void OnFinishedChanneling(Transform circle)
    {
        if (circle != ritualistCircle) return;

        if(ritualistCircle.GetComponent<DetectionCircle>()?.hasPlayer ?? false)
        {
            PlayerController.Instance.GetComponent<PlayerStats>().TakeDamage(damageInCircle);
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
        int rand = UnityEngine.Random.Range(0, 2);

        if (rand == 0)
        {
            SwapPos(target);

        }
        else
        {
            GameEventHandler.Instance.SpawnATricksterProjectileAt(target.position, null);
            GameEventHandler.Instance.DestroyObject(target.gameObject);
        }

        stateMachine.TransitiontTo(stateMachine.IdleState);
        StartCoroutine(CooldownSwapPosition(stateMachine.LookoutState));
    }

    private void SwapPos(Transform target)
    {
        Vector3 tempPosition = transform.position;
        transform.position = target.position;
        target.position = tempPosition;
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public override void DestroyEnemy()
    {
        PlayerEventHandler.Instance.RitualistFinishedChanneling -= OnFinishedChanneling;
        stateMachine.SelectIngredientState.SwapPositionReady -= OnSwapPositionReady;
        ingredientCircle.gameObject.SetActive(false);
        base.DestroyEnemy();
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        if(stateMachine == null)
            stateMachine = new RitualistStateMachine(ritualistCircle, ingredientCircle);
        stateMachine.Initialise(stateMachine.LookoutState);
        PlayerEventHandler.Instance.RitualistFinishedChanneling += OnFinishedChanneling;
        stateMachine.SelectIngredientState.SwapPositionReady += OnSwapPositionReady;
    }
}
