using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resettable : MonoBehaviour
{
    private Action Reset;

    public void SetResetFunction(Action resetFunc)
    {
        Reset = resetFunc;
    }
    public virtual void ResetFunction() 
    { 
        Reset?.Invoke();
    }
}
