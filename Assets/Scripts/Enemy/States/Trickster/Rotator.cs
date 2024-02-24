using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private TricksterEnemy trickster;

    private void Start()
    {
        trickster = GetComponentInParent<TricksterEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            other.GetComponent<FallableObject>().SwapToCirculate(gameObject.transform);
            trickster.AddCapturedIngredient(other.gameObject);
        }
    }
}
