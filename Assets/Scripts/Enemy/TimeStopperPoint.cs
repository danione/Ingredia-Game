using System.Collections.Generic;
using UnityEngine;

public class TimeStopperPoint : MonoBehaviour
{
    [SerializeField] private float radius;
    private HashSet<Collider> colliders = new();
    private bool hasBeenReleased = false;
    private GameObject parent;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = radius;
    }

    public void Init(GameObject parent)
    {
        GameEventHandler.Instance.ReleasedAllTimeStopPoints += OnRelease;
        this.parent = parent;
    }

    private void OnRelease(GameObject obj)
    {
        if (obj != parent) return;

        hasBeenReleased = true;

        foreach(var collider in colliders)
        {
            if(collider.gameObject.activeSelf)
                collider.GetComponent<FallableObject>().SwapToMove();
        }

        GameEventHandler.Instance.ReleasedAllTimeStopPoints -= OnRelease;
        colliders.Clear();

        GameEventHandler.Instance.DestroyObject(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("Ingredient") || other.CompareTag("Projectile")) && !hasBeenReleased)
        {
            
            FallableObject otherFall = other.GetComponent<FallableObject>();
            if (!otherFall.IsRotating)
            {
                otherFall.SwapToFreeze();
                if (!colliders.Contains(other)) colliders.Add(other);
            }
        }
    }
}
