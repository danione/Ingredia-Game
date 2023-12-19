using UnityEngine;

public class BasicIngredient : FallableObject, IIngredient
{
    private IngredientData _data;
    public IngredientData Data { get { return _data; } set => Initialise(value); }

    private SpriteRenderer _spriteRenderer;
    
    public void Initialise(IngredientData data, bool isHighlighted = false)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _data = data;

        _spriteRenderer.sprite = isHighlighted ? _data.highlightedSprite : _data.sprite;

        GameEventHandler.Instance.HighlightedIngredient += OnHighlight;
        PlayerEventHandler.Instance.EmptiedCauldron += OnCauldronEmptied;
    }

    private void OnHighlight(IngredientData data)
    {
        if (data == _data)
        {
            Highlight();
        }

    }

    public void Highlight()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _data.highlightedSprite;
    }

    private void OnCauldronEmptied()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _data.sprite;
    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.HighlightedIngredient -= OnHighlight;
        PlayerEventHandler.Instance.EmptiedCauldron -= OnCauldronEmptied;
    }
}
