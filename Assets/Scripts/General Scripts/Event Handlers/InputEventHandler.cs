using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventHandler : MonoBehaviour
{
    public static InputEventHandler instance;
    public Action<bool> MovementInputTampering;
    public Action MoveRandomly;
    public Action PickDirection;
    public Action<float> MoveTowardsTarget;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMovement(bool isMoving)
    {
        MovementInputTampering?.Invoke(isMoving);
    }

    public void MovePlayerRandomly()
    {
        MoveRandomly?.Invoke();
    }

    public void PickRandomDirection()
    {
        PickDirection?.Invoke();
    }

    public void MoveTowards(float direction)
    {
        MoveTowardsTarget?.Invoke(direction);
    }
}
