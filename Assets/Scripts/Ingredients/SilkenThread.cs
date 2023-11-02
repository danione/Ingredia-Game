using UnityEngine;
public class SilkenThread : FallableObject, IIngredient, IProduct
{
    public string Name => "silkenThread";

    public string ProductName => Name;

    public void Initialise()
    {
       // Debug.Log("Nothing yet");
    }
}

