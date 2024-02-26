using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SimpleProjectile projectile = other.gameObject.GetComponent<SimpleProjectile>();
        if (projectile != null && projectile.IsSourcePlayer())
        {
            GameEventHandler.Instance.DestroyObject(other.gameObject);
        }
    }
}
