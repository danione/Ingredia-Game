using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIManager : MonoBehaviour
{

    [SerializeField] private Transform timerTemplateObject;
    [SerializeField] private TimerDataObject testObject;
    [SerializeField] private float offsetY;
    private List<Transform> activeTimers;
    // Start is called before the first frame update

    void Start()
    {
        activeTimers = new List<Transform>();
        activeTimers.Add(CreateANewTimer());
        activeTimers.Add(CreateANewTimer());
    }

    private Transform CreateANewTimer()
    {
        var positionEstablish = new Vector3(timerTemplateObject.position.x, 
            timerTemplateObject.position.y - activeTimers.Count * (timerTemplateObject.GetComponent<RectTransform>().rect.height + offsetY), 
            timerTemplateObject.position.z);

        var newObject = Instantiate(timerTemplateObject, timerTemplateObject.parent);
        newObject.transform.position = positionEstablish;
        newObject.GetChild(0).GetComponent<Image>().sprite = testObject.BorderSpriteIdle;
        newObject.GetChild(1).GetComponent<Image>().sprite = testObject.IconSpriteIdle;
        newObject.gameObject.SetActive(true);

        return newObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
