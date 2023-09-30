using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallableObject : MonoBehaviour, IMovable
{
    [SerializeField] protected float fallSpeed;

    public virtual void Move()
    {
        gameObject.transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
    }

    private void Update()
    {
        Move();
    }

    
}
