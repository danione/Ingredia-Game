using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float upperYBound = 13.0f;
    private float lowerYBound = -13.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < lowerYBound || transform.position.y > upperYBound)
        {
            Product product = gameObject.GetComponent<Product>();
            if (product != null)
            {
                product.ObjectPool.Release(product);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
