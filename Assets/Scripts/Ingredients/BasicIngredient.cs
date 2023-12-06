using UnityEngine;

public class BasicIngredient : FallableObject, IIngredient
{
    private IngredientData _data;
    public IngredientData Data { get { return _data; } set => Initialise(value); }

    public bool isHighlighted;
    
    public void Initialise(IngredientData data)
    {
        _data = data;
        gameObject.GetComponent<SpriteRenderer>().sprite = _data.sprite;
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
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnCauldronEmptied()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.HighlightedIngredient -= OnHighlight;
        PlayerEventHandler.Instance.EmptiedCauldron -= OnCauldronEmptied;
    }
}
