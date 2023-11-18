using UnityEngine;

public class BasicIngredient : FallableObject, IIngredient
{
    private IngredientData _data;
    public IngredientData Data { get { return _data; } set => Initialise(value); }

    public void Initialise(IngredientData data)
    {
        _data = data;
        gameObject.GetComponent<SpriteRenderer>().sprite = _data.sprite;
    }
}
