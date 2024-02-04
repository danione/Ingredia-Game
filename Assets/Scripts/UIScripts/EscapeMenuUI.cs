using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject escapeMenu;

    // Start is called before the first frame update
    void Start()
    {
        PlayerEventHandler.Instance.EscapeMenuOpened += OnEscapeMenuOpened;
        PlayerEventHandler.Instance.ClosedAllOpenMenus += OnCloseMenu;
    }

    private void OnCloseMenu()
    {
        escapeMenu.SetActive(false);
    }

    private void OnEscapeMenuOpened()
    {
        if(escapeMenu.activeSelf == false)
        {
            GameManager.Instance.PauseGame();
        }
        else
        {
            PlayerEventHandler.Instance.CloseAMenu();
            GameManager.Instance.ResumeGame();
        }

        escapeMenu.SetActive(!escapeMenu.activeSelf);
    }
}
