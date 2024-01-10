using UnityEngine;

public class BasicIngredient : FallableObject, IIngredient
{
    private IngredientData _data;
    public IngredientData Data { get { return _data; } set => Initialise(value); }

    private SpriteRenderer _spriteRenderer;
    private Transform _highlightRenderer;

    private void Start()
    {
        _highlightRenderer = transform.GetChild(1);
    }

    public void Initialise(IngredientData data, bool isHighlighted = false)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _data = data;

        _spriteRenderer.sprite = _data.sprite;

        if(_data.highlightedSprite != null && _highlightRenderer != null)
        {
            _highlightRenderer.gameObject.GetComponent<SpriteRenderer>().sprite = _data.highlightedSprite;
            _highlightRenderer.gameObject.SetActive(isHighlighted);
        }



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
        _highlightRenderer.gameObject.SetActive(true);
    }

    private void OnCauldronEmptied()
    {
        _highlightRenderer.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.HighlightedIngredient -= OnHighlight;
        PlayerEventHandler.Instance.EmptiedCauldron -= OnCauldronEmptied;
    }
}
