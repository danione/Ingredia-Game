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
    [SerializeField] private PotionsData testPotion2;


    [SerializeField] private NotificationObject ghostObject;
    [SerializeField] private NotificationObject laserObject;
    [SerializeField] private NotificationObject shieldObject;

    private int activeTimersCount;

    void Start()
    {
        GameEventHandler.Instance.GhostActivated += OnGhostActivated;
        GameEventHandler.Instance.GhostDeactivated += OnAnyDisabled;
        GameEventHandler.Instance.LaserActivated += OnLaserEnabled;
        GameEventHandler.Instance.LaserDeactivated += OnAnyDisabled;
        GameEventHandler.Instance.ShieldDisabled += OnAnyDisabled;
        GameEventHandler.Instance.ShieldEnabled += OnShieldActivated;
        

        activeTimersCount = 0;

        GhostSetup();

        LaserSetup();

        ShieldSetup();
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerController.Instance.inventory.AddPotion(testPotion2);
        }
    }

    private void OnLaserEnabled()
    {
        AssignNewTimer(laserObject);
    }
    private void OnGhostActivated()
    {
        AssignNewTimer(ghostObject);
    }

    private void OnShieldActivated()
    {
        AssignNewTimer(shieldObject);
    }

    private void OnAnyDisabled()
    {
        activeTimersCount--;
    }

    private void GhostSetup()
    {
        GameEventHandler.Instance.GhostDeactivated += ghostObject.OnDeactivated;
        GameEventHandler.Instance.SentGhostCurrentTimers += ghostObject.OnSendCurrentTimers;
        PlayerEventHandler.Instance.TransformIntoGhost += ghostObject.OnTransform;
    }

    private void LaserSetup()
    {
        GameEventHandler.Instance.LaserDeactivated += laserObject.OnDeactivated;
        GameEventHandler.Instance.SentLaserStats += laserObject.OnSendCurrentTimers;
        PlayerEventHandler.Instance.LaserFired += laserObject.OnTransform;
    }

    private void ShieldSetup()
    {
        GameEventHandler.Instance.ShieldDisabled += shieldObject.OnDeactivated;
        GameEventHandler.Instance.SentShieldStats += shieldObject.OnSendCurrentTimers;
    }

    public void AssignNewTimer(NotificationObject notObject)
    {
        Transform newObject = notObject.GenerateANewTimer(this, activeTimersCount);
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
