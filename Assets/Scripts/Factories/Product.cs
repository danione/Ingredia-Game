using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Product : MonoBehaviour
{
    private IObjectPool<Product> objectPool;

    public IObjectPool<Product> ObjectPool { get { return objectPool; } set=> objectPool = value; }
}
