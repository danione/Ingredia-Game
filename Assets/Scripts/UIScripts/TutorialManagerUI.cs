using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManagerUI : MonoBehaviour
{
    private static TutorialManagerUI instance;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private List<string> congratulatoryHeadlines;
    [SerializeField] private List<string> congratulatoryDescriptions;
    
    [SerializeField] private List<GameObject> disableObjects;


    public void DisplayFromTutorialStage(TutorialStage stage)
    {
        title.text = stage.title;
        description.text = stage.description;
    }

    public void DisplayCongrats()
    {
        int random = Random.Range(0, congratulatoryHeadlines.Count);
        title.text = congratulatoryHeadlines[random];
        description.text = congratulatoryDescriptions[random];
    }
}
