using UnityEngine;

public class BasicIngredient : FallableObject, IIngredient, IProduct
{
    [SerializeField] private string nameOfIngredient;
    public string Name => name;

    public string ProductName => name;

    public void Initialise()
    {
        // TBA
    }
}
