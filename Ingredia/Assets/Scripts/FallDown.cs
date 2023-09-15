using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    public float fallSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameOver)
        {
            transform.Translate(Time.deltaTime * Vector3.down * fallSpeed);
        }
        
    }
}
