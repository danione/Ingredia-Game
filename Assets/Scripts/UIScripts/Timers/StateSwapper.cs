using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateSwapper
{
    public void SetValue(TimerNotificationObject timerNotification, string value)
    {
        Transform strengthLeft;
        try
        {
            strengthLeft = timerNotification.currentObject.GetChild(2);
        } catch {
            return;
        }

        strengthLeft.GetComponent<TextMeshProUGUI>().text = value;
    }

    public void ChangeImages(TimerNotificationObject timerNotification, bool isTransforming)
    {
        if (timerNotification.currentObject == null) return;

        if (isTransforming)
        {
            timerNotification.currentObject.GetChild(0).GetComponent<Image>().sprite = timerNotification.timerDataObject.BorderSpriteEngaged;
            timerNotification.currentObject.GetChild(1).GetComponent<Image>().sprite = timerNotification.timerDataObject.IconSpriteEngaged;
            timerNotification.currentObject.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            timerNotification.currentObject.GetChild(0).GetComponent<Image>().sprite = timerNotification.timerDataObject.BorderSpriteIdle;
            timerNotification.currentObject.GetChild(1).GetComponent<Image>().sprite = timerNotification.timerDataObject.IconSpriteIdle;
            timerNotification.currentObject.GetChild(2).gameObject.SetActive(false);
        }
    }
}
