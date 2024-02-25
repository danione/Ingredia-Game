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
            FallableObject obj = other.GetComponent<FallableObject>();
            if(!obj.IsRotating)
            {
                obj.SwapToCirculate(gameObject.transform, new Vector3(0, 0, Random.Range(0, 2) * 2 - 1));
                trickster.AddCapturedIngredient(other.gameObject);
            }
            
        }
    }
}
