using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    [SerializeField] private RectTransform backrgoundRectTransform;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private float textPaddingSize;
    [SerializeField] private RectTransform canvas;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ShowTooltip("djhsajhdsjahdjshajhds");
    }

    private void Update()
    {
        rectTransform.anchoredPosition = Input.mousePosition / canvas.localScale.x;
    }


    private void ShowTooltip(string text)
    {
        gameObject.SetActive(true);

        tooltipText.text = text;
        tooltipText.ForceMeshUpdate();

        Vector2 bgSize = tooltipText.GetRenderedValues(false);
        Vector2 padding = new Vector2(textPaddingSize * 2, textPaddingSize*2);

        backrgoundRectTransform.sizeDelta = bgSize + padding;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }


}
