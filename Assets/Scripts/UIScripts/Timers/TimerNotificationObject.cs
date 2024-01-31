using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TimerNotificationObject
{
    // Setup
    public Transform timerTemplateObject;
    public TimerDataObject timerDataObject;
    public TimerPulsingObject pulsingData;

    [System.NonSerialized] public Transform currentObject;
   
    private bool isPulsing;
    private float scalingDuration;


    public Transform CreateANewTimer(TimerUIManager manager)
    {
        Transform newObject = GameObject.Instantiate(timerTemplateObject, timerTemplateObject.parent);
        newObject.GetChild(0).GetComponent<Image>().sprite = timerDataObject.BorderSpriteIdle;
        newObject.GetChild(1).GetComponent<Image>().sprite = timerDataObject.IconSpriteIdle;
        newObject.gameObject.SetActive(true);
        scalingDuration = pulsingData.scalingDurationStart;
        currentObject = newObject;
        isPulsing = true;
        manager.StartCoroutine(Pulse());

        return newObject;
    }
    private IEnumerator Pulse()
    {
        while (isPulsing)
        {
            yield return RepeatLerping(pulsingData.minScale, pulsingData.maxScale, scalingDuration);
            yield return RepeatLerping(pulsingData.maxScale, pulsingData.minScale, scalingDuration);
        }
    }

    private IEnumerator RepeatLerping(Vector3 startScale, Vector3 endScale, float time)
    {
        float t = 0.0f;
        float rate = (1f / time) * pulsingData.scalingSpeed;

        while (t < 1f)
        {
            t += Time.deltaTime * rate;
            if (currentObject == null) break;
            currentObject.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
    }

    public void ChangePulse(float pulseAmount)
    {

       if (pulsingData.lastCheckpoint < pulseAmount && pulseAmount < pulsingData.firstCheckpoint)
       {
            scalingDuration = pulsingData.scalingDurationFirstCheckpoint;
       }
       else if(pulseAmount < pulsingData.lastCheckpoint)
       {
            scalingDuration = pulsingData.scalingDurationLastCheckpoint;
       }
        else if(pulseAmount > pulsingData.firstCheckpoint)
        {
            scalingDuration = pulsingData.scalingDurationStart;
        }
    }

    public void ForgetObject()
    {
        isPulsing = false;
        GameObject.Destroy(currentObject.gameObject);
    }
}
