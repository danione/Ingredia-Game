using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIManager : MonoBehaviour
{

    [SerializeField] private Transform timerTemplateObject;
    [SerializeField] private TimerDataObject testObject;
    // Start is called before the first frame update

    void Start()
    {
        var newObject = Instantiate(timerTemplateObject, timerTemplateObject.parent);
        newObject.GetChild(0).GetComponent<Image>().sprite = testObject.BorderSpriteIdle;
        newObject.GetChild(1).GetComponent<Image>().sprite = testObject.IconSpriteIdle;
        newObject.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
