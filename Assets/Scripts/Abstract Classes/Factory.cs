using UnityEngine;

public abstract class Factory
{
    public abstract Transform GetProduct(Vector3 position, IngredientData data);
}
