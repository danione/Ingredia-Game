using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplayCanvas : MonoBehaviour
{
    private static UIDisplayCanvas instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
}
