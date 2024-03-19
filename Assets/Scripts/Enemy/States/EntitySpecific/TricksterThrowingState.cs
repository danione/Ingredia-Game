using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterThrowingState : IState
{
    private TricksterMoveState moveState;
    private List<GameObject> capturedIngredients = new();
    private TricksterEnemy currentObject;
    private float currentTime = 0;
    private float maxTime = 3;
    private bool isFinished = false;

    public TricksterThrowingState(List<GameObject> capturedIngredients, TricksterEnemy currentObject, TricksterMoveState moveState)
    {
        this.capturedIngredients = capturedIngredients;
        this.currentObject = currentObject;
        this.moveState = moveState;
    }

    public void Enter()
    {
        currentTime = 0;
        isFinished = false;
        moveState.SetFinished(false);
        moveState.Enter();
    }

    public void Exit()
    {
        moveState.Exit();
    }

    private void Finished()
    {
        isFinished = true;
        moveState.SetFinished(true);
        GameEventHandler.Instance.FinishThrowingTrickster(currentObject);
    }

    void IState.Update()
    {
        moveState.Update();

        if (isFinished) { return; }

        if (maxTime - currentTime > 0)
        {
            currentTime += Time.deltaTime;
            return;
        }

        int i = Random.Range(0, capturedIngredients.Count);
        if(i < capturedIngredients.Count)
        {
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

        if (capturedIngredients.Count == 0)
        {
            Finished();
        }
    }
}
