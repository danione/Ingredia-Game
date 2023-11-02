using UnityEngine;

public class Ashes : FallableObject, IIngredient, IProduct
{
    public string Name => "ashes";

    public string ProductName { get => Name;}

    public void Initialise()
    {
       // Debug.Log("Nothing for now");
    }
}
