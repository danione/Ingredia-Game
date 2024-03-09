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

    private List<NotificationObject> listObjects = new();

    private int activeTimersCount;

    void Start()
    {
        
        GameEventHandler.Instance.GhostActivated += OnGhostActivated;
        GameEventHandler.Instance.LaserActivated += OnLaserEnabled;
        GameEventHandler.Instance.ShieldEnabled += OnShieldActivated;

        ghostObject.SetupManager(this);
        laserObject.SetupManager(this);
        shieldObject.SetupManager(this);


        GhostSetup();
        LaserSetup();
        ShieldSetup();
    }

    private void OnDestroy()
    {
        try
        {
            GameEventHandler.Instance.GhostActivated -= OnGhostActivated;
            GameEventHandler.Instance.LaserActivated -= OnLaserEnabled;
            GameEventHandler.Instance.ShieldEnabled -= OnShieldActivated;
        }
        catch { }
        try
        {
            GameEventHandler.Instance.LaserDeactivated -= laserObject.OnDeactivated;
            GameEventHandler.Instance.SentLaserStats -= laserObject.OnSendCurrentTimers;
            PlayerEventHandler.Instance.LaserFired -= laserObject.OnTransform;
        }
        catch { }
        try
        {

            GameEventHandler.Instance.GhostDeactivated -= ghostObject.OnDeactivated;
            GameEventHandler.Instance.SentGhostCurrentTimers -= ghostObject.OnSendCurrentTimers;
            PlayerEventHandler.Instance.TransformIntoGhost -= ghostObject.OnTransform;
        }
        catch { }

        try
        {
            GameEventHandler.Instance.ShieldDisabled -= shieldObject.OnDeactivated;
            GameEventHandler.Instance.SentShieldStats -= shieldObject.OnSendCurrentTimers;
        }
        catch { }
        listObjects.Clear();
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

    public void OnAnyDisabled(int disabledAtPos)
    {
        foreach(var obj in listObjects)
        {
            int objPos = obj.GetCurrentPos();
            if (objPos > disabledAtPos)
                obj.ChangePos(objPos-1, offsetY);
        }
        activeTimersCount--;
    }

    private void GhostSetup()
    {
        GameEventHandler.Instance.GhostDeactivated += ghostObject.OnDeactivated;
        GameEventHandler.Instance.SentGhostCurrentTimers += ghostObject.OnSendCurrentTimers;
        PlayerEventHandler.Instance.TransformIntoGhost += ghostObject.OnTransform;
        listObjects.Add(ghostObject);
    }

    private void LaserSetup()
    {
        GameEventHandler.Instance.LaserDeactivated += laserObject.OnDeactivated;
        GameEventHandler.Instance.SentLaserStats += laserObject.OnSendCurrentTimers;
        PlayerEventHandler.Instance.LaserFired += laserObject.OnTransform;
        listObjects.Add(laserObject);
    }

    private void ShieldSetup()
    {
        GameEventHandler.Instance.ShieldDisabled += shieldObject.OnDeactivated;
        GameEventHandler.Instance.SentShieldStats += shieldObject.OnSendCurrentTimers;
        listObjects.Add(shieldObject);
    }

    public void AssignNewTimer(NotificationObject notObject)
    {
        Transform newObject = notObject.GenerateANewTimer(this, activeTimersCount);
        if (newObject == null) { return; }
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
