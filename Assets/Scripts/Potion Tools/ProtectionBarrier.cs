using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionBarrier : MonoBehaviour
{
    [SerializeField] private List<string> targets = new();

    private void OnTriggerEnter(Collider other)
    {
        if (targets.Contains(other.tag))
        {
            GameEventHandler.Instance.DestroyObject(other.gameObject);
            return;
        }
        
        SimpleProjectile proj = other.GetComponent<SimpleProjectile>();
        if (proj != null && !proj.IsSourcePlayer())
        {
            GameEventHandler.Instance.DestroyObject(other.gameObject);
        }
    }
}
