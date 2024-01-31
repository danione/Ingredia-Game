using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIManager : MonoBehaviour
{
    [SerializeField] private float offsetY;
    private List<Transform> activeTimers;

    [SerializeField] private PotionsData testPotion;

    [SerializeField] private GhostNotificationObject ghostObject;

    void Start()
    {
        activeTimers = new List<Transform>();
        GameEventHandler.Instance.GhostActivated += OnGhostActivated;
        GameEventHandler.Instance.GhostDeactivated += OnGhostDeactivated;
        ghostObject.Setup();
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
        activeTimers.Clear();
    }

    private void AssignANewTimer()
    {
        var newObject = ghostObject.GenerateANewTimer(this);
        var positionEstablish = new Vector3(newObject.position.x,
        newObject.position.y - activeTimers.Count * (newObject.GetComponent<RectTransform>().rect.height + offsetY),
        newObject.position.z);

        newObject.position = positionEstablish;

        activeTimers.Add(newObject);
    }
}
