using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float upperYBound = 13.0f;
    private float lowerYBound = -13.0f;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < lowerYBound || transform.position.y > upperYBound)
        {
            GameEventHandler.Instance.DestroyObject(gameObject);
        }
    }
}
