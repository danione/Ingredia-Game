using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterThrowingState : IState
{
    private List<GameObject> capturedIngredients = new();
    private TricksterEnemy currentObject;
    private float currentTime = 0;
    private float maxTime = 3;

    public TricksterThrowingState(List<GameObject> capturedIngredients, TricksterEnemy currentObject)
    {
        this.capturedIngredients = capturedIngredients;
        this.currentObject = currentObject;
    }

    public void Enter()
    {
        currentTime = 0;
    }

    public void Exit()
    {
    }


    void IState.Update()
    {
        if (capturedIngredients.Count == 0)
            return;

        if(maxTime - currentTime > 0)
        {
            currentTime += Time.deltaTime;
            return;
        } 

        int i = Random.Range(0, capturedIngredients.Count);
        FallableObject picked = capturedIngredients[i].GetComponent<FallableObject>();
        Vector3 direction = PlayerController.Instance.transform.position - currentObject.transform.position;

        // Calculate the rotation needed to look at the target object
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the target rotation
        currentObject.gameObject.transform.rotation = Quaternion.Slerp(currentObject.gameObject.transform.rotation, targetRotation, 5 * Time.deltaTime);

        picked.SwapToMove();

        capturedIngredients.RemoveAt(i);

        currentTime = 0;

    }
}
