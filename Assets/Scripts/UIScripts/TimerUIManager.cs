using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIManager : MonoBehaviour
{
    [SerializeField] private float offsetY;

    [SerializeField] private PotionsData testPotion;
    [SerializeField] private PotionsData testPotion1;


    [SerializeField] private GhostNotificationObject ghostObject;
    [SerializeField] private LaserNotificationObject laserObject;

    private int activeTimersCount;

    void Start()
    {
        GameEventHandler.Instance.GhostActivated += OnGhostActivated;
        GameEventHandler.Instance.GhostDeactivated += OnGhostDeactivated;
        GameEventHandler.Instance.LaserActivated += OnLaserActivated;
        GameEventHandler.Instance.LaserDeactivated += OnLaserDeactivated;
        activeTimersCount = 0;

        ghostObject.Setup();
        laserObject.Setup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerController.Instance.inventory.AddPotion(testPotion);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerController.Instance.inventory.AddPotion(testPotion1);
        }
    }

    private void OnGhostActivated()
    {
        AssignNewGhostTimer();
    }

    private void OnGhostDeactivated()
    {
        activeTimersCount--;
    }

    private void OnLaserActivated()
    {
        AssignNewLaserTimer();
    }

    private void OnLaserDeactivated()
    {
        activeTimersCount--;
    }

    public void AssignNewGhostTimer()
    {
        Transform newObject = ghostObject.GenerateANewTimer(this);
        AssignTimer(newObject);
    }

    public void AssignNewLaserTimer()
    {
        Transform newObject = laserObject.GenerateANewTimer(this);
        AssignTimer(newObject);
    }

    private void AssignTimer(Transform obj)
    {
        var positionEstablish = new Vector3(obj.position.x,
        obj.position.y - activeTimersCount * (obj.GetComponent<RectTransform>().rect.height + offsetY),
        obj.position.z);

        obj.position = positionEstablish;

        activeTimersCount++;
    }
}
