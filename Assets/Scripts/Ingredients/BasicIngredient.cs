using UnityEngine;

public class BasicIngredient : FallableObject, IIngredient
{
    private IngredientData _data;
    public IngredientData Data { get { return _data; } set => Initialise(value); }

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    public void Initialise(IngredientData data, bool isHighlighted = false)
    {
        _data = data;
        _spriteRenderer.sprite = _data.sprite;
    }
}
