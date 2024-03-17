using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImprovedUpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject improvedUpgrade;
    [SerializeField] private float waitFor;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private float fadeOutSpeed;

    private TextMeshProUGUI improvedUpgradeText;
    private void Start()
    {
        GameEventHandler.Instance.UpgradeImproved += OnUpgradeImproved;
        improvedUpgradeText = improvedUpgrade.GetComponent<TextMeshProUGUI>();

    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.UpgradeImproved -= OnUpgradeImproved;
    }

    private void OnUpgradeImproved(string name)
    {
        improvedUpgradeText.text = name + " has been improved!";
        improvedUpgradeText.gameObject.SetActive(true);
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        improvedUpgradeText.alpha = 0;

        while (improvedUpgradeText.alpha  < 1)
        {
            improvedUpgradeText.alpha += fadeInSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(waitFor);

        while (improvedUpgradeText.alpha > 0)
        {
            improvedUpgradeText.alpha -= fadeOutSpeed * Time.deltaTime;
            yield return null;
        }

        improvedUpgrade.SetActive(false);
    }
   
}
