using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIManager : MonoBehaviour
{

    [SerializeField] private Transform timerTemplateObject;
    
    [SerializeField] private float offsetY;
    private List<Transform> activeTimers;
    // Start is called before the first frame update

    [SerializeField] private TimerDataObject testObject;
    [SerializeField] private PotionsData testPotion;

    void Start()
    {
        activeTimers = new List<Transform>();
        GameEventHandler.Instance.GhostActivated += OnGhostActivated;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerController.Instance.inventory.AddPotion(testPotion);
        }
        
    }

    private void OnGhostActivated()
    {
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
}
