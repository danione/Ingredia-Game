using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallableObject : MonoBehaviour, IMovable
{
    [SerializeField] protected float fallSpeed = 10.0f;
    private Action currentMovementPattern;
    private Transform pivotObject;
    private Vector3 rotationDirection;
    private bool isRotating = false;
    public bool IsRotating => isRotating;

    public virtual void Move()
    {
        gameObject.transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
    }

    public void SwapToCirculate(Transform pivotObject, Vector3 direction)
    {
        this.pivotObject = pivotObject;
        this.rotationDirection = direction;
        isRotating = true;
        currentMovementPattern = Circulate;
    }

    public virtual void SwapToFreeze()
    {
        isRotating = false;
        currentMovementPattern = null;
    }

    public void SwapToMove()
    {
        isRotating = false;
        currentMovementPattern = Move;
        transform.rotation = Quaternion.identity;
    }

    protected void Circulate()
    {
        if(!gameObject.activeSelf || gameObject == null) SwapToFreeze();
            gameObject.transform.RotateAround(pivotObject.position, rotationDirection, fallSpeed * 30 * Time.deltaTime);
    }

    private void Update()
    {
        currentMovementPattern?.Invoke();
    }
}
