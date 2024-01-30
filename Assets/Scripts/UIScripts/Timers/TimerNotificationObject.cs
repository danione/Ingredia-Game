using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TimerNotificationObject
{
    // Setup
    public Transform timerTemplateObject;
    public TimerDataObject timerDataObject;

    protected static Transform currentObject;
    
    public Transform CreateANewTimer()
    {
        Transform newObject = GameObject.Instantiate(timerTemplateObject, timerTemplateObject.parent);
        newObject.GetChild(0).GetComponent<Image>().sprite = timerDataObject.BorderSpriteIdle;
        newObject.GetChild(1).GetComponent<Image>().sprite = timerDataObject.IconSpriteIdle;
        newObject.gameObject.SetActive(true);
        currentObject = newObject;
        FurtherSetupInstructions();
        return newObject;
    }

    protected virtual void FurtherSetupInstructions()
    {
        // Nothing
    }

    public void ForgetObject()
    {
        currentObject = null;
    }


}
