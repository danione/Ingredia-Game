using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectFactory : Factory
{
    private MonoBehaviour productPrefab;

    public ObjectFactory(MonoBehaviour product)
    {
        productPrefab = product;
    }

    public override IProduct GetProduct(Vector3 position)
    {
        GameObject instance = GameObject.Instantiate(productPrefab.gameObject, position, Quaternion.identity);
        IProduct product = instance.GetComponent<IProduct>();
        product.Initialise();
        return product;

    }
}
