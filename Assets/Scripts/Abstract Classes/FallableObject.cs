using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallableObject : MonoBehaviour, IMovable
{
    [SerializeField] protected float fallSpeed = 10.0f;

    public virtual void Move()
    {
        gameObject.transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
    }

    private void Update()
    {
        Move();
    }

    
}
