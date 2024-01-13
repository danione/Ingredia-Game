using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.gameObject.transform.position = gameObject.transform.position;
            TutorialManager.instance.Collided();
            Destroy(gameObject);
        }
    }
   
}
