using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplayCanvas : MonoBehaviour
{
    [SerializeField] private List<GameObject> tutorialDisableObjects;
    [SerializeField] private List<GameObject> normalDisableObjects;

    private static UIDisplayCanvas instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        GameEventHandler.Instance.SetTutorialMode += OnSetTutorialMode; 
    }

    private void OnSetTutorialMode()
    {
        foreach (GameObject go in tutorialDisableObjects)
        {
            go.SetActive(false);
        }
        foreach(GameObject go in normalDisableObjects)
        {
            go.SetActive(true);
        }
    }
}
