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


    [SerializeField] private GhostNotificationObject ghostObject;
    [SerializeField] private LaserNotificationObject laserObject;
    [SerializeField] private ProtectionNotificationObject shieldObject;

    private int activeTimersCount;

    void Start()
    {
        GameEventHandler.Instance.GhostActivated += OnGhostActivated;
        GameEventHandler.Instance.GhostDeactivated += OnGhostDeactivated;
        GameEventHandler.Instance.LaserActivated += OnLaserActivated;
        GameEventHandler.Instance.LaserDeactivated += OnLaserDeactivated;
        GameEventHandler.Instance.ShieldDisabled += OnShieldDisabled;
        GameEventHandler.Instance.ShieldEnabled += OnShieldEnabled;

        activeTimersCount = 0;

        ghostObject.Setup();
        laserObject.Setup();
        shieldObject.Setup();
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

    private void OnShieldEnabled()
    {
        AssignNewBarrierTimer();
    }

    private void OnShieldDisabled()
    {
        activeTimersCount--;
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

    public void AssignNewBarrierTimer()
    {
        Transform newObject = shieldObject.GenerateANewTimer(this);
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
