using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventHandler : MonoBehaviour
{
    public static InputEventHandler instance;
    public Action<bool> MovementInputTampering;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = new InputEventHandler();
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetMovement(bool isMoving)
    {
        MovementInputTampering?.Invoke(isMoving);
    }
}
