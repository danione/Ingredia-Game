using UnityEngine;

public abstract class Factory
{
    public abstract IIngredient GetProduct(Vector3 position, IngredientData data);
}
