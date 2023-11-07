using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionBarrier : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("Dangerous Object"))
        {
            Destroy(other.gameObject);
        }
    }
}
