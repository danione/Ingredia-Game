using UnityEngine;

public class BasicIngredient : FallableObject, IIngredient
{
    private IngredientData _data;
    public IngredientData Data { get { return _data; } set => Initialise(value); }
    
    public float Rarity { get
        {
            return _data.spawnChance == 0 ? (1.0f / Constants.Instance.IngredientsCount) : _data.spawnChance;
        } }
    
    public void Initialise(IngredientData data)
    {
        _data = data;
        gameObject.GetComponent<SpriteRenderer>().sprite = _data.sprite;
    }
}
