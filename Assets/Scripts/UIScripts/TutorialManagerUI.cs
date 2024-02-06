using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManagerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private List<string> congratulatoryHeadlines;
    [SerializeField] private List<string> congratulatoryDescriptions;
    [SerializeField] private GameObject tutorialUI;

    private void Start()
    {
        tutorialUI.SetActive(true);
    }

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
