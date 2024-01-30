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
    private bool currentGhostState = false;
    private bool active;


    [SerializeField] private Vector3 minScale = new Vector3(1.05f, 1.05f, 1.05f);
    [SerializeField] private Vector3 maxScale = new Vector3(0.95f, 0.95f, 0.95f);
    [SerializeField] private float scalingSpeed = 2.5f;
    [SerializeField] private float scalingDuration = 2;

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
    }

    private void OnGhostActivated()
    {
        activeTimers.Add(CreateANewTimer());
        PlayerEventHandler.Instance.TransformIntoGhost += OnTransformIntoGhost;
        active = true;
        StartCoroutine(Pulse());
    }

    private void OnGhostDeactivated()
    {
        PlayerEventHandler.Instance.TransformIntoGhost -= OnTransformIntoGhost;
        active = false;
        Destroy(activeTimers[0].gameObject);
        activeTimers.Clear();
    }

    private void OnTransformIntoGhost(bool isTransforming)
    {
        if (currentGhostState != isTransforming) 
        {
            ChangeImages(isTransforming);
            currentGhostState = isTransforming;  
        }
    }

    private void ChangeImages(bool isTransforming)
    {
        if (activeTimers.Count == 0) return;

        if (isTransforming)
        {
            activeTimers[0].GetChild(0).GetComponent<Image>().sprite = testObject.BorderSpriteEngaged;
            activeTimers[0].GetChild(1).GetComponent<Image>().sprite = testObject.IconSpriteEngaged;
        }
        else
        {
            activeTimers[0].GetChild(0).GetComponent<Image>().sprite = testObject.BorderSpriteIdle;
            activeTimers[0].GetChild(1).GetComponent<Image>().sprite = testObject.IconSpriteIdle;
        }
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

public class PulseEffect
{
    // Grow parameters

}
