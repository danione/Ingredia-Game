using System.Collections.Generic;
using UnityEngine;

public class TimeStopperPoint : MonoBehaviour
{
    [SerializeField] private float radius;
    private HashSet<Collider> colliders = new();
    private bool hasBeenReleased = false;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = radius;

    }

    public void Init()
    {
        GameEventHandler.Instance.ReleasedAllTimeStopPoints += OnRelease;
    }

    private void OnRelease(GameObject obj)
    {
        hasBeenReleased = true;

        foreach(var collider in colliders)
        {
            if(collider.gameObject.activeSelf)
                collider.GetComponent<FallableObject>().SwapToMove();
        }

        GameEventHandler.Instance.ReleasedAllTimeStopPoints -= OnRelease;

        colliders.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("Ingredient") || other.CompareTag("Projectile")) && !hasBeenReleased)
        {
            other.GetComponent<FallableObject>().SwapToFreeze();

            if(!colliders.Contains(other)) colliders.Add(other);
        }
    }
}
