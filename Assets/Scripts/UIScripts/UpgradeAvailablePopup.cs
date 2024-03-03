using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeAvailablePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popup;
    [SerializeField] private float speedFadeout = 0.1f;
    [SerializeField] private float waitingSeconds = 3f;
    private UpgradeManager manager;

    private void Start()
    {
        manager = GameManager.Instance.GetComponent<UpgradeManager>();
        popup.gameObject.SetActive(false);
        PlayerEventHandler.Instance.CollectedGold += OnGoldCollected;
    }

    private void OnDestroy()
    {
        PlayerEventHandler.Instance.CollectedGold -= OnGoldCollected;
    }

    private void OnGoldCollected(int amount)
    {
        if (manager.canAffordUpgrades() && !popup.gameObject.activeSelf)
        {
            popup.alpha = 0;
            popup.gameObject.SetActive(true);
            StartCoroutine(FadeInNOut());
        }
    }

    private IEnumerator FadeInNOut()
    {
        while (popup.alpha < 1)
        {
            popup.alpha += speedFadeout * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(waitingSeconds);
        
        while(popup.alpha > 0)
        {
            popup.alpha -= speedFadeout * Time.deltaTime;
            yield return null;
        }
        popup.gameObject.SetActive(false);
    }
}
