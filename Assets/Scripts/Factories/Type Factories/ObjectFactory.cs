using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectFactory : Factory
{
    private Transform productPrefab;

    public ObjectFactory(Transform product)
    {
        productPrefab = product;
    }

    public override Transform GetProduct(Vector3 position, IngredientData scriptableObject)
    {
        GameObject instance = GameObject.Instantiate(productPrefab.gameObject, position, Quaternion.identity);
        IIngredient product = instance.GetComponent<IIngredient>();
        product.Initialise(scriptableObject);
        return instance.transform;

    }
}
