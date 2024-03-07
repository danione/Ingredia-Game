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
    [SerializeField] float fixedWidth;

    private RectTransform rectTransform;
    private RectTransform tooltipRectTransform;


    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) { instance = this; }
        else { Destroy(gameObject); }

        rectTransform = GetComponent<RectTransform>();
        tooltipRectTransform = tooltipText.rectTransform;
        HideTooltip();
    }

    private void Start()
    {
        PlayerEventHandler.Instance.UpgradesMenuOpen += HideTooltip_Static;
        PlayerEventHandler.Instance.ClosedAllOpenMenus += HideTooltip_Static;
    }

    private void OnDestroy()
    {
        PlayerEventHandler.Instance.ClosedAllOpenMenus -= HideTooltip_Static;
        PlayerEventHandler.Instance.UpgradesMenuOpen -= HideTooltip_Static;

    }

    private void ShowTooltip(string text, RectTransform obj)
    {
        gameObject.SetActive(true);

        tooltipText.text = text;
        tooltipText.ForceMeshUpdate();

        Vector2 bgSize = tooltipText.GetRenderedValues(false);

        // Calculate the desired height
        float desiredHeight = Mathf.Max(bgSize.y, (bgSize.x / fixedWidth) * bgSize.y);

        bgSize.y = desiredHeight;
        bgSize.x = fixedWidth;
        Vector2 padding = new Vector2(textPaddingSize * 2, textPaddingSize*2);

        tooltipRectTransform.sizeDelta = bgSize + padding;
        backrgoundRectTransform.sizeDelta = bgSize + padding;
        SetPos(obj);
    }

    private void SetPos(RectTransform obj)
    {
        Vector2 anchoredPos = obj.position;
        anchoredPos.x = obj.position.x - obj.rect.width / 2;
        anchoredPos.y = obj.position.y + obj.rect.height / 2;

        if (anchoredPos.x + backrgoundRectTransform.rect.width > canvas.rect.width)
        {
            anchoredPos.x = canvas.rect.width - backrgoundRectTransform.rect.width;
        }

        if (anchoredPos.y + backrgoundRectTransform.rect.height > canvas.rect.height)
        {
            anchoredPos.y = canvas.rect.height - backrgoundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPos;
    }


    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string text, RectTransform obj)
    {
        instance.ShowTooltip(text, obj);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
