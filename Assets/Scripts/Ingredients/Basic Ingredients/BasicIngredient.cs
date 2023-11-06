using UnityEngine;

public class BasicIngredient : FallableObject, IIngredient, IProduct
{
    [SerializeField] private string nameOfIngredient;
    public string IngredientName => nameOfIngredient;

    public string ProductName => nameOfIngredient;

    public void Initialise()
    {
        // TBA
    }
}
