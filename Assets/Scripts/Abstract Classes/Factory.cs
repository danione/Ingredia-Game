using UnityEngine;

public abstract class Factory
{
    public abstract IProduct GetProduct(Vector3 position);
}
