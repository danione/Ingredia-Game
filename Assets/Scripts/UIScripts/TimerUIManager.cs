using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIManager : MonoBehaviour
{
    [SerializeField] private float offsetY;
    private List<Transform> activeTimers;
    // Start is called before the first frame update

    [SerializeField] private TimerDataObject testObject;
    [SerializeField] private PotionsData testPotion;

    [SerializeField] private Vector3 minScale = new Vector3(1.05f, 1.05f, 1.05f);
    [SerializeField] private Vector3 maxScale = new Vector3(0.95f, 0.95f, 0.95f);
    [SerializeField] private float scalingSpeed = 2.5f;
    [SerializeField] private float scalingDuration = 2;

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

    /*
    private IEnumerator Pulse()
    {
        while (active)
        {
            yield return RepeatLerping(minScale, maxScale, scalingDuration);
            yield return RepeatLerping(maxScale, minScale, scalingDuration);
        }
    }

   

    IEnumerator RepeatLerping(Vector3 startScale, Vector3 endScale, float time)
    {
        float t = 0.0f;
        float rate = (1f / time) * scalingSpeed;

        while(t< 1f)
        {
            t += Time.deltaTime * rate;
            if (activeTimers[0] == null) break;
            activeTimers[0].localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
    }*/


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
        var newObject = availableObjects[0].CreateANewTimer();
        var positionEstablish = new Vector3(newObject.position.x,
        newObject.position.y - activeTimers.Count * (newObject.GetComponent<RectTransform>().rect.height + offsetY),
        newObject.position.z);

        newObject.position = positionEstablish;

        activeTimers.Add(newObject);
    }
}
