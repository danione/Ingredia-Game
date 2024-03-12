using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCircle : MonoBehaviour
{
    public bool hasPlayer = false;

    private void OnEnable()
    {
        hasPlayer = false;
    }

    private void OnDisable()
    {
        if(hasPlayer)
            PlayerEventHandler.Instance.AdjustMovementSpeed(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        
        if(controller != null)
        {
            hasPlayer = true;
            PlayerEventHandler.Instance.AdjustMovementSpeed(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            hasPlayer = false;
            PlayerEventHandler.Instance.AdjustMovementSpeed(false);
        }
    }
}
