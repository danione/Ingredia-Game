using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI instance;

    [SerializeField] private RectTransform backrgoundRectTransform;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private float textPaddingSize;
    [SerializeField] private RectTransform canvas;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) { instance = this; }
        else { Destroy(gameObject); }

        rectTransform = GetComponent<RectTransform>();
        HideTooltip();
    }

    private void Update()
    {
        Vector2 anchoredPos = Input.mousePosition / canvas.localScale.x;

        if(anchoredPos.x + backrgoundRectTransform.rect.width > canvas.rect.width)
        {
            anchoredPos.x = canvas.rect.width - backrgoundRectTransform.rect.width;
        }

        if(anchoredPos.y + backrgoundRectTransform.rect.height > canvas.rect.height)
        {
            anchoredPos.y = canvas.rect.height - backrgoundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPos;
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

    public static void ShowTooltip_Static(string text)
    {
        instance.ShowTooltip(text);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
