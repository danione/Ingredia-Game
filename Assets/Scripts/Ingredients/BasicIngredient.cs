using UnityEngine;

public class BasicIngredient : FallableObject, IIngredient
{
    private IngredientData _data;
    public IngredientData Data { get { return _data; } set => Initialise(value); }

    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _highlightRenderer;

    private void Awake()
    {
        _spriteRenderer = transform.GetChild(2).GetComponent<SpriteRenderer>();
        _highlightRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        /*
        GameEventHandler.Instance.HighlightedIngredient += OnHighlight;
        PlayerEventHandler.Instance.EmptiedCauldron += OnCauldronEmptied;*/
    }

    private void OnDestroy()
    {
        /*
        try
        {
            GameEventHandler.Instance.HighlightedIngredient -= OnHighlight;
            PlayerEventHandler.Instance.EmptiedCauldron -= OnCauldronEmptied;
        }
        catch { }
        */
    }

    public void Initialise(IngredientData data, bool isHighlighted = false)
    {
        
        
        _data = data;
        _spriteRenderer.sprite = _data.sprite;
        /*if(_highlightRenderer != null)
        {
            _highlightRenderer.gameObject.SetActive(false);
        }*/
    }
    /*
    private void OnHighlight(IngredientData data)
    {
        if (data == _data)
        {
            Debug.Log("Highlighting " + data.ingredientName);

            Highlight();
        }

    }

    public void Highlight()
    {
        
        if(_data.highlightedSprite != null && _highlightRenderer != null && !_highlightRenderer.gameObject.activeSelf)
        {
            _highlightRenderer.gameObject.SetActive(true);
            _highlightRenderer.sprite = _data.highlightedSprite;
        }
            
    }

    private void OnCauldronEmptied()
    {
        if(_highlightRenderer != null)
            _highlightRenderer.gameObject.SetActive(false);
    }
    */
}
