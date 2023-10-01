using UnityEngine;

public class Fishbones : FallableObject, IIngredient, IProduct
{
    public string Name => "fishbones";

    public string ProductName => Name;

    public void Initialise()
    {
        // Debug.Log("Nothing yet");
    }
}
