using UnityEngine;

public class Pumpkin : FallableObject, IIngredient, IProduct
{
    public string Name => "pumpkin";
    public string ProductName => Name;

    public void Initialise()
    {
       // Debug.Log("Nothing yet");
    }
}
