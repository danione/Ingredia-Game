using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallableObject : MonoBehaviour, IMovable
{
    [SerializeField] protected float fallSpeed = 10.0f;
    private Action currentMovementPattern;
    private Transform pivotObject;

    private void Start()
    {
        currentMovementPattern = Move;
    }

    public virtual void Move()
    {
        gameObject.transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
    }

    public void SwapToCirculate(Transform pivotObject)
    {
        this.pivotObject = pivotObject;
        currentMovementPattern = Circulate;
    }

    public void SwapToMove()
    {
        currentMovementPattern = Move;
    }

    private void Circulate()
    {
        gameObject.transform.RotateAround(pivotObject.position, new Vector3(0,0,1), fallSpeed * 30 * Time.deltaTime);
    }

    private void Update()
    {
        currentMovementPattern?.Invoke();
    }

    
}
