using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIManager : MonoBehaviour
{
    [SerializeField] private float offsetY;
    private List<Transform> activeTimers;

    [SerializeField] private TimerDataObject testObject;
    [SerializeField] private PotionsData testPotion;

    [SerializeField] private List<TimerNotificationObject> availableObjects;

    void Start()
    {
        activeTimers = new List<Transform>();
        GameEventHandler.Instance.GhostActivated += OnGhostActivated;
        GameEventHandler.Instance.GhostDeactivated += OnGhostDeactivated;
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
        AssignANewTimer();
    }

    private void OnGhostDeactivated()
    {
        Destroy(activeTimers[0].gameObject);
        availableObjects[0].ForgetObject();
        activeTimers.Clear();
    }

    private void AssignANewTimer()
    {
        var newObject = availableObjects[0].CreateANewTimer(this);
        var positionEstablish = new Vector3(newObject.position.x,
        newObject.position.y - activeTimers.Count * (newObject.GetComponent<RectTransform>().rect.height + offsetY),
        newObject.position.z);

        newObject.position = positionEstablish;

        activeTimers.Add(newObject);
    }
}
