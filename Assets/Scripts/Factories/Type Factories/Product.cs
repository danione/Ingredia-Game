using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Resettable))]
public class Product : MonoBehaviour
{
    private IObjectPool<Product> objectPool;

    public IObjectPool<Product> ObjectPool { get { return objectPool; } set=> objectPool = value; }

    public void ResetObject() {
        gameObject.GetComponent<Resettable>().ResetFunction();
    }
}
