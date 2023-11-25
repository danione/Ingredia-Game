using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNearbyObjects : MonoBehaviour
{
    public Dictionary<int,GameObject> ingredients = new ();

    private void Start()
    {
        PlayerController.Instance.collision += OnRemoveElement;
    }

    public void OnRemoveElement(int instanceID)
    {
        if (ingredients.ContainsKey(instanceID))
        {
            ingredients.Remove(instanceID);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ingredient"))
        {
            ingredients[other.gameObject.GetInstanceID()] = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnRemoveElement(other.gameObject.GetInstanceID());
    }
}
