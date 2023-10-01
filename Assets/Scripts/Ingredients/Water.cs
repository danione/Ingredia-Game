using UnityEngine;

public class Water : FallableObject, IIngredient, IProduct
{
    public string Name => "water";

    public string ProductName => Name;

    public void Initialise()
    {
       // Debug.Log("Nothing yet");
    }
}
