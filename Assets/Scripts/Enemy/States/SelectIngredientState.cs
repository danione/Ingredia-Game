using System;
using UnityEngine;

public class SelectIngredientState : IState
{
    private Transform ingredientCircle;
    private float xBorder = 13.0f;
    private float yBorderUpper = 4.0f;
    private float yBorderLower = 2.5f;
    private float stoppingDistance = 1.0f;
    private float speed = 3.0f;
    public Action<Transform> SwapPositionReady;

    public SelectIngredientState(Transform ingredientCircle)
    {
        this.ingredientCircle = ingredientCircle;
    }

    public void Enter()
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-xBorder, xBorder),UnityEngine.Random.Range(yBorderLower,yBorderUpper),ingredientCircle.position.z);
        ingredientCircle.gameObject.SetActive(true);
        ingredientCircle.transform.position = position;
    }

    public void Exit()
    {
        ingredientCircle.gameObject.SetActive(false);
    }

    public void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Ingredient");

        if(targets.Length == 0 ) { return; }

        Transform target = FindNearestTarget(targets);

        if(target == null) { return; }
        Move(target);
    }

    private void Move(Transform target)
    {
        Vector3 targetDirection = target.position - ingredientCircle.position;
        float distance = targetDirection.magnitude;

        if (distance > stoppingDistance)
        {
            Vector3 desiredVelocity = targetDirection.normalized;

            // Apply the steering force to move the object.
            ingredientCircle.position += desiredVelocity * speed * Time.deltaTime;
        }
        else
        {
            SwapPositionReady?.Invoke(target);
        }
    }

    private Transform FindNearestTarget(GameObject[] targets)
    {
        float closestDistance = float.MaxValue;
        Transform closestTarget = null;

        foreach( GameObject target in targets )
        {
            float distance = Vector3.Distance(ingredientCircle.position, target.transform.position);
            if( distance < closestDistance )
            {
                closestDistance = distance;
                closestTarget = target.transform;
            }
        }

        return closestTarget;
    }
}
